using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SetPlayerSceneManager : InGameManeger
{
    [SerializeField]
    private TextMeshProUGUI[] pushA_UIs; 

    private bool[] instanceFlag = default;
    private bool[] decideFlag = default;
    private int decideCnt = default;
    private Vector3[] playerPos = new Vector3[GameInformation.MAX_PLAYER_VALUE] {

        new Vector3(-10000,0,0),
        new Vector3(-10000,0,0),
        new Vector3(-10000,0,0),
        new Vector3(-10000,0,0)
    };

    protected override string SetPlayerPrefab(int index)
    {
        string playerPrefabPath = "Player/Test/Cube_" + index.ToString();
        return playerPrefabPath;
    }

    protected override Type SetPlayerScript()
    {
        return typeof(SetPlayerScenePlayerContllore);
    }

    private void Start()
    {
        instanceFlag = new bool[GameInformation.MAX_PLAYER_VALUE];
        decideFlag = new bool[GameInformation.MAX_PLAYER_VALUE];

        for (int i = 0; i < instanceFlag.Length; i++) {
            instanceFlag[i] = true;
            decideFlag[i]   = false;
        }
    }

    protected override void Update()
    {
        base.Update();

        for (int i = 0; i < pushA_UIs.Length; i++) {

            GamingColor(pushA_UIs[i]);
        }

        for (int i = 0; i < GameInformation.MAX_PLAYER_VALUE; i++)
        {
            if (instanceFlag[i] && playerInformation[i] != null)
            {
                instanceFlag[i] = false;

                Vector3 v = playerPos[i];
                Quaternion q = Quaternion.identity;
                player[i] = CreatePlayer(playerInformation[i], v, q);

                pushA_UIs[i].enabled = false;

                Debug.Log("player" + (i + 1) + "が追加されました");
            }
        }

        for (int i = 0; i < GameInformation.MAX_PLAYER_VALUE; i++)
        {
            if (!decideFlag[i] || playerInformation[i].playerFBXPath == null)
            {
                return;
            }
        }

        NextSceneJump();
    }

    public override string SceneName => GameInformation.LoadScene;

    public override void OnLoaded(PlayerInformation[] data)
    {

        if (data is null || data is not PlayerInformation[] playerInformation)
        {
            Debug.LogError("data is null");
            return;
        }

        GameInformation.RandomGameScene();

        // presenterを取得して、Presenter側の初期化メソッドを実行して、シーン全体を動かす
        var presenter = UnityEngine.Object.FindAnyObjectByType<InGameManeger>();
        presenter.SetPlayerInformation(playerInformation);
    }
    public override void OnUnLoaded() {
    }

    protected override void NextSceneJump()
    {

        SSceneManager.LoadScene<SetPlayerSceneManager>(playerInformation).Forget();
    }

    public void SetPlayerInformation(PlayerInformation data ,int index) {

        playerInformation[index] += data;
        decideFlag[index] = true;
    }

    private void GamingColor(MaskableGraphic ui)
    {
        float addValue = 1f / 256f * 16f;
        float maxValue = 1f;

        float r = ui.color.r;
        float g = ui.color.g;
        float b = ui.color.b;

        if (r == maxValue && g == 0)
        {

            b += addValue;
        }

        if (g == 0 && b == maxValue)
        {
            r -= addValue;
        }

        if (r == 0 && b == maxValue)
        {
            g += addValue;
        }

        if (r == 0 && g == maxValue)
        {
            b -= addValue;
        }

        if (b == 0 && g == maxValue)
        {

            r += addValue;
        }

        if (b == 0 && r == maxValue)
        {
            g -= addValue;
        }

        ui.color = new Color(r, g, b);
    }
}
