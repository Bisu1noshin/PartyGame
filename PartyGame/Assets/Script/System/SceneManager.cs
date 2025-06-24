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

    private List<PlayerData> playerDatas;
    private List<InputDevice> inputDevices;
    private PlayerData[] pd;
    private Type playerScript;
    private PartyGame inputAction;
    private bool joinPlayerflag;
    protected GameObject[] player;

    private void Awake()
    {
        playerScript = PlayerType();

        playerDatas = new List<PlayerData>();
        pd = new PlayerData[PlayerLength];

        if (PlayerDataContllore.PlayerDataContllore_instance == null)
        {
            GameObject go = Resources.Load<GameObject>("System/PlayerDate");
            Instantiate(go);

            player = new GameObject[PlayerLength];
        }
        else {

            playerDatas = PlayerDataContllore.PlayerDataContllore_instance.GetPlayerDate();
            player = new GameObject[playerDatas.Count];
        }

        // inputManager
        {
            inputAction = new PartyGame();

            inputAction.Test.Test.started += OnTestInput;

            inputAction.Enable();
        }
        
        inputDevices = new List<InputDevice>();

        foreach (var device in InputSystem.devices)
        {
            // デバイス名をログ出力
            inputDevices.Add(device);
        }

        joinPlayerflag = false;
    }

    private void Update()
    {
        if (!DebugMode) {

            DebugMode = DebugCreatePlayer();
        }

        ExitDeviceIndex();

        UnityUpdate();
    }

    private GameObject CreatePlayer(PlayerData pd)
    {
        GameObject go = playerprefab;

        return PlayerParent.CreatePlayer(go, pd, playerScript);
    }

    private bool DebugCreatePlayer() {

        for (int i = 0; i < pd.Length; i++) {

            if (pd[i] == null) { return false; }
        }

        List<PlayerData> playerDatas = new List<PlayerData>();

        foreach (PlayerData pd in pd) {
            playerDatas.Add(pd);
        }

        PlayerDataContllore.PlayerDataContllore_instance.InitializePlayerDatas(playerDatas);

        for (int i = 0; i < player.Length; i++) {

            if (player[i] == null) {

                player[i]=CreatePlayer(playerDatas[i]);
            }
        }

        return true;
    }

    private void ExitDeviceIndex()
    {
        for (int i = 0; i < playerDatas.Count; i++){
            for (int j = 0; j < inputDevices.Count; j++) {

                if (playerDatas[i].JudgeInputControl(inputDevices[j])){

                    break;
                }

                pd[i] = null;
                joinPlayerflag = true;
            }
        }
    }

    private void JoinDevice(int index) {

        List<PlayerData> playerDatas = new List<PlayerData>();
        playerDatas = PlayerDataContllore.PlayerDataContllore_instance.GetPlayerDate();

        playerDatas[index] = pd[index];

        PlayerDataContllore.PlayerDataContllore_instance.InitializePlayerDatas(playerDatas);
    }

    // 抽象メソッド

    protected abstract void UnityUpdate();
    protected abstract Type PlayerType();

    // 入力イベント

    public void OnTestInput(InputAction.CallbackContext context)
    {
        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        // プレイヤーを登録
        for (int i = 0; i < pd.Length; i++)
        {
            if (pd[i] == null)
            {

                int user = i + 1;
                pd[i] = new PlayerData(user, device);

                Debug.Log("入室しました player:" + user);

                if (joinPlayerflag){

                    JoinDevice(i);
                    joinPlayerflag = false;
                }

                return;
            }

            Debug.Log("playerの数が最大値です");
        }
    }
}
