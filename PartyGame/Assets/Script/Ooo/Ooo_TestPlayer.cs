using UnityEngine;
using UnityEngine.InputSystem;

public class Ooo_TestPlayer : PlayerParent
{
    Vector3 moveVec;
    float plSpeed = 10.0f;


    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        transform.position += moveVec * plSpeed * Time.deltaTime;
    }

    protected override void MoveUpdate(Vector2 vec)
    {
        moveVec = new Vector3(vec.x, 0, vec.y);
    }

    protected override void LookUpdate(Vector2 vec)
    {

    }

    protected override void OnButtonA()
    {

        Debug.Log("user" + playerData.GetUserValue() + "OnButtonA");
    }

    protected override void UpButtonA() { }

    protected override void OnButtonB() { }

    protected override void UpButtonB() { }

    protected override void OnButtonX() { }

    protected override void UpButtonX() { }

    protected override void OnButtonY() { }

    protected override void UpButtonY() { }
}