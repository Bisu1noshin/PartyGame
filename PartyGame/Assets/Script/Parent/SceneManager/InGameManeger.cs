using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InGameManeger : MonoBehaviour
{
    [SerializeField] private bool DebagMode = default;
    [SerializeField] private int maxPlayerCount = default;

    protected PlayerInformation[] playerInformation = default;
    protected PlayerParent2[] player;

    private     Type            playerScript;
    private     InputAction     playerJoinInputAction;
    private     InputDevice[]   joinedDevices = default;
    private     int             currentPlayerCount = 0;
    private     bool            createPlayerFlag = false;


    // コントローラーが抜けたときの処理
    // プレイヤーを呼び出す関数b
    // データが無い時の例外処理、デバッグ用の処理

    protected virtual void Awake()
    {
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
        }

        playerScript = SetPlayerScript();
        joinedDevices = new InputDevice[maxPlayerCount];
        player = new PlayerParent2[maxPlayerCount];
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
            DebagMode = false;
        }
#endif


    }

    // 抽象メソッド

    protected abstract string SetPlayerPrefab();
    protected abstract Type SetPlayerScript();

    // メソッド

    private void OnJoin(InputAction.CallbackContext context)
    {
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

        Vector3 vector3 = Vector3.zero;
        Quaternion quat = Quaternion.identity;
        GameObject prefab =
            Resources.Load<GameObject>(SetPlayerPrefab());

        // PlayerInputを所持した仮想のプレイヤーをインスタンス化
        // ※Join要求元のデバイス情報を紐づけてインスタンスを生成する
        PlayerParent2.CreatePlayer(
            prefab,
            playerScript,
            context.control.device,
            currentPlayerCount,
            vector3,
            quat
            );

        // Joinしたデバイス情報を保存
        joinedDevices[currentPlayerCount] = context.control.device;

        currentPlayerCount++;
    }

    private void ExitDeviceIndex(InputDevice device) {

        int index = 0;
        foreach (var d_ in joinedDevices) {

            if (d_ == device) return;
            index++;
        }
    }

    // 継承先使用可能

    protected PlayerParent2 CreatePlayer(PlayerInformation playerInformation,Vector3 p,Quaternion q) {

        GameObject prefab =
            Resources.Load<GameObject>(SetPlayerPrefab());

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
