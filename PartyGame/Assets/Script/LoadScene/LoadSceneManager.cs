using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class LoadSceneManager : InGameManeger
{
    private static int GameSceneIndex = 0;

    protected override string SetPlayerPrefab(int index)
    {
        string playerPrefabPath = "Player/Test/Cube_" + index.ToString();
        return playerPrefabPath;
    }

    protected override Type SetPlayerScript()
    {
        return typeof(TestPlayer);
    }

    protected override void Update()
    {
        base.Update();

        NextSceneJump();
    }

    public override string SceneName => NextRandGame();

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
    public override void OnUnLoaded() {

        if (GameSceneIndex>=3) {
            GameSceneIndex = 0;
            return;
        }

        GameSceneIndex++;
    }

    protected override void NextSceneJump()
    {

        SSceneManager.LoadScene<LoadSceneManager>(playerInformation).Forget();
    }

    private string NextRandGame() {

        string sceneName = GameInformation.GameScenes[GameSceneIndex];

        return "TestGame";
    }
}
