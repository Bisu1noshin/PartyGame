using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    public PlayerData[] Player = new PlayerData[playerLenge];
    private List<PlayerData> playerDatas;

    private PartyGame inputAction;
    private const int playerLenge = 2;

    private bool OnScene;

    private void Awake()
    {
        inputAction = new PartyGame();

        inputAction.Test.Test.started += OnTestInput;
        inputAction.Test.Device.started += OnDevice;

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

                int user = i + 1;
                playerDatas.Add(new PlayerData(user, device));

                Debug.Log("入室しました player:" + user);
                return;
            }

            Debug.Log("playerの数が最大値です");
        }
    }

    public void OnDevice(InputAction.CallbackContext context) {

        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        for (int i = 0; i < Player.Length; i++)
        {
            if (Player[i] == null) { continue; }

            if (Player[i].JudgeInputControl(device))
            {
                Debug.Log("コントローラーを認識 player:" + (i + 1));
            }
        }

        OnScene = true;
    }

    private void Update()
    {
        if (OnScene) {


            PlayerDataContllore.PlayerDataContllore_instance.InitializePlayerDatas(playerDatas);
            SceneManager.LoadScene("TestGame");
        }
    }
}
