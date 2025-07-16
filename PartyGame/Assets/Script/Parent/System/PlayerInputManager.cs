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
    public override string SceneName => "SceneName";
    public override void OnLoaded(ISceneData data) {

        //if (data is null || data is not BattleSceneData battleData)
        //{
        //    Debug.LogError("data is null");
        //    return;
        //}

        //// presenterを取得して、Presenter側の初期化メソッドを実行して、シーン全体を動かす
        //var presenter = UnityEngine.Object.FindAnyObjectByType<InGameManeger>();
        //presenter.Initialize(battleData);
    }
    public override void OnUnLoaded() { }


}
