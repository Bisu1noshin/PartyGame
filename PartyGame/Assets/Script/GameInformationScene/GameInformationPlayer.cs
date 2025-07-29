using UnityEngine;

public class GameInformationPlayer : PlayerParent
{
    Vector3 moveVec;
    float plSpeed = 10.0f;

    private void Update()
    {
    }

    protected override void MoveUpdate(Vector2 vec)
    {

        //移動方向を決定
        moveVec = new Vector3(vec.x, 0, vec.y);

        // 回転の補正
        //animationContllore.RotaitionContllore(vec);
    }

    protected override void LookUpdate(Vector2 vec)
    {

    }

    protected override void OnButtonA()
    {

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
}
