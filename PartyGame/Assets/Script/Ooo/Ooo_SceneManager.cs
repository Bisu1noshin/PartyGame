using System;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.Linq;
using Cysharp.Threading.Tasks.Triggers;

public class Ooo_SceneManager : InGameManeger
{
    const int PLAYER_CNT = 4;   //最大プレイヤーは4人
    enum GameStatus
    {
        standby,    //スタンバイ 始まる前
        play,       //インゲーム プレイ中
        finish,     //フィニッシュ ゲーム終了
        non         //それ以外 基本的に使われない
    };

    private GameStatus status; //ゲームステータス管理
    float countTimer = 4f; //タイマー ゲーム時間で初期化する(秒)
    float endTimer = 1.5f;
    float timer = 40f;
    bool playerFlag = false;
    public static int[] playerScore = new int[PLAYER_CNT]; //各プレイヤー点数保存
    public static int[] playerEscape = new int[PLAYER_CNT];

    [SerializeField] GameObject StartText; //Startの文字のPrefab
    [SerializeField] GameObject FinishText; //Finishの文字のPrefab
    [SerializeField] GameObject Canvas; //キャンバス(文字のPrefabを表示するのに必要)
    [SerializeField] TMP_Text text_Timer; //タイマーを表示するText
    [SerializeField] TMP_Text[] scoreText = new TMP_Text[PLAYER_CNT]; //プレイヤースコアText
    [SerializeField] TMP_Text[] escapeMashText = new TMP_Text[PLAYER_CNT]; // Bボタン連打テキスト
    private string text;

    protected override Type SetPlayerScript()
    {
        return typeof(Ooo_TestPlayer);
    }

    private void Start()
    {
        playerInformation = new PlayerInformation[PLAYER_CNT];
        status = GameStatus.standby;
        
        //プレイヤースコア0で初期化
        for (int i = 0; i < PLAYER_CNT; i++)
        {
            playerScore[i] = 0;
        }
    }


    protected override async void Update()
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
            Vector3[] vec = new Vector3[]
            {
                new Vector3 (-1, 0, 0),
                new Vector3 (1, 0, 0),
                new Vector3 (-1, 0, -1),
                new Vector3 (1, 0, -1)
            };

            Quaternion quat = Quaternion.identity;

            for (int i = 0; i < PLAYER_CNT; i++)
            {
                player[i] = CreatePlayer(
                    playerInformation: playerInformation[i],
                    p: vec[i],
                    q: quat,
                    index: i+1
                    );
            }
            playerFlag = true;
        }

        if (status == GameStatus.standby)
        {            
            if (countTimer == 4f)
            {
                //「Start」の文字を召喚
                GameObject go = Instantiate(StartText);
                go.transform.SetParent(Canvas.transform);
                go.transform.position = new Vector3(1000, 600, 0);  
            }
            countTimer -= Time.deltaTime;

            //Statusを変更
            if (countTimer <= 0f)
            {
                countTimer = 0f;            //カウントText終わったっら
                status = GameStatus.play;   //timer開始
            }
        }

        //---------------インゲーム処理---------------
        if (status == GameStatus.play)
        {
            timer -= Time.deltaTime;
            text_Timer.text = "Time: " + timer.ToString("F0");

            for (int i = 0; i < PLAYER_CNT; i++)
            {
                if (scoreText[i] != null)
                {
                    scoreText[i].text = "P" + (i + 1) + "  score: " + playerScore[i];

                    //Trap状態なら連打Text表示
                    if (player[i] is Ooo_TestPlayer testPlayer && testPlayer.isTrapped)
                    {
                        scoreText[i].text += "\nYou are Trapped!\nBボタンおして! " + testPlayer.nowEscapeClick + "/10";
                    }
                }
            }
            //------------------------------------------------


            if (timer <= 0f)
            {
                timer = 0;

                //---------------順位処理---------------
                int[] val = new int[4] { playerScore[0], playerScore[1], playerScore[2], playerScore[3] };
                for (int i = 0; i < PLAYER_CNT; ++i)
                {
                    int maxCnt = playerScore.Max();                 //最大の点数を取得
                    int maxPl = Array.IndexOf(playerScore, maxCnt); //最大点を取ったPlayerの番号を取得
                    int rank = i + 1;                               //被りなしの場合の順位
                    for (int j = 0; j < i; ++j)
                    {
                        if (val[j] == maxCnt)   //過去の点数と同じなら
                        {
                            rank = j + 1;       //同順位に更新
                            break;
                        }
                    }
                    playerInformation[maxPl].AddPlayerScore(rank);
                    playerScore[maxPl] = playerScore.Max();    //該当者の得点をリセット
                    val[i] = maxCnt;            //同順位判定用のものをセット
                }
                //------------------------------------------------

                //「Finish」の文字を召喚
                if (endTimer == 1.5f)
                {
                    GameObject go = Instantiate(FinishText);
                    go.transform.SetParent(Canvas.transform);
                    go.transform.position = new Vector3(1000, 600, 0);
                }
                endTimer -= Time.deltaTime;

                //Statusを変更
                if (endTimer <= 0f)
                {
                    endTimer = 0f;
                    status = GameStatus.finish;
                }
            }
        }

        //Statusを変更
        if (status == GameStatus.finish)
        {
            await NextScene();
        }

        //ESCでゲーム終了
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

        //Score処理
        public static void AddScore(int playerIndex)
        { 
    
            if(playerIndex >= 0 && playerIndex < PLAYER_CNT)
            {
            playerScore[playerIndex]++;
            }
        }


    protected override string SetPlayerPrefab(int index)
    {
        string str =
            "Player/VRM/VRM_" + index.ToString();

        return str;
    }

    public override string SceneName => "TitleScene";

    
    public override void OnUnLoaded() { }

}
