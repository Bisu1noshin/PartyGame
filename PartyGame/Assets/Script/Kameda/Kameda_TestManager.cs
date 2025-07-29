using UnityEngine;
public enum GameState
{
    Title, Introduction, Ready, Play, End, Result
}
public partial class Kameda_TestSceneManager : InGameManeger
{
    void TitleUpdate()
    {
        if (!TitleFlag) {
            Instantiate(Resources.Load("Kameda/Thumbnail") as GameObject);
            TitleFlag = true;
        }
        if(timer >= 2.0f)
        {
            timer = 0;
            state = GameState.Introduction;
        }
    }
    void IntroUpdate()
    {
        if(timer >= 2.0f)
        {
            timer = 0;
            state = GameState.Ready;
        }
    }
    void ReadyUpdate()
    {
        if(Kameda_CntDnController.Instance == null)
        {
            GameObject go = Instantiate(Resources.Load("Font/CountDown") as GameObject);
            cd = go.GetComponent<Kameda_CntDnController>();
        }
        if (!ReadyFlag)
        {
            GameObject go = Instantiate(Resources.Load("Font/Text_Start") as GameObject);
            go.transform.SetParent(GameObject.Find("Canvas").transform, false);
            ReadyFlag = true;
        }
        if(timer >= 4.0f)
        {
            timer = 0;
            state = GameState.Play;
        }
    }
    void PlayUpdate()
    {
        UpdatePlayersTransform();
        cd.SetText("00:" + (60 - (int)timer).ToString());
        if(timer >= 60.0f)
        {
            timer = 0;
            state = GameState.End;
        }
    }
    void EndUpdate()
    {
        if (!EndFlag)
        {
            GetRank();
            GameObject go = Instantiate(Resources.Load("Font/Text_Finish") as GameObject);
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            EndFlag = true;
        }
        if(timer >= 2.0f)
        {
            timer = 0;
            NextSceneJump();
        }
    }
    void ResultUpdate()
    {
        
    }
    void StateUpdate(GameState s)
    {
        switch (s)
        {
            case GameState.Title:
                TitleUpdate(); break;
            case GameState.Introduction:
                IntroUpdate(); break;
            case GameState.Ready:
                ReadyUpdate(); break;
            case GameState.Play:
                PlayUpdate(); break;
            case GameState.End:
                EndUpdate(); break;
            case GameState.Result:
                ResultUpdate(); break;
        }
    }
}