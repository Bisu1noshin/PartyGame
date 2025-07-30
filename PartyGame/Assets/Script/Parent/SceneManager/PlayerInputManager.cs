using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : InGameManeger
{
    protected override string SetPlayerPrefab(int index)
    {
        string playerPrefabPath = "Player/VRM/VRM_" + index.ToString();
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

        if (playerInformation[0] == null) {
            return;
        }

        if(player[0] == null)
        {
            PlayerInformation p = playerInformation[0];
            Vector3 vector = Vector3.zero;
            player[0] = CreatePlayer(p, vector, Quaternion.identity, 1);
        }
        

        if (Input.anyKey) {

            //await NextScene();
        }

    }

    public override string SceneName => "LoadScene";
    public override void OnUnLoaded() { }
}
