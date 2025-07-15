using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Onishi_TestSceneManager : InGameManeger
{
    bool playable = false;
    protected override Type SetPlayerScript()
    {
        return typeof(Onishi_TestPlayer);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override string SetPlayerPrefab(int index)
    {
        string str =
            "Player/Test/Cube_" + index.ToString();

        return str;
    }
}
