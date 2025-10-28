using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class ResultSceneManager : InGameManeger
{
    [SerializeField] TextMeshProUGUI[] Rank = new TextMeshProUGUI[4];
    [SerializeField] TextMeshProUGUI Txt_ty;
    private GameInformationPlayer[] _player = default;　// playerの派生クラス

    AudioSource audioSource;
    [SerializeField] AudioClip SE_don;
    [SerializeField] AudioClip SE_clap;

    private int[] rank = new int[4] { -1, -1, -1, -1 }; //Pl0~3に順位番号が入る
    private bool[] isSet = new bool[4] { false, false, false, false };

    private int index = 0;
    private float timeCnt = 0;
    private const float createCnt = 0.5f;

    protected override string SetPlayerPrefab(int index)
    {
        string str =
            "Player/VRM/VRM_" + index.ToString();
        return str;
    }

    protected override Type SetPlayerScript()
    {
        return typeof(GameInformationPlayer);
    }

    private void Start()
    {
        // 例外処理
        {
            // プレイヤーの情報がなかった場合
            if (playerInformation == null)
            {
                Debug.LogError("playerの情報がnullです。");
                return;
            }
        }

        // その他初期化処理
        {
            for (int i = 0; i < 4; i++)
            {
                if (playerInformation[i] == null)
                    throw new ArgumentOutOfRangeException(
                       "playerInformation "+ i.ToString() +"is null"
                       );
            }
        }

        audioSource = GetComponent<AudioSource>();

        // playerの召喚
        {
            int length = playerInformation.Length;
            _player = new GameInformationPlayer[length];

            int[] score = new int[length];

            for (int i = 0; i < length; i++)
            {
                Vector3 pos = new Vector3(-10000, 0, 0);// 画面外に飛ばす
                player[i] = CreatePlayer(playerInformation[i], pos, Quaternion.identity, i + 1);

                // playerの派生クラスの取得
                _player[i] = player[i].gameObject.GetComponent<GameInformationPlayer>();
                score[i] = playerInformation[i].PlayerScore;
            }

            int[] val = new int[4] { -1, -1, -1, -1 }; //同順位判定
            for (int i = 0; i < length; ++i)
            {
                int maxCnt = score.Max(); //最大の点数を取得
                int maxPl = Array.IndexOf(score, maxCnt); //最大点を取ったPlayerの番号を取得
                int rank_ = i + 1; //被りなしの場合の順位をメモ
                for (int j = 0; j < i; ++j)
                {
                    if (val[j] == maxCnt) //過去の点数と同じなら
                    {
                        rank_ = j + 1; //同順位に更新
                        break;
                    }
                }
                rank[maxPl] = rank_;
                score[maxPl] = -1;
                val[i] = maxCnt; //同順位判定用のものをセット
            }
        }
    }
    protected override void Update()
    {
        base.Update();

        for (int i = 0; i < Rank.Length; i++)
        {
            GamingColor(Rank[i]);
        }
        GamingColor(Txt_ty);

        timeCnt += Time.deltaTime;

        SetPlayerRank();

        if (GetAllDecide())
        {
            SceneManager.LoadScene("TitleScene"); 
        }

    }

    public override string SceneName => "TitleScene";
    public override void OnUnLoaded() {
        int[] score = new int[4] {

            playerInformation[0].PlayerScore,
            playerInformation[1].PlayerScore,
            playerInformation[2].PlayerScore,
            playerInformation[3].PlayerScore
        };

        for (int i = 0; i < 4; i++)
        {

            Debug.Log("index :" + i + "score :" + score[i]);
        }
    }
    private void GamingColor(MaskableGraphic ui)
    {
        float addValue = 1f / 256f * 16f;
        float maxValue = 1f;

        float r = ui.color.r;
        float g = ui.color.g;
        float b = ui.color.b;

        if (r == maxValue && g == 0)
        {

            b += addValue;
        }

        if (g == 0 && b == maxValue)
        {
            r -= addValue;
        }

        if (r == 0 && b == maxValue)
        {
            g += addValue;
        }

        if (r == 0 && g == maxValue)
        {
            b -= addValue;
        }

        if (b == 0 && g == maxValue)
        {

            r += addValue;
        }

        if (b == 0 && r == maxValue)
        {
            g -= addValue;
        }

        ui.color = new Color(r, g, b);
    }
    private bool GetAllDecide()
    {

        bool flag = false;

        foreach (var player in _player)
        {

            // 一人でもふらぐがたっていなければはじく
            if (!player.GetDecide()) { return flag; }
        }

        flag = true;

        foreach (var player in _player)
        {
            // 全員のフラグをおろす
            player.SetDecideToFlase();
        }

        return flag;
    }

    private void SetPlayer(int index) {

        int length = GameInformation.MAX_PLAYER_VALUE;
        Vector3[] lastpos = new Vector3[4] { new Vector3(-6, -2, 0), new Vector3(-2, -2, 0), new Vector3(2, -2, 0), new Vector3(6, -2, 0) };

        if (isSet[rank[index] - 1] == false) //その位置になければ
        {
            player[index].transform.position = lastpos[rank[index] - 1]; //配置
            isSet[rank[index] - 1] = true;
        }
        else //既に置かれていたら
        {
            for (int j = rank[index]; j < length; ++j)
            {
                if (isSet[j] == false) //次の順位を探して設置
                {
                    player[index].transform.position = lastpos[j];
                    isSet[j] = true;
                    break;
                }
            }
        }
        switch (rank[index])
        {
            case 1:
                Rank[rank[index] - 1].SetText("No.1!!");
                break;
            case 2:
                Rank[rank[index] - 1].SetText("No.2");
                break;
            case 3:
                Rank[rank[index] - 1].SetText("No.3");
                break;
            case 4:
                Rank[rank[index] - 1].SetText("No.4");
                break;
        }
        player[index].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }

    private void SetPlayerRank()
    {
        if (index == 5) { return; }

        if (timeCnt >= createCnt * index)
        {
            if (index < 4)
            {
                SetPlayer(index);
                AudioSource.PlayClipAtPoint(SE_don, Camera.main.transform.position);
                index++;
            }
            else if (index == 4)
            {
                Txt_ty.SetText("Thank You for Playing!!!");
                AudioSource.PlayClipAtPoint(SE_clap, Camera.main.transform.position);
                index++;
            }
        }
    }
}
