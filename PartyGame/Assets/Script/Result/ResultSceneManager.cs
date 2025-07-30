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
    }
    protected override void Update()
    {
        base.Update();

        if (Input.anyKey)
        {

            SceneManager.LoadScene("SetPlayerScene");
        }

    }

    public override string SceneName => "LoadScene";
    public override void OnUnLoaded() { }
}
