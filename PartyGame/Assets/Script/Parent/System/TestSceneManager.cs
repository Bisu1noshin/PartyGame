using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestSceneManager : SceneManagerParent
{
    protected override Type PlayerType()
    {
        return typeof(TestPlayer);
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
