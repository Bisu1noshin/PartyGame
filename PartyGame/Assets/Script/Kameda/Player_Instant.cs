using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;

public class Player_Instant : PlayerParent
{
    float PlayerSpeed = 4.0f;
    Vector3 moveVec;
    Rigidbody rb;
    Light_Script ls;
    GameObject oni;
    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(-5, 0, -2) + Vector3.up * -1.25f;
        moveVec = Vector3.zero;
        transform.localScale = Vector3.one * 0.5f;
        gameObject.GetOrAddComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, -100, 0));
        ls = GameObject.Find("Light_Player" + GetPlayerNum()).GetComponent<Light_Script>();
        ls.player = this.gameObject;
        oni = GameObject.Find("Oni");
    }
    private void Update()
    {
        rb.position += moveVec.normalized * PlayerSpeed * Time.deltaTime;
        UpdateTransformforOni();
        SetLightColorInDenger();
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
    void UpdateTransformforOni()
    {
        int playernum = GetPlayerNum() - 1;
        GameObject.Find("Oni").GetComponent<Oni_Script>().playersPos[playernum] = transform;
    }
    int GetPlayerNum()
    {
        return GetPlayerDate().GetUserValue();
    }
    void SetLightColorInDenger()
    {
        float mag = (oni.transform.position - transform.position).magnitude;
        if(mag < 5.0f)
        {
            ls.lightColor = Mathf.Clamp01((mag - 2.5f) / 5.0f);
        }
        else
        {
            ls.lightColor = 1;
        }
    }
}
