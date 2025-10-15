using UnityEngine;

public enum StatePhase
{
    Enter,
    Update,
    Exit,
}

public interface IState //疑似的なステートマシン
{
    GameState State { get; }
    StatePhase Phase { get; }
    void Start();
    void Update();
}

public interface IGameState {
    void ExecuteState();
}

public abstract class ParentGameState : IGameState{

    protected   PlayerParent[]  PlayerParent    =    default;
    StatePhase phase = StatePhase.Enter;
    float m_timer = 0;
    protected abstract float TimeLimit();
    void  Enter()
    {
        if (phase != StatePhase.Enter) { return; }
        OnEnter();
        phase++;
    }
    void Update()
    {
        if (phase != StatePhase.Update) { return; }
        OnUpdate();
        m_timer += Time.deltaTime;
        if (m_timer > TimeLimit())
        {
            phase++;
        }
    }
    void Exit()
    {
        if (phase != StatePhase.Exit) { return; }
        OnExit();
    }
    protected abstract void OnEnter();
    protected abstract void OnUpdate();
    protected abstract void OnExit();
    public void ExecuteState()
    {
        Enter();
        Update();
        Exit();
    }
}

public class SceneManager_test {

    static PlayerParent[] p = new PlayerParent[4];
    //IGameState PGS = new ParentGameState(p);
    StatePhase s = 0;
    void UpDate() {

        //PGS.onUpdate(s);
        //PGS.onEnter(s);
        //PGS.onExit(s);
        //if (!PGS.onEnter(s) && !PGS.onExit(s))
        //{
        //    PGS.onUpdate(s);
        //}
    }
}
