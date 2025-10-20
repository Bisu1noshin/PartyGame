using UnityEngine;

public class EndState : IState
{
    public GameState State => GameState.End;
    public StatePhase Phase => m_Phase;
    StatePhase m_Phase = StatePhase.Enter;
    float m_timer = 0.0f;

    public void Start()
    {
        Kameda_UIManager.Create(Resources.Load("Font/Text_Finish") as GameObject);
        Kameda_UIManager.Destroy("CountDown");
        m_Phase = StatePhase.Update;
        Kameda_SceneManager.Instance.UpdatePlayerScore();
    }

    public void Update()
    {
        m_timer += Time.deltaTime;
        if(m_timer > 2.0f)
        {
            m_Phase = StatePhase.Exit;
        }
    }
}
