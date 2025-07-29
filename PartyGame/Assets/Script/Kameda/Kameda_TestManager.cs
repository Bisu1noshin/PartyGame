using UnityEngine;
public enum GameState
{
    Title, Introduction, Ready, Play, End, Result
}
public partial class Kameda_TestSceneManager : InGameManeger
{
    void TitleUpdate()
    {
        if(timer >= 2.0f)
        {
            timer = 0;
            state = GameState.Introduction;
        }
    }
    void IntroUpdate()
    {

    }
    void ReadyUpdate()
    {

    }
    void PlayUpdate()
    {
        UpdatePlayersTransform();
    }
    void EndUpdate()
    {

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