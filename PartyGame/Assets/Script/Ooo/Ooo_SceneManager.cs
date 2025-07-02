using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Ooo_SceneManager : SceneManagerParent
{
    protected override Type PlayerType()
    {
        return typeof(Ooo_TestPlayer);
    }

    protected override void UnityUpdate()
    {

    }

    protected override string PlayerFilePath(int index)
    {
        string str =
            "Player/Test/Cube_" + index.ToString();

        return str;
    }
}
