using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Ooo_SceneManager : InGameManeger
{
    const int PLAYER_CNT = 2;   //最大プレイヤーは4人
    enum GameStatus
    {
        standby,    //スタンバイ 始まる前
        play,       //インゲーム プレイ中
        finish,     //フィニッシュ ゲーム終了
        non         //それ以外 基本的に使われない
    };

    private GameStatus status; //ゲームステータス管理
    float timer = 20f; //タイマー ゲーム時間で初期化する(秒)
    bool playerFlag = false;
    private int[] playerScore = new int[PLAYER_CNT]; //各プレイヤー点数保存



    [SerializeField] GameObject StartText; //Startの文字のPrefab
    [SerializeField] GameObject FinishText; //Finishの文字のPrefab
    [SerializeField] GameObject Canvas; //キャンバス(文字のPrefabを表示するのに必要)
    [SerializeField] TMP_Text text_Timer; //タイマーを表示するText
    [SerializeField] TMP_Text[] scoreText = new TMP_Text[PLAYER_CNT]; //プレイヤースコアText
    


    protected override Type SetPlayerScript()
    {
        return typeof(Ooo_TestPlayer);
    }

    private void Start()
    {
        //playerInformation = new PlayerInformation[PLAYER_CNT];
        status = GameStatus.standby;

        //プレイヤースコア0で初期化
        for(int i = 0; i < PLAYER_CNT; i++)
        {
            playerScore[i] = 0;
        }
    }

    protected  override async void Update()
    {
        base.Update();
        for (int i = 0; i < PLAYER_CNT; i++)
        {
            if (playerInformation[i] == null)
            {
                return;
            }
        }

        // 呼び出し
        if (!playerFlag)
        {
            Vector3 vec = Vector3.zero;
            Quaternion quat = Quaternion.identity;


            for (int i = 0; i < PLAYER_CNT; i++)
            {
                player[i] = CreatePlayer(
                    playerInformation: playerInformation[i],
                    p: vec,
                    q: quat
                    );
            }

            playerFlag = true;
        }

        if (status == GameStatus.standby)
        {
            //「Start」の文字を召喚
            GameObject go = Instantiate(StartText);
            go.transform.SetParent(Canvas.transform);
            go.transform.position = new Vector3(600, 400, 0);

            //Statusを変更
            status = GameStatus.play;
        }

        //インゲーム処理
        if (status == GameStatus.play)
        {
            timer -= Time.deltaTime;
            text_Timer.text = timer.ToString("F0");

            for (int i = 0; i < PLAYER_CNT; i++)
            {
                if (scoreText[i] != null)
                {
                    scoreText[i].text = "P" + (i + 1) + "\nscore: " + playerScore[i];
                }
            }

            //ゲーム終了時処理
            if (timer <= 0f)
            {
                timer = 0;

                //「Finish」の文字を召喚
                GameObject go = Instantiate(FinishText);
                go.transform.SetParent(Canvas.transform);
                go.transform.position = new Vector3(600, 400, 0);

                //Statusを変更
                status = GameStatus.finish;
            }
        }

        // 追記
        {

            if (status == GameStatus.finish) {

                await NextScene();
            }
        }

    }

    //Score管理関数
    public void AddScore(int playerIndex)
    {
        if(playerIndex >= 0 && playerIndex < PLAYER_CNT)
        {
            playerScore[playerIndex]++;
        }
    }

    









    protected override string SetPlayerPrefab(int index)
    {
        string str =
            "Player/Test/Cube_" + index.ToString();

        return str;
    }

    public override string SceneName => GameInformation.LoadScene;

    public override void OnUnLoaded()
    {
        Debug.Log("Exit_Ooo");
    }
}
