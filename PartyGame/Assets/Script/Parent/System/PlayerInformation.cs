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
public class PlayerInformation
{
    public      InputDevice     PairWithDevice      { get; private set; } = default;
    public      CharacterType   SelectedCharacter   { get; private set; } = default;
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
