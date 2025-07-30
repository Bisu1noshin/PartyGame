using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneManager : InGameManeger
{
    [SerializeField] TextMeshProUGUI[] Rank;


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
        // 例外処理
        {
            // プレイヤーの情報がなかった場合
            if (playerInformation == null)
            {
                Debug.LogError("playerの情報がnullです。");
                return;
            }
        }

        // その他初期化処理
        {
            // pass
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

        for (int i = 0; i < Rank.Length; i++)
        {
            GamingColor(Rank[i]);
        }


        if (Input.anyKey)
        {
            SceneManager.LoadScene("TitleScene");
        }

    }

    public override string SceneName => "LoadScene";
    public override void OnUnLoaded() { }
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
