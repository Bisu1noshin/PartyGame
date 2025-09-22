using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public partial class Kameda_TestSceneManager : InGameManeger
{
    Oni_Script o_s;
    [SerializeField] GameObject light;
    public List<PlayerParent> Caughts = new List<PlayerParent>();
    GameState state;
    float timer;
    bool ReadyFlag;
    bool EndFlag;
    bool TitleFlag;
    bool introFlag;
    public Dictionary<int, int> points = new();
    public Dictionary<PlayerParent, int> PlayerNum = new();
    Kameda_CntDnController cd;
    public GameObject introTxt;

    private void Start()
    {
        ReadyFlag = false;
        EndFlag = false;
        TitleFlag = false;
        introFlag = false;
        Caughts.Clear();
        
        o_s = GameObject.Find("Oni").GetComponent<Oni_Script>();
        for(int i = 0; i < 4; ++i)
        {
            Instantiate(light);
        }
        SetPlayers();
        //SetPlayerInformations();
    }
    protected override void Update()
    {
        base.Update();
        StateUpdate(state);
        if(Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        timer += Time.deltaTime;
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
    public override string SceneName => GameInformation.LoadScene;
    public override void OnUnLoaded()
    {
        Debug.Log("Exit_Kameda");
    }
    public GameState GetGameState() => state;
    void UpdatePlayersTransform()
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
    void SetPlayers()
    {
        if(playerInformation == null) { return; }

        // 追記

        Vector3 pos = new Vector3(5, -1.25f, -4);

        //for(int i = GetNullPlayerNum(); i < playerInformation.Length; i++)
        //{
        //    if (playerInformation[i] == null) { continue; }
        //    pos.x = 5 - i;
        //    CreatePlayer(playerInformation[i], pos, Quaternion.Euler(0, 0, 0));
        //}

        
        for (int i = GetNullPlayerNum(); i < playerInformation.Length; i++)
        {
            if (playerInformation[i] == null) { continue; }
            pos.x = 5 - i;
            player[i] = CreatePlayer(playerInformation[i], pos, Quaternion.Euler(0, 0, 0), i + 1);// playerに代入する
        }

        SetPlayerInformations();// ここで呼ぶ
    }
    int GetNullPlayerNum()
    {

        int i;
        for(i = 0; i < player.Length; ++i)
        {
            if (player[i] == null) { return i; }
        }
        return 4;
    }
    void GetRank()
    {
        target[] targets = new target[4]; 
        for(int i = 0; i < player.Length; ++i)
        {
            targets[i] = new target(i, points[i]);
        }
        for(int i = 0; i < 4; ++i)
        {
            for(int j = 0; j < 3 - i; ++j)
            {
                if (targets[j].score > targets[j + 1].score)
                {
                    target tmp = targets[j];
                    targets[j] = targets[j + 1];
                    targets[j + 1] = tmp;
                }
            }
        }
        for(int i = 0; i < 4; ++i)
        {
            playerInformation[targets[i].num].AddPlayerScore(4 - i);
        }
    }
    void SetPlayerInformations()
    {
        points.Clear();
        for(int i = 0; i < player.Length; ++i)
        {
            // 追記

            if (player[i] == null) {

                Debug.LogError("PlayerがNullです。");
                return;
            }

            points.Add(i, 0);
            PlayerNum.Add(player[i], i);

            // Dictionaryにnullは入んないよ。
            // playerをInctance化した後に呼び出すよ
        }
    }
}
class target {
    public int num, score;
    public target(int a, int b)
    {
        num = a; score = b;
    }
};