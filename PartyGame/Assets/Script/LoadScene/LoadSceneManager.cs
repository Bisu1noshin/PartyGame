using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

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

    protected override void Awake()
    {
        base.Awake();
    }

    private async void Start()
    {
        await NextScene();
    }

    public override string SceneName => GameInformation.GameScenes[GameSceneIndex];

    public override void OnUnLoaded()
    {
        Debug.Log("Exit_load  index:" + GameSceneIndex);
    }

    private void GameSceneAdd() {

        Debug.Log("AddInadex");

        if (GameSceneIndex >= 3)
        {
            GameSceneIndex = 0;
            return;
        }

        GameSceneIndex++;
    }

    private new async Task NextScene() {

        var presenter =
            await SSceneManager.LoadScene<InGameManeger>(playerInformation, SceneLeftimeManager);
        if (presenter == null) { Debug.LogError("NULL!!");return; }
        presenter.SetPlayerInformation(playerInformation);
        GameSceneAdd();
    }
}
