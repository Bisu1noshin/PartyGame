using UnityEngine;

/// <summary>
/// 最低限の回転制御スクリプト
/// </summary>
public abstract class RotationFixer : MonoBehaviour
{
    public enum AnimState { Idle, Run };
    public AnimState state { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = AnimState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case AnimState.Idle:
                IdleRotate();
                break;
            case AnimState.Run:
                RunningRotate();
                break;
        }
    }
    public void SetState(AnimState state)
    {
        this.state = state;
    }
    protected abstract void IdleRotate();
    protected abstract void RunningRotate();
}
