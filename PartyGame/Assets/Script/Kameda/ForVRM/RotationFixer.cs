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
    protected void Rotate(Vector3 delta)
    {
        Vector3 angle = transform.localEulerAngles;
        angle += delta;
        transform.localEulerAngles = angle;
    }
    protected void Rotate(float x, float y, float z)
    {
        Vector3 angle = transform.localEulerAngles;
        angle += new Vector3(x, y, z);
        transform.localEulerAngles = angle;
    }
}
