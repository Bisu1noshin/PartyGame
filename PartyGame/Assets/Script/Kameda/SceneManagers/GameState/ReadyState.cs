using UnityEngine;

public class ReadyState : IState
{
    StatePhase m_Phase = StatePhase.Enter;
    public StatePhase Phase => m_Phase;
    public GameState State => GameState.Ready;
    float m_timer = 0.0f;

    public void Start()
    {
        GameObject go = Kameda_UIManager.Create(Resources.Load("Font/CountDown") as GameObject, "TimeCount");
        go.transform.localPosition = new(0, 510, 0);
        go.transform.localScale = new(1, 1.5f, 1);
        Kameda_UIManager.Create(Resources.Load("Font/Text_Start") as GameObject);
        m_Phase = StatePhase.Update;
    }

    public void Update()
    {
        // if (PauseState.enable == true) return;
        m_timer += Time.deltaTime;
        if (m_timer >= 4.0f)
        {
            m_Phase = StatePhase.Exit;
        }
    }
}
