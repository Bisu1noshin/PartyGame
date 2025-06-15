using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    public PlayerData[] Player = new PlayerData[playerLenge];

    private PartyGame inputAction;
    private const int playerLenge = 2;

    private void Awake()
    {
        inputAction = new PartyGame();

        inputAction.Test.Test.started += OnTestInput;

        inputAction.Enable();
    }

    // 入力イベント

    public void OnTestInput(InputAction.CallbackContext context)
    {
        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        // ログ出力
        print($"Control Path: {control.path}, Device: {device}, Control: {control}");

        // プレイヤーを登録
        for (int i = 0; i < Player.Length; i++) {

            if (Player[i] == null) {

                Player[i] = new PlayerData(i + 1, device);

                int user = i + 1;
                Debug.Log("入室しました player:" + user);
                return;
            }

            Debug.Log("playerの数が最大値です");
        }
    }

    private void Update()
    {
        
    }
}
