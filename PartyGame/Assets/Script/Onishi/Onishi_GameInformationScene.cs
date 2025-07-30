using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

//---------------------------------------------------------
// クラスの名前を変更して実装する
// SceneNameを自分のゲームシーンにする
// 必要な要素を追記する
//---------------------------------------------------------

public class Onishi_GameInformationScene : InGameManeger
{
    private GameInformationPlayer[] _player = default;　// playerの派生クラス

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

            for (int i = 0; i < length; i++)
            {
                Vector3 pos = new Vector3(-10000, 0, 0);// 画面外に飛ばす
                player[i] = CreatePlayer(playerInformation[i], pos, Quaternion.identity);

                // playerの派生クラスの取得
                _player[i] = player[i].gameObject.GetComponent<GameInformationPlayer>();
            }
        }
    }
    protected override void Update()
    {
        base.Update();

    }

    public override string SceneName => "OnishiTestScene";// <-変更する!!
    public override void OnUnLoaded() { }

    /// <summary>
    /// 全員のフラグが立っているか確認するメソッド
    /// </summary>
    /// <returns></returns>
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
