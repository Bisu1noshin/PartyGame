using UnityEngine;

public class PlayState : IState
{
    StatePhase m_Phase = StatePhase.Ready;
    public StatePhase Phase => m_Phase;
    public GameState State => GameState.Play;
    float m_timer = 0.0f;
    GameObject count;

    public void Start()
    {
        count = GameObject.Find("TimeCount");
        m_Phase = StatePhase.Started;
    }

    public void Update()
    {
        if (count.gameObject.TryGetComponent<Kameda_CntController>(out var c))
        {
            c.SetText((60 - (int)m_timer).ToString());
        }
        m_timer += Time.deltaTime;
        if (m_timer >= 60.0f)
        {
            m_Phase = StatePhase.Ended;
        }
    }
}
