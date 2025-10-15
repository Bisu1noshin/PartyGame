using UnityEngine;

public class PlayState : IState
{
    StatePhase m_Phase = StatePhase.Enter;
    public StatePhase Phase => m_Phase;
    public GameState State => GameState.Play;
    float m_timer = 0.0f;
    GameObject count;

    public void Start()
    {
        count = GameObject.Find("TimeCount");
        m_Phase = StatePhase.Update;
    }

    public void Update()
    {
        if (Kameda_CntController.Instance != null)
        {
            Kameda_CntController.Instance.SetText((60 - (int)m_timer).ToString());
        }
        m_timer += Time.deltaTime;
        Kameda_SceneManager.Instance.UpdatePlayersTransform();
        if (m_timer >= 60.0f || Kameda_SceneManager.Instance.AllPlayerCaught())
        {
            m_Phase = StatePhase.Exit;
        }
    }
}
