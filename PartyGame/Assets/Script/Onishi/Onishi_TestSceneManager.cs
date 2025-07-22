using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Onishi_TestSceneManager : InGameManeger
{
    enum GameStatus
    {
        standby,    //スタンバイ 始まる前
        play,       //インゲーム プレイ中
        finish,     //フィニッシュ ゲーム終了
        non         //それ以外 基本的に使われない
    };

    GameStatus status; //ゲームステータス管理
    float timer = 0f; 
    float endTime = 20f; //終了時間

    bool playerFlag = false; //プレイヤーの存在フラグ

    [SerializeField]GameObject StartText;
    [SerializeField] GameObject Canvas;

    protected override Type SetPlayerScript()
    {
        return typeof(Onishi_TestPlayer);
    }

    private void Start()
    {
        playerInformation = new PlayerInformation[1];
        status = GameStatus.standby;
    }

    protected override void Update()
    {
        base.Update();

        for(int i=0; i<1; i++)
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

            for (int i = 0; i < 1; i++)
            {
                player[i] = CreatePlayer(
                    playerInformation : playerInformation[i],
                    p : vec,
                    q : quat
                    );
            }

            playerFlag = true;
        }

        if (status==GameStatus.standby)
        {
            Debug.Log("開始");
            GameObject go = Instantiate(StartText);
            go.transform.SetParent(Canvas.transform);
            status = GameStatus.play;
        }

        if (status==GameStatus.play)
        {
            timer += Time.deltaTime;
            if (timer >= endTime)
            {
                Debug.Log("終了");
                status = GameStatus.finish;
            }
        }
    }

    protected override string SetPlayerPrefab(int index)
    {
        string str =
            "Player/Test/Cube_" + index.ToString();

        return str;
    }

    public bool isPlaying()
    {
        if (status == GameStatus.play) return true;
        else return false;
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

        SSceneManager.LoadScene<PlayerInputManager>(playerInformation).Forget();
    }
}
