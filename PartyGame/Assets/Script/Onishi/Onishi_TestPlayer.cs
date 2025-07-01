using UnityEngine;
using UnityEngine.InputSystem;

public class Onishi_TestPlayer : PlayerParent
{
    Vector3 moveVec;
    float plSpeed = 10.0f;

    int bombCnt = 0; //手榴弾

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        transform.position += moveVec * plSpeed * Time.deltaTime;

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
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
        if (bombCnt >= 1)
        {

        }
        Debug.Log("user"+ playerData.GetUserValue() +"OnButtonA");
    }

    protected override void UpButtonA() { }

    protected override void OnButtonB() { }

    protected override void UpButtonB() { }

    protected override void OnButtonX() { }

    protected override void UpButtonX() { }

    protected override void OnButtonY() { }

    protected override void UpButtonY() { }

    private void OnTriggerEnter(Collider other)
    {
        bombCnt++;
        Destroy(other.gameObject);
    }
}
