using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : PlayerParent2
{

    protected override void MoveUpdate(Vector2 vec){
        
    }

    protected override void LookUpdate(Vector2 vec)
    {
        
    }

    protected override void OnButtonA() {

        Debug.Log("OnButtonA");
    }

    protected override void UpButtonA() { }

    protected override void OnButtonB() { }

    protected override void UpButtonB() { }

    protected override void OnButtonX() { }

    protected override void UpButtonX() { }

    protected override void OnButtonY() { }

    protected override void UpButtonY() { }
}
