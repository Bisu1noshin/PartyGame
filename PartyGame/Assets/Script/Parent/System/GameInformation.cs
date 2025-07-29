using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameInformation
{
    public const int MAX_PLAYER_VALUE = 4;

    public const string LoadScene      =   "LoadScene";
    public const string KAMEDA_GAME    =   "Kameda";
    public const string OOO_GAME       =   "Ooo";
    public const string ONISHI_GAME    =   "OnishiTestScene";

    public static string[] GameScenes = default; 
    public static void RandomGameScene() {

        string[] _GameScenes = new string[3] {

            KAMEDA_GAME,
            OOO_GAME,
            ONISHI_GAME
        };

        //System.Random random = new System.Random();

        //for (int i = 0; i < 10; i++) {

        //    int a = random.Next(0, 2);
        //    int b = random.Next(0, 2);

        //    string temp     = _GameScenes[a];
        //    _GameScenes[a]   = _GameScenes[b];
        //    _GameScenes[b]   = temp;
        //}

        string[] strings = new string[4]
        {
            "ResultScene",
            _GameScenes[0],
            _GameScenes[1],
            _GameScenes[2]
        };

        GameScenes = strings;
    }
}
