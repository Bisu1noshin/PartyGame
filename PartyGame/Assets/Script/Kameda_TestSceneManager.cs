using System;
using System.Collections.Generic;
using UnityEngine;

public class Kameda_TestSceneManager : SceneManagerParent
{
    enum GameState { Title, Introsuction, GameMain, Finish, Result };
    GameState gameState;
    bool PlayersNotExist;
    private void Start()
    {
        gameState = GameState.Title;
        PlayersNotExist = true;
    }
    
    protected override Type PlayerType()
    {
        return typeof(Kameda_TestSceneManager);
    }
    protected override void UnityUpdate()
    {
        switch (gameState)
        {
            case GameState.Title:
                if(PlayersNotExist)
                {
                    List<PlayerData> pds = new List<PlayerData>();
                    pds = PlayerDataContllore.PlayerDataContllore_instance.GetPlayerDate();
                    int index = 0;
                    Vector3 originpos = new Vector3(-3, 0, 4);
                    foreach (PlayerData playerData in pds)
                    {
                        player[index] = CreatePlayer(playerData, originpos, Quaternion.Euler(0, -90, 0));
                        index++;
                        originpos.x += 2.0f;
                    }
                    PlayersNotExist = false;
                }
                
                break;
            case GameState.Introsuction:
                break;
                case GameState.GameMain:
                break;
            case GameState.Finish:
                break;
            case GameState.Result:
                break;
            default:
                break;
        }
    }

    protected override string PlayerFilePath(int index)
    {
        string str =
            "Player/Test/Cube_" + index.ToString();

        return str;
    }
}
