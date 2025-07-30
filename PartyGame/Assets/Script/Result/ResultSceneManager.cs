using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class ResultSceneManager : InGameManeger
{
    [SerializeField] TextMeshProUGUI[] Rank;
    private GameInformationPlayer[] _player = default;　// playerの派生クラス

    protected override string SetPlayerPrefab(int index)
    {
        string playerPrefabPath = "Player/VRM/VRM_" + index.ToString();
        return playerPrefabPath;
    }

    protected override Type SetPlayerScript()
    {
        return typeof(GameInformationPlayer);
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
            _player = new GameInformationPlayer[length];

            int[] score= new int[length];

            for (int i = 0; i < length; i++)
            {
                Vector3 pos = new Vector3(-10000, 0, 0);// 画面外に飛ばす
                player[i] = CreatePlayer(playerInformation[i], pos, Quaternion.identity);

                // playerの派生クラスの取得
                _player[i] = player[i].gameObject.GetComponent<GameInformationPlayer>();
                score[i] = playerInformation[i].PlayerScore;
            }

            int[] rank = new int[4] { -1, -1, -1, -1 };
            for (int i = 0; i < length; ++i)
            {
                int maxCnt = score.Max(); //最大の点数を取得
                int maxPl = Array.IndexOf(score, maxCnt); //最大点を取ったPlayerの番号を取得
                rank[i] = maxPl;
            }

            Vector3[] lastpos = new Vector3[4] { new Vector3(-7, -2, 0), new Vector3(-3, -2, 0), new Vector3(1, -2, 0), new Vector3(5, -2, 0) };
            for(int i = 0; i < length; ++i)
            {
                player[rank[i]].transform.position =lastpos[i];
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

        if (!GetAllDecide()) {
            return;
        }



        if (GetAllDecide())
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
    private bool GetAllDecide()
    {

        bool flag = false;

        foreach (var player in _player)
        {

            // 一人でもふらぐがたっていなければはじく
            if (!player.GetDecide()) { return flag; }
        }

        flag = true;

        foreach (var player in _player)
        {
            // 全員のフラグをおろす
            player.SetDecideToFlase();
        }

        return flag;
    }
}
