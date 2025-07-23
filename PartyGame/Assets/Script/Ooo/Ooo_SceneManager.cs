using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Ooo_SceneManager : InGameManeger
{
    protected override Type SetPlayerScript()
    {
        return typeof(Ooo_TestPlayer);
    }

    protected override void Update()
    {

    }

    protected override string SetPlayerPrefab(int index)
    {
        string str =
            "Player/Test/Cube_" + index.ToString();

        return str;
    }

    public override string SceneName => "TitleScene";

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

        SSceneManager.LoadScene<Ooo_SceneManager>(playerInformation).Forget();
    }
}
