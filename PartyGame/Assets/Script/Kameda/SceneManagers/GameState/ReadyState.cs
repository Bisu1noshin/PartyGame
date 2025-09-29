using UnityEngine;

public class ReadyState : IState
{
    StatePhase m_Phase = StatePhase.Ready;
    public StatePhase Phase => m_Phase;
    public GameState State => GameState.Ready;
    float m_timer = 0.0f;

    public void Start()
    {
        GameObject go = Kameda_UIManager.Create(Resources.Load("Font/CountDown") as GameObject, "TimeCount");
        go.transform.localPosition = new(0, 220, 0);
        Kameda_UIManager.Create(Resources.Load("Font/Text_Start") as GameObject);
        m_Phase = StatePhase.Started;
    }

    public void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer >= 4.0f)
        {
            m_Phase = StatePhase.Ended;
        }
    }
}
