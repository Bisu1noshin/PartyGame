using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : InGameManeger
{
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
        playerInformation = new PlayerInformation[GameInformation.MAX_PLAYER_VALUE];
        GameInformation.RandomGameScene();
    }
    protected override async void Update()
    {
        base.Update();

        if (playerInformation == null) {
            return;
        }

        PlayerInformation p = playerInformation[0]; 

        for (int i = 0; i < GameInformation.MAX_PLAYER_VALUE; i++) {

            playerInformation[i] = p;
        }

        if (Input.anyKey) {

            await NextScene();
        }

    }

    public override string SceneName => "LoadScene";
    public override void OnUnLoaded() { }
}
