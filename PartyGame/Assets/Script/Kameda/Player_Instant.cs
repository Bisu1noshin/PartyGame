using UnityEngine;

public class Player_Instant : PlayerParent
{
    float PlayerSpeed = 4.0f;
    Vector3 moveVec;
    Rigidbody rb;
    Light_Script ls;
    GameObject oni;
    SphereCollider sc;

    protected void Start()
    {
        moveVec = Vector3.zero;
        transform.localScale = Vector3.one * 0.5f;
        transform.position = Vector3.one * -1.2f;

        InitComponents();
        ls = GameObject.Find("Light_Player").GetComponent<Light_Script>();
        ls.gameObject.name = "Light_Player_Used";
        ls.player = this.gameObject;
        oni = GameObject.Find("Oni");

        rb.freezeRotation = true;
    }
    private void Update()
    {
        if (Kameda_SceneManager.Instance.State != GameState.Play) { return; }
        rb.position += moveVec.normalized * PlayerSpeed * Time.deltaTime;
        UpdateTransformforOni();
        SetLightColorInDenger();

        // アニメーションの管理
        if (moveVec.x == 0 && moveVec.y == 0)
        {
            // 入力がなければ待機
            animationContllore.SetAniamation(PlayerAniamtonState.Idle);
        }
        else
        {

            // 入力があれば走る
            animationContllore.SetAniamation(PlayerAniamtonState.Walk);
        }
    }
    protected override void MoveUpdate(Vector2 vec)
    { 
        moveVec = new Vector3(vec.x, 0, vec.y);

        // 回転の補正
        animationContllore.RotaitionContllore(vec);
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
    void InitComponents()
    {
        sc = this.gameObject.AddComponent<SphereCollider>();
        sc.radius = 0.5f; sc.center = new Vector3(0, 0.5f, 0);
        rb = this.gameObject.AddComponent<Rigidbody>();
        Kameda_PlayerSeeker sm = Kameda_SceneManager.Instance;
        Debug.Log("入室 : Player" + sm.GetPlayerNum(this));
    }
}
