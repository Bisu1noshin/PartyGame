using UnityEngine;
using UnityEngine.InputSystem;

public class Onishi_TestPlayer : PlayerParent
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void MoveUpdata(Vector2 vec)
    {
        Vector3 pos = this.transform.position;
        pos += new Vector3(vec.x, 0, vec.y);
        transform.position = pos;
        Debug.Log(vec);
    }

    protected override void LookUpdata(Vector2 vec)
    {
        
    }

    protected override void OnButtonA() {

        Debug.Log("user"+ playerData.GetUserValue() +"OnButtonA");
    }

    protected override void UpButtonA() { }

    protected override void OnButtonB() { }

    protected override void UpButtonB() { }

    protected override void OnButtonX() { }

    protected override void UpButtonX() { }

    protected override void OnButtonY() { }

    protected override void UpButtonY() { }
}
