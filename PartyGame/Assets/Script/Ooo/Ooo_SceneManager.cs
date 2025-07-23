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


}
