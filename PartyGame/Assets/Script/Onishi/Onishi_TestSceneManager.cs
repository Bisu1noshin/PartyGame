using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Linq;

public class Onishi_TestSceneManager : InGameManeger
{
    const int PLAYER_CNT = 4; //最終的に4にする

    enum GameStatus
    {
        standby,    //スタンバイ 始まる前
        play,       //インゲーム プレイ中
        finish,     //フィニッシュ ゲーム終了
        result,     //リザルト 爆弾個数の受け渡しが終わった後
        non         //それ以外 基本的に使われない
    };
  
    GameStatus status; //ゲームステータス管理
    float timer = 10f; //タイマー ゲーム時間で初期化する(秒)

    bool playerFlag = false; //プレイヤーの存在フラグ

    [SerializeField] GameObject StartText; //Startの文字のPrefab
    [SerializeField] GameObject FinishText; //Finishの文字のPrefab
    [SerializeField] GameObject Canvas; //キャンバス(文字のPrefabを表示するのに必要)
    [SerializeField] TMP_Text text_Timer; //タイマーを表示するText

    private int[] bombCnt = new int[PLAYER_CNT];
    private bool start = false;
    private bool finishcnt = false;
    private GameObject go_start = null;
    private GameObject go_finish = null;

    protected override Type SetPlayerScript()
    {
        return typeof(Onishi_TestPlayer);
    }

    private void Start()
    {
        // 追記
        // playerInformationがnullになってた
        // playerInformation = new PlayerInformation[PLAYER_CNT];
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
            Vector3[] vec = new Vector3[4] {
                new Vector3(-10, 0, 10),
                new Vector3(10, 0, 10),
                new Vector3(-10, 0, 0),
                new Vector3(10, 0, 0)
            }; //プレイヤーごとの初期位置
            Quaternion quat = Quaternion.identity;

            for (int i = 0; i < PLAYER_CNT; i++)
            {
                player[i] = CreatePlayer(
                    playerInformation : playerInformation[i],
                    p : vec[i],
                    q : quat
                    );
            }

            playerFlag = true;
        }

        //ここまでくると開始可能となる
        //ゲーム開始時処理
        if (status == GameStatus.standby) 
        {
            if (!start) 
            {
                //「Start」の文字を召喚
                go_start = Instantiate(StartText);
                go_start.transform.SetParent(Canvas.transform);
                go_start.transform.position = new Vector3(600, 320, 0);
                start = true;
            }

            else if (start == true && go_start == null)
            {
                status = GameStatus.play;
            }
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
                go_finish = Instantiate(FinishText);
                go_finish.transform.SetParent(Canvas.transform);
                go_finish.transform.position = new Vector3(600, 400, 0);

                //Statusを変更
                status = GameStatus.finish;
            }
        }

        if (status == GameStatus.finish)
        {
            if (go_finish == null)
            {
                status = GameStatus.result;
            }
        }

        if (status == GameStatus.result)
        {
            if (finishcnt) {
                status = GameStatus.non;
                return;
            }

            int[] val = new int[4] { -1, -1, -1, -1 };
            for(int i = 0; i < PLAYER_CNT; ++i)
            {
                int maxCnt = bombCnt.Max(); //最大の点数を取得
                int maxPl = Array.IndexOf(bombCnt, maxCnt); //最大点を取ったPlayerの番号を取得
                int rank = i + 1; //被りなしの場合の順位
                for (int j = 0; j < i; ++j) 
                {
                    if (val[j] == maxCnt) //過去の点数と同じなら
                    {
                        rank = j + 1; //同順位に更新
                        break;
                    }
                }
                playerInformation[maxPl].AddPlayerScore(rank);
                bombCnt[maxPl] = -1; //該当者の得点をリセット
                val[i] = maxCnt; //同順位判定用のものをセット
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

    public override string SceneName => GameInformation.LoadScene;

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
    public override void OnUnLoaded()
    {
        Debug.Log("Exit_Onishi");
    }
}
