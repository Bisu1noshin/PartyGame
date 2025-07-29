using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public partial class Kameda_TestSceneManager : InGameManeger
{
    Oni_Script o_s;
    [SerializeField] GameObject light;
    GameState state;
    float timer;
    private void Start()
    {
        o_s = GameObject.Find("Oni").GetComponent<Oni_Script>();
        for(int i = 0; i < 4; ++i)
        {
            Instantiate(light);
        }
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
            "Player/Test_Kameda/Cube_" + index.ToString();

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
    public override void OnUnLoaded() {
        Debug.Log("Exit_Kameda");
    }

    protected override void NextSceneJump()
    {

        SSceneManager.LoadScene<Kameda_TestSceneManager>(playerInformation).Forget();
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
        Vector3 pos = new Vector3(5, -1.25f, -4);
        for(int i = GetNullPlayerNum(); i < playerInformation.Length; i++)
        {
            pos.x = 5 - i;
            CreatePlayer(playerInformation[i], pos, Quaternion.Euler(0, 0, 0));
        }
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
}
