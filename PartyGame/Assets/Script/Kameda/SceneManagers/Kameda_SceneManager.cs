using System;
using System.Collections.Generic;
using UnityEngine;

public interface Kameda_PlayerSeeker
{
    int GetPlayerNum(PlayerParent p);
    bool AllPlayerCaught();
    void AddCaught(PlayerParent p);
    void UpdatePlayersTransform();
    void AddPoint(int num, int _point);

    GameState State { get; }
}
public enum GameState
{
    Title = 0, Intro, Ready, Play, End, Result
}

public class Kameda_SceneManager : InGameManeger, Kameda_PlayerSeeker
{
    public static Kameda_SceneManager Instance { get; private set; }
    Oni_Script o_s;
    public List<PlayerParent> Caughts = new();
    public GameState State => currentState.State;
    IState currentState = new TitleState();

    public int[] points = new int[GameInformation.MAX_PLAYER_VALUE];
    [SerializeField] public Dictionary<PlayerParent, int> PlayerNum = new();

    private IReadOnlyDictionary<GameState, IState> StateDic = new Dictionary<GameState, IState>()
    {
        { GameState.Title,  new TitleState() },
        { GameState.Intro, new IntroState() },
        { GameState.Ready, new ReadyState() },
        { GameState.Play, new PlayState() },
        { GameState.End, new EndState() }
    };

    protected override void Awake()
    {
        base.Awake();

        // シングルトーンの処理
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }
    }
    private void Start()
    {
        Caughts.Clear();

        o_s = GameObject.Find("Oni").GetComponent<Oni_Script>();
        for (int i = 0; i < 4; ++i)
        {
            Kameda_UIManager.CreateInWorld(Resources.Load("Kameda/PlayerLight") as GameObject);
        }
        SetPlayers();
    }
    protected override void Update()
    {
        base.Update();
        StateUpdate();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    void StateUpdate() //現在の状態に応じてステートマシンを呼び出す
    {
        if (currentState.Phase == StatePhase.Enter) //スタート
        {
            currentState.Start();
        }
        else if (currentState.Phase == StatePhase.Update) //アップデート
        {
            currentState.Update();
        }
        else if (currentState.Phase == StatePhase.Exit) //次のステート
        {
            SetNextState(currentState.State);
        }
    }

    async void SetNextState(GameState state) //次のステートを呼び出す
    {
        if (state == GameState.End)
        {
            await NextScene();
        }
        else
        {
            currentState = StateDic[state + 1];
        }
    }


    public void UpdatePlayersTransform() //プレイヤーの位置を更新する　鬼が使う
    {
        int j = 0;
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i] != null)
            {
                o_s.playersPos[j++] = player[i].transform;
            }
        }
    }

    void SetPlayers() //プレイヤーを4人召喚する
    {
        if (playerInformation == null) { return; }

        // 追記

        Vector3 pos = new(5, -1.25f, -4);

        for (int i = GetNullPlayerNum(); i < playerInformation.Length; i++)
        {
            if (playerInformation[i] == null) { continue; }
            pos.x = 5 - i;
            player[i] = CreatePlayer(playerInformation[i], pos, Quaternion.Euler(0, 0, 0), i + 1);// playerに代入する
        }

        SetPlayerInformations();// ここで呼ぶ
    }
    public int GetPlayerNum(PlayerParent p)
    {
        if (!PlayerNum.ContainsKey(p)) {
            //追加
            PlayerNum.Add(p, GetNullPlayerNum());
            Debug.LogError("存在しないキー :" + p.GetType().Name);
            return -1;
        }
        return PlayerNum.GetValueOrDefault(p);
    }
    int GetNullPlayerNum() //プレイヤーを追加する場所を決める
    {
        int i;
        for (i = 0; i < player.Length; ++i)
        {
            if (player[i] == null) { return i; }
        }
        return 4;
    }
    public bool AllPlayerCaught()
    {
        return Caughts.Count == 4;
    }
    public void AddPoint(int num, int _point)
    {
        points[num] = _point;
    }

    public void UpdatePlayerScore() //スコアを決める
    {
        if (player[0] == null) return;

        (int num, int score)[] targets = new (int num, int score)[4];
        for (int i = 0; i < player.Length; ++i)
        {
            targets[i] = (i, points[i]);
        }

        for (int i = 0; i < 4; ++i) //スコアを参照して昇順にソート
        {
            for (int j = 0; j < 3 - i; ++j)
            {
                if (targets[j].score > targets[j + 1].score)
                {
                    (targets[j], targets[j + 1]) = (targets[j + 1], targets[j]);
                }
            }
        }
        for (int i = 0; i < 4; ++i)
        {
            playerInformation[targets[i].num].AddPlayerScore(i + 1);
        }
    }

    void SetPlayerInformations()
    {
        for (int i = 0; i < player.Length; ++i)
        {
            // 追記

            if (player[i] == null)
            {

                Debug.LogError("PlayerがNullです。");
                return;
            }

            points[i] = 0;
            PlayerNum.Add(player[i], i);

            // playerをInctance化した後に呼び出すよ
        }
    }
    public void AddCaught(PlayerParent p)
    {
        Caughts.Add(p);
    }

    protected override Type SetPlayerScript()
    {
        return typeof(Player_Instant);
    }

    protected override string SetPlayerPrefab(int index)
    {
        string str =
            "Player/VRM/VRM_" + index.ToString();

        return str;
    }

    public override string SceneName => "ResultScene";

    public override void OnUnLoaded()
    {
        Debug.Log("Exit_Kameda");
    }

}
