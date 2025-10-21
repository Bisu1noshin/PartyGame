using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class ResultSceneManager : InGameManeger
{
    [SerializeField] TextMeshProUGUI[] Rank = new TextMeshProUGUI[4];
    private GameInformationPlayer[] _player = default;　// playerの派生クラス

    public class RankInfo
    {
        public int playerindex { get; private set; } = default;
        public int rank { get; private set; } = default;

        /// <summary>
        /// プレイヤーの順位を保存する
        /// </summary>
        /// <param name="playerindex"></param>
        /// <param name="rank"></param>
        public RankInfo(int playerindex,int rank) {

            if (playerindex < 0 || playerindex > 4) {

                throw new ArgumentOutOfRangeException(
                    "プレイヤーの配列があってません"
                    );
            }

            this.playerindex = playerindex;

            if (rank < 0 || rank > 4){

                throw new ArgumentOutOfRangeException(
                    "プレイヤーの配列があってません"
                    );
            }

            this.rank = rank;
        }
    }

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

            //// 順位の処理
            //{
            //    RankInfo[] ranks = new RankInfo[length];

            //    for (int i = 0; i < ranks.Length; i++) {

            //        ranks[i] = new RankInfo();
            //    }
            //}

            int[] rank = new int[4] { -1, -1, -1, -1 }; //Pl0~3に順位番号が入る
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

            Vector3[] lastpos = new Vector3[4] { new Vector3(-6, -2, 0), new Vector3(-2, -2, 0), new Vector3(2, -2, 0), new Vector3(6, -2, 0) };
            bool[] isSet = new bool[4] { false, false, false, false };
            for(int i = 0; i < length; ++i)
            {
                if (isSet[rank[i]-1] == false) //その位置になければ
                {
                    player[i].transform.position = lastpos[rank[i] - 1]; //配置
                    isSet[rank[i]-1] = true;
                }
                else //既に置かれていたら
                {
                    for (int j = rank[i]; j < length; ++j) 
                    {
                        if (isSet[j] == false) //次の順位を探して設置
                        {
                            player[i].transform.position = lastpos[j];
                            isSet[j] = true;
                            break;
                        }
                    }
                }
                switch (rank[i])
                {
                    case 1:
                        Rank[rank[i] - 1].SetText("No.1!!");
                        break;
                    case 2:
                        Rank[rank[i] - 1].SetText("No.2");
                        break;
                    case 3:
                        Rank[rank[i] - 1].SetText("No.3");
                        break;
                    case 4:
                        Rank[rank[i] - 1].SetText("No.4");
                        break;
                }
                player[i].transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            }

            for(int i = 0; i < length; ++i)
            {
                Debug.Log("player:" + i + " pos:" + player[i].transform.position.ToString());
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

        if (!GetAllDecide()) {
            return;
        }



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
}
