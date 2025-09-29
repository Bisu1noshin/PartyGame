using UnityEngine;

public class IntroState : IState
{
    public StatePhase Phase => m_Phase;
    StatePhase m_Phase = StatePhase.Ready;
    float m_timer = 0.0f;
    public GameState State => GameState.Intro;

    public void Start()
    {
        Kameda_UIManager.Create(Resources.Load("Font/Text_Intro") as GameObject, "intro");
        m_Phase = StatePhase.Started;
    }

    public void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer >= 5.0f)
        {
            Kameda_UIManager.Destroy("intro");
            m_Phase = StatePhase.Ended;
        }
    }
}
