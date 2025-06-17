using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerData
{
    //
    private int playerNumber;
    private int playerFBXId;
    private int playerScore;
    private InputControl device;

    private static class PlayerDateConstNum {

        public const int playerFBXLengh = 1;
    }

    /// <summary>
    /// Playreの情報を登録するときに呼び出す
    /// </summary>
    /// <param name="num"></param>
    /// <param name="Id"></param>
    public PlayerData(int num, InputDevice d_, int FBXId = 0)
    {

        playerNumber = num;
        playerFBXId = FBXId;
        playerScore = 0;
        device = d_;
    }

    public PlayerData SetPlayerFBXId(int FBXId)
    {

        PlayerData data = this;

        if (FBXId > PlayerDateConstNum.playerFBXLengh || FBXId <= 0) {

            Debug.Log("FbxIdが不正な値を指定しています");
            return data;
        }

        data.playerFBXId = FBXId;

        return data;
    }

    public PlayerData AddPlayerScore(int score) {

        PlayerData data = this;

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

    public int GetUserValue() {

        return this.playerNumber;
    }

    public static List<PlayerData> DebugData(int playerLengh) {

        List<PlayerData> datas = new List<PlayerData>();

        if (playerLengh > 3) { return datas; }

        for (int i = 0; i < playerLengh; i++) {

            //Input.GetJoystickNames();
            //datas.Add(new PlayerData());
        }

        return datas;
    }
}
