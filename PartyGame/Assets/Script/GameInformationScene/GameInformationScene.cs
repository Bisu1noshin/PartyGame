using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInformationScene :InGameManeger
{
    protected override string SetPlayerPrefab(int index)
    {
        string playerPrefabPath = "Player/Test/Cube_" + index.ToString();
        return playerPrefabPath;
    }

    protected override Type SetPlayerScript()
    {
        return typeof(GameInformationPlayer);
    }

    private void Start()
    {
        if (playerInformation == null)
        {
            Debug.LogError("playerの情報がnullです。");
            return;
        }

        // playerの召喚
        {
            int length = playerInformation.Length;

            for (int i = 0; i < length; i++)
            {

                Vector3 pos = new Vector3(-10000, 0, 0);// 画面外に飛ばす
                player[i] = CreatePlayer(playerInformation[i], pos, Quaternion.identity);
            }
        }
    }
    protected override void Update()
    {
        base.Update();

    }

    public override string SceneName => "LoadScene";

    public override void OnLoaded(PlayerInformation[] data)
    {

        if (data is null || data is not PlayerInformation[] playerInformation)
        {
            Debug.LogError("data is null");
            return;
        }

        // presenterを取得して、Presenter側の初期化メソッドを実行して、シーン全体を動かす
        var presenter = UnityEngine.Object.FindAnyObjectByType<InGameManeger>();
        presenter.SetPlayerInformation(playerInformation);
    }
    public override void OnUnLoaded() { }
}
