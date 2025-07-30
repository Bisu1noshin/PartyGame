using UnityEngine;

public class GameInformationPlayer : PlayerParent
{
    private bool decide = default;

    private void Update()
    {
    }

    protected override void MoveUpdate(Vector2 vec)
    {
    }

    protected override void LookUpdate(Vector2 vec)
    {

    }

    protected override void OnButtonA()
    {
        decide = true;
        Debug.Log("OnButtonA");
    }

    protected override void UpButtonA() { }

    protected override void OnButtonB()
    {
    }

    protected override void UpButtonB() { }

    protected override void OnButtonX() { }

    protected override void UpButtonX() { }

    protected override void OnButtonY() { }

    protected override void UpButtonY() { }

    public bool GetDecide() {

        bool flag = decide;

        return flag;
    }
    public void SetDecideToFlase() { decide = false; }
}
