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
    protected void Start()
    {
        moveVec = Vector3.zero;
        transform.localScale = Vector3.one * 0.5f;
        gameObject.GetOrAddComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, -100, 0));
        ls = GameObject.Find("Light_Player").GetComponent<Light_Script>();
        ls.gameObject.name = "Light_Player_Used";
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
    public void OnCaught()
    {
        ls.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
