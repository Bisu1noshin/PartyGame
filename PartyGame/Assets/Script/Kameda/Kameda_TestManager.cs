using System.Threading.Tasks;
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
        if (!introFlag)
        {
            GameObject g = Instantiate(introTxt);
            g.name = "intro";
            g.transform.SetParent(GameObject.Find("Canvas").transform);
            g.transform.localPosition = Vector3.zero;
            g.transform.localRotation = Quaternion.identity;
            g.transform.localScale = Vector3.one;
            introFlag = true;
        }
        if(timer >= 5.0f)
        {
            Destroy(GameObject.Find("intro"));
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
            go.name = "CountDown";
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
        cd.SetText((60 - (int)timer).ToString());
        if(timer >= 60.0f)
        {
            timer = 0;
            state = GameState.End;
        }
    }
    async void EndUpdate()
    {
        if (!EndFlag)
        {
            GetRank();
            GameObject go = Instantiate(Resources.Load("Font/Text_Finish") as GameObject);
            go.transform.SetParent(GameObject.Find("Canvas").transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            Destroy(GameObject.Find("CountDown"));
            EndFlag = true;
        }
        if(timer >= 2.0f)
        {
            timer = 0;
            await NextScene();
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