using System;
using System.Collections.Generic;
using UnityEngine;

public class Kameda_TestSceneManager : InGameManeger
{
    Oni_Script o_s;
    [SerializeField] GameObject light;
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
        int j = 0;
        for(int i = 0; i < player.Length; i++)
        {
            if (player[i] != null)
            {
                o_s.playersPos[j] = player[i].transform;
                j++;
            }
        }
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
