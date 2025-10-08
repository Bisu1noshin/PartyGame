using UnityEngine;

public class TitleState : IState
{
    public GameState State => GameState.Title;


    public StatePhase Phase => m_Phase;
    StatePhase m_Phase = StatePhase.Enter;
    float m_timer = 0.0f;


    public void Start()
    {
        Kameda_UIManager.Create(Resources.Load("Kameda/Thumbnail") as GameObject, "Logo");
        m_Phase = StatePhase.Update;
    }

    public void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > 2.0f)
        {
            m_Phase = StatePhase.Exit;
        }
    }
}
