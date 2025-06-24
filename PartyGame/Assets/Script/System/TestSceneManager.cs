using NUnit.Framework;
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
}
