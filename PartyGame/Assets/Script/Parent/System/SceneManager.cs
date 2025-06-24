using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class SceneManagerParent : MonoBehaviour
{

    [SerializeField] private GameObject playerprefab;

    [Header("デバッグ用")]

    [SerializeField] private bool DebugMode;
    [SerializeField] private int PlayerLength;

    private List<InputDevice> inputDevices;
    private PlayerData[] pd;
    private Type playerScript;
    private PartyGame inputAction;
    private int joinPlayerflag;
    protected PlayerParent[] player;

    private void Awake()
    {
        playerScript = PlayerType();

        pd = new PlayerData[PlayerLength];

        inputDevices = new List<InputDevice>();

        if (PlayerDataContllore.PlayerDataContllore_instance == null)
        {
            GameObject go = Resources.Load<GameObject>("System/PlayerDate");
            Instantiate(go);

            player = new PlayerParent[PlayerLength];
        }
        else {

            List<PlayerData> pds = new List<PlayerData>();
            pds = PlayerDataContllore.PlayerDataContllore_instance.GetPlayerDate();
            player = new PlayerParent[pds.Count];
            
            foreach (PlayerData playerData in pds) {

                inputDevices.Add(playerData.GetDevice());
            }
        }

        // inputManager
        {
            inputAction = new PartyGame();

            inputAction.Test.Test.started += OnTestInput;

            InputSystem.onDeviceChange += (device, change) =>
            {

                switch (change)
                {
                    // 新しいデバイスがシステムに追加された
                    case InputDeviceChange.Added: break;

                    // 既存のデバイスがシステムから削除された
                    case InputDeviceChange.Removed:break;

                    // 切断された
                    case InputDeviceChange.Disconnected:
                        {

                            ExitDeviceIndex(device);
                        }
                        break;

                    // 再接続
                    case InputDeviceChange.Reconnected: break;

                    // 有効化
                    case InputDeviceChange.Enabled: break;

                    // 無効化
                    case InputDeviceChange.Disabled: break;

                    // 使用方法変更
                    case InputDeviceChange.UsageChanged: break;

                    // 構成変更
                    case InputDeviceChange.ConfigurationChanged: break;

                    case InputDeviceChange.SoftReset: break;

                    case InputDeviceChange.HardReset: break;

                    default: throw new ArgumentOutOfRangeException(nameof(change), change, null);
                }
            };

            inputAction.Enable();
        }

        joinPlayerflag = 0;
    }
    private void Update()
    {
        if (DebugMode) {

            DebugMode = DebugCreatePlayer();
            return;
        }

        UnityUpdate();
    }

    private void OnDestroy()
    {
        inputAction.Disable();
    }

    protected PlayerParent CreatePlayer(PlayerData pd,Vector3 p,Quaternion q)
    {
        GameObject go = playerprefab;

        // prefabをResorceから読み込む
        go = Resources.Load<GameObject>(PlayerFilePath(pd.GetFbxId()));

        return PlayerParent.CreatePlayer(go, pd, playerScript, p, q);
    }

    private bool DebugCreatePlayer() {

        for (int i = 0; i < pd.Length; i++) {

            if (pd[i] == null) { return true; }
        }

        List<PlayerData> playerDatas = new List<PlayerData>();
        
        foreach (PlayerData pd in pd) {
            playerDatas.Add(pd);
        }

        PlayerDataContllore.PlayerDataContllore_instance.InitializePlayerDatas(playerDatas);

        for (int i = 0; i < player.Length; i++) {

            if (player[i] == null) {

                Vector3 p = Vector3.zero;
                Quaternion q = Quaternion.identity;
                player[i] = CreatePlayer(playerDatas[i], p, q);
            }
        }

        return false;
    }

    private void ExitDeviceIndex(InputDevice device)
    {
        for (int i = 0; i < pd.Length; i++){

            if (pd[i] == null) { continue; }

            if (pd[i].JudgeInputControl(device))
            {
                pd[i] = null;
                joinPlayerflag++;
                break;
            }
        }

        for (int i = 0; i < inputDevices.Count; i++)
        {

            if (inputDevices[i] == device)
            {
                inputDevices[i] = null;
                return;
            }
        }
    }

    private void JoinDevice(int index,InputDevice device) {

        List<PlayerData> playerDatas = new List<PlayerData>();
        playerDatas = PlayerDataContllore.PlayerDataContllore_instance.GetPlayerDate();

        int user = index + 1;
        pd[index] = new(user, device);
        inputDevices.Add(device);

        playerDatas[index] = pd[index];

        PlayerDataContllore.PlayerDataContllore_instance.InitializePlayerDatas(playerDatas);
    }

    // 抽象メソッド

    protected abstract void UnityUpdate();
    protected abstract Type PlayerType();
    protected abstract string PlayerFilePath(int index);

    // 入力イベント

    public void OnTestInput(InputAction.CallbackContext context)
    {
        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        // 存在しているデバイスは除外
        foreach (InputDevice Id in inputDevices)
        {
            if (device == Id) { return; }
        }

        // プレイヤーを再登録
        if (joinPlayerflag > 0)
        {
            for (int i = 0; i < pd.Length; i++) {

                if (pd[i] == null){

                    JoinDevice(i, device);
                    player[i].SetPlayerData(pd[i]);
                    joinPlayerflag --;
                    return;
                }
            }
        }

        // プレイヤーを登録
        for (int i = 0; i < pd.Length; i++)
        {

            if (pd[i] == null)
            {

                int user = i + 1;
                pd[i] = new PlayerData(user, device);
                inputDevices.Add(device);

                Debug.Log("入室しました player:" + user);

                return;
            }
        }

        Debug.Log("playerの数が最大値です");
    }
}
