using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class Onishi_TestSceneManager : InGameManeger
{
    const int PLAYER_CNT = 4; //最終的に4にする

    enum GameStatus
    {
        standby,    //スタンバイ 始まる前
        play,       //インゲーム プレイ中
        finish,     //フィニッシュ ゲーム終了
        non         //それ以外 基本的に使われない
    };

    GameStatus status; //ゲームステータス管理
    float timer = 5f; //タイマー ゲーム時間で初期化する(秒)

    bool playerFlag = false; //プレイヤーの存在フラグ

    [SerializeField] GameObject StartText; //Startの文字のPrefab
    [SerializeField] GameObject FinishText; //Finishの文字のPrefab
    [SerializeField] GameObject Canvas; //キャンバス(文字のPrefabを表示するのに必要)
    [SerializeField] TMP_Text text_Timer; //タイマーを表示するText

    private int[] bombCnt = new int[PLAYER_CNT];
    private bool finishcnt=false;

    protected override Type SetPlayerScript()
    {
        return typeof(Onishi_TestPlayer);
    }

    private void Start()
    {
        playerInformation = new PlayerInformation[PLAYER_CNT];
        status = GameStatus.standby;
    }

    protected override void Update()
    {
        base.Update();

        //Escapeでデバッグモードを抜けるだけ
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif

        for (int i=0; i<PLAYER_CNT; i++)
        {
            if (playerInformation[i] == null) {
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
                    playerInformation : playerInformation[i],
                    p : vec,
                    q : quat
                    );
            }

            playerFlag = true;
        }

        //ここまでくると開始可能となる
        //ゲーム開始時処理
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

        if (status == GameStatus.finish)
        {
            if (finishcnt) {
                status = GameStatus.non;
                return;
            }
            int[] val = bombCnt;
            Array.Sort(val);
            for (int i = 0; i < PLAYER_CNT; i++) 
            {
                for (int j = 3; j > 0; j--) 
                {
                    if (bombCnt[i] == val[j])
                    {
                        playerInformation[i].AddPlayerScore(4 - j);
                        Debug.Log("player" + i.ToString() + "は順位" + (4 - j).ToString());
                        break;
                    }
                }
            }
            finishcnt = true;
        }
    }

    //プレイ中か返す関数
    public bool isPlaying()
    {
        if (status == GameStatus.play) return true;
        else return false;
    }

    //ゲーム終了したか返す関数
    public bool isFinish()
    {
        if(status== GameStatus.finish) return true;
        else return false;
    }

    //プレイヤーの爆弾個数を受け取る関数
    public void getBombCnt(int pl_, int bombCnt_)
    {
        bombCnt[pl_] = bombCnt_;
    }

    //以下、必要がなければ触らない----------------------------------------------

    protected override string SetPlayerPrefab(int index)
    {
        string str =
            "Player/Test/Cube_" + index.ToString();

        return str;
    }

    public override string SceneName => "TitleScene";

    public override void OnLoaded(PlayerInformation[] data)
    {

        if (data is null || data is not PlayerInformation[] playerInformation)
        {
            Debug.LogError("data is null");
            return;
        }

        // presenterを取得して、Presenter側の初期化メソッドを実行して、シーン全体を動かす
        var presenter = UnityEngine.Object.FindAnyObjectByType<InGameManeger>();
        presenter.SetPlayerInformation(playerInformation);
    }
    public override void OnUnLoaded() { }

    protected override void NextSceneJump()
    {
        SSceneManager.LoadScene<Onishi_TestSceneManager>(playerInformation).Forget();
    }
}
