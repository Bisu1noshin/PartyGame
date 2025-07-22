using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetPlayerSceneManager : InGameManeger
{
    private bool[] instanceFlag = default;
    private bool[] decideFlag = default;
    private int decideCnt = default;

    protected override string SetPlayerPrefab(int index)
    {
        string playerPrefabPath = "Player/Test/Cube_" + index.ToString();
        return playerPrefabPath;
    }

    protected override Type SetPlayerScript()
    {
        return typeof(TestPlayer);
    }

    private void Start()
    {
        instanceFlag = new bool[GameInformation.MAX_PLAYER_VALUE];
        decideFlag = new bool[GameInformation.MAX_PLAYER_VALUE];
        for (int i = 0; i < instanceFlag.Length; i++) {
            instanceFlag[i] = true;
            decideFlag[i] = false;
        }
    }

    protected override void Update()
    {
        base.Update();

        for (int i = 0; i < GameInformation.MAX_PLAYER_VALUE; i++)
        {
            if (instanceFlag[i] && playerInformation[i] != null)
            {
                instanceFlag[i] = false;

                Vector3 v = Vector3.zero;
                Quaternion q = Quaternion.identity;
                player[i] = CreatePlayer(playerInformation[i], v, q);
            }
        }

        for (int i = 0; i < GameInformation.MAX_PLAYER_VALUE; i++)
        {
            if (!decideFlag[i] || playerInformation[i] == null)
            {
                return;
            }
        }

        NextSceneJump();
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

    protected override void NextSceneJump()
    {

        SSceneManager.LoadScene<SetPlayerSceneManager>(playerInformation).Forget();
    }

    public void SetPlayerInformation(PlayerInformation data ,int index) {

        playerInformation[index] = data;
        decideFlag[index] = true;
    }
}
