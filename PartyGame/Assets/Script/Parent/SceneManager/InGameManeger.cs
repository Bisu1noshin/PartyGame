using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public abstract class InGameManeger : MonoBehaviour
{
    [SerializeField] private bool DebagMode = default;
    [SerializeField] private int maxPlayerCount = default;

    protected PlayerInformation[] playerInformation = default;
    protected PlayerParent2[] player;

    private     Type            playerScript;
    private     InputAction     playerJoinInputAction;
    private     InputDevice[]   joinedDevices = default;
    private     List<int>       lostDeviceIndex;
    private     int             currentPlayerCount = 0;


    // コントローラーが抜けたときの処理
    // プレイヤーを呼び出す関数b
    // データが無い時の例外処理、デバッグ用の処理

    protected virtual void Awake()
    {
        lostDeviceIndex = new List<int>();

        // inputManager
        {
            playerJoinInputAction =
                new InputAction("playerJoin", InputActionType.Button, "<XInputController>/buttonSouth");

            playerJoinInputAction.performed += OnJoin;

            InputSystem.onDeviceChange += (device, change) =>
            {

                switch (change)
                {
                    // 新しいデバイスがシステムに追加された
                    case InputDeviceChange.Added: break;

                    // 既存のデバイスがシステムから削除された
                    case InputDeviceChange.Removed: break;

                    // 切断された
                    case InputDeviceChange.Disconnected:
                        {
                            lostDeviceIndex.Add(ExitDeviceIndex(device));
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
        }

        playerScript = SetPlayerScript();
        joinedDevices = new InputDevice[maxPlayerCount];
        player = new PlayerParent2[maxPlayerCount];
#if UNITY_EDITOR
        if (DebagMode)
            playerInformation = new PlayerInformation[maxPlayerCount];
#endif
    }

    protected virtual void OnDestroy()
    {
        playerJoinInputAction.performed -= OnJoin;
        playerJoinInputAction?.Dispose();
    }

    protected virtual void OnEnable() {
        playerJoinInputAction.Enable();
    }

    protected virtual void OnDisable() {
        playerJoinInputAction.Disable();
    }

    protected virtual void Update() {
#if UNITY_EDITOR

        if (DebagMode)
        {
            if (currentPlayerCount >= maxPlayerCount) {

                for (int i = 0; i < maxPlayerCount; i++) {

                    Vector3 vector3 = Vector3.zero;
                    Quaternion quat = Quaternion.identity;
                    GameObject prefab =
                        Resources.Load<GameObject>(SetPlayerPrefab(0));

                    // PlayerInputを所持した仮想のプレイヤーをインスタンス化
                    // ※Join要求元のデバイス情報を紐づけてインスタンスを生成する
                    player[i] = PlayerParent2.CreatePlayer(
                        prefab,
                        playerScript,
                        playerInformation[i].PairWithDevice,
                        i,
                        vector3,
                        quat
                        );
                }
               
                DebagMode = false;
            }
            return;
        }
#endif


    }

    // 抽象メソッド

    protected abstract string SetPlayerPrefab(int index);
    protected abstract Type SetPlayerScript();

    // メソッド

    private void OnJoin(InputAction.CallbackContext context)
    {
        if (lostDeviceIndex.Count > 0)
        {
            PlayerInput pi = player[lostDeviceIndex[0]].gameObject.GetComponent<PlayerInput>();
            InputUser.PerformPairingWithDevice(context.control.device, pi.user);
            joinedDevices[lostDeviceIndex[0]] = context.control.device;
            lostDeviceIndex.Remove(lostDeviceIndex[0]);
            return;
        }

        // プレイヤー数が最大数に達していたら、処理を終了
        if (currentPlayerCount >= maxPlayerCount)
        {
            return;
        }

        // Join要求元のデバイスが既に参加済みのとき、処理を終了
        foreach (var device in joinedDevices)
        {
            if (context.control.device == device)
            {
                return;
            }
        }

        playerInformation[currentPlayerCount] =
            new PlayerInformation(context.control.device);

        // Joinしたデバイス情報を保存
        joinedDevices[currentPlayerCount] = context.control.device;

        currentPlayerCount++;
    }

    private int ExitDeviceIndex(InputDevice device) {

        int index = 0;

        foreach (var d_ in joinedDevices) {

            if (d_ == device) {

                joinedDevices[index] = null;
                return index;
            }

            index++;
        }

        return index;
    }

    // 継承先使用可能

    protected PlayerParent2 CreatePlayer(PlayerInformation playerInformation,Vector3 p,Quaternion q) {

        GameObject prefab =
            Resources.Load<GameObject>(SetPlayerPrefab(0));

        PlayerParent2 pp =
        PlayerParent2.CreatePlayer(
            prefab: prefab,
            type: playerScript,
            playerIndex: (int)playerInformation.SelectedCharacter,
            device: playerInformation.PairWithDevice,
            position:p,
            roatation:q
        );

        return pp;
    }

    // 参照可能メソッド

    public void SetPlayerInformation(PlayerInformation[] p_) {

        playerInformation = p_;
        currentPlayerCount = p_.Length;
    }
}
