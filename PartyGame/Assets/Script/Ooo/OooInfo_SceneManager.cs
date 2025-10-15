using System;
using UnityEngine;

public class OooInfo_SceneManager :InGameManeger
{
    private bool playerFlag = default;

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
        playerFlag = false;
    }
    protected override async void Update()
    {
        base.Update();

        if (playerInformation[0] == null)
        {
            return;
        }

        // 呼び出し
        if (!playerFlag)
        {
            Vector3 vec = new Vector3(-10000, 0, 0);

            Quaternion quat = Quaternion.identity;

            for (int i = 0; i < GameInformation.MAX_PLAYER_VALUE; i++)
            {
                player[i] = CreatePlayer(
                    playerInformation: playerInformation[i],
                    p: vec,
                    q: quat,
                    index: i + 1
                    );
            }
            playerFlag = true;
        }

        // ボタンが押されたら次のシーンへ
        if (Input.anyKey)
        {

            await NextScene();
        }

    }

    public override string SceneName => GameInformation.OOO_GAME;
    public override void OnUnLoaded() { }

}
