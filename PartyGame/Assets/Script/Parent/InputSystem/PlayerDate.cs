using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerDate
{
    //
    private int playerNumber;
    private int playerFBXId;
    private int playerScore;
    private InputDevice device;

    private static class PlayerDateConstNum {

        public const int playerFBXLength = 1;
    }

    /// <summary>
    /// Playerの情報を登録するときに呼び出す
    /// </summary>
    /// <param name="num"></param>
    /// <param name="Id"></param>
    public PlayerDate(int num, InputDevice d_, int FBXId = 0)
    {

        playerNumber = num;
        playerFBXId = FBXId;
        playerScore = 0;
        device = d_;
    }

    public PlayerDate SetPlayerFBXId(int FBXId)
    {

        PlayerDate data = this;

        if (FBXId > PlayerDateConstNum.playerFBXLength || FBXId <= 0) {

            Debug.Log("FbxIdが不正な値を指定しています");
            return data;
        }

        data.playerFBXId = FBXId;

        return data;
    }

    public PlayerDate AddPlayerScore(int score) {

        PlayerDate data = this;

        data.playerScore += score;
        Debug.Log("PlayerNumber:" + data.playerNumber + ",Score->" + data.playerScore);

        return data;
    }

    public bool JudgeInputControl(InputDevice input){

        if (this == null) { return false; }

        if (device == input)
        {
            return true;
        }

        return false;
    }

    public InputDevice GetDevice() {

        return device;
    }

    public int GetUserValue() {

        return this.playerNumber;
    }

    public int GetFbxId() {

        return this.playerFBXId;
    }
}
