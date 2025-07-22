using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public enum CharacterType
{
    Character1,
    Character2,
    Character3,
    Character4,
}
public enum TeamColor {

    RED     =   1,
    BlUE    =   -1
}
public class TeamDivisionTable {

    /// <summary>
    /// indexから適当な割り振りをする
    /// </summary>
    /// <returns></returns>
    public TeamColor[] GetTeamDivisionTable(int index) {

        TeamColor[] result = new TeamColor[GameInformation.MAX_PLAYER_VALUE];

        int     resultNum   = index % 6;
        int     length      = resultNum / 2;
        int     color       = 1 + (-2 * resultNum % 2);

        result[0] = (TeamColor)color;

        for (int i = 1; i < GameInformation.MAX_PLAYER_VALUE; i++) {

            if (length + 1 == i)
            {

                result[i] = (TeamColor)color;
            }
            else {

                result[i] = (TeamColor)(color * -1);
            }        
        }

        return result;
    }
}
public class PlayerInformation
{
    public      InputDevice     PairWithDevice      { get; private set; } = default;
    public      CharacterType   SelectedCharacter   { get; private set; } = default;
    public      TeamColor       TeamColor           { get; private set; } = default;
    public      int             PlayerScore         { get; private set; } = default;
    public      string          playerFBXPath       { get; private set; } = default;

    public PlayerInformation(InputDevice pairWithDevice, int index)
    {
        if (index > 3) {

            throw new ArgumentOutOfRangeException(
                "playerの最大人数を越えています。"
                );
        }

        PairWithDevice      = pairWithDevice;
        SelectedCharacter   = (CharacterType)index;
        PlayerScore         = 0;
    }

    public PlayerInformation(int index,string path) {

        if (index > 3)
        {

            throw new ArgumentOutOfRangeException(
                "playerの最大人数を越えています。"
                );
        }

        PairWithDevice = null;
        SelectedCharacter = (CharacterType)index;
        playerFBXPath = path;
        PlayerScore = 0;
    }

    public static PlayerInformation operator +(PlayerInformation left, PlayerInformation right) {

        PlayerInformation pi = left;

        pi.SelectedCharacter = right.SelectedCharacter;
        pi.playerFBXPath = right.playerFBXPath;

        return pi;
    }

    public void AddPlayerScore(int score) {

        if (score <= 0) {
            throw new ArgumentOutOfRangeException(
                "追加する点数が0以下です。"
                );
        }
        PlayerScore += score;
    }

}
