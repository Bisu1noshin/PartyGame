using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Ooo_TestPlayer : PlayerParent
{
    Vector3 moveVec;

    //プレイヤー関連設定
    float plSpeed = 10.0f;
    public GameObject waterbombPrefab;

    //Waterbomb関連設定
    public float waterbombTime = 3f;    //基本爆発時間は3秒(囲まれたら3秒）
    public float escapeTimeUp = 0.3f;   //連打すれば0.3秒ずつ減少
    public int maxEscapeClick = 10;     //最大連打可能回数（3秒内に10回押したら脱出可能）

    private bool isTrapped = false;    //相手のWaterbombに囲まれたか
    private float nowEscapeClick = 0;   //現在脱出ボタンを押した回数
    

   


    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (!isTrapped)    //囲まれてなかったら普通に移動できる
        {
            transform.position += moveVec * plSpeed * Time.deltaTime;
        }
    }

    protected override void MoveUpdate(Vector2 vec)
    {
        if (!isTrapped)    //囲まれてなかったら移動できる
        {
            moveVec = new Vector3(vec.x, 0, vec.y);
        }
    }

    protected override void LookUpdate(Vector2 vec)
    {

    }

    protected override void OnButtonA()
    {
        Debug.Log("user" + playerData.GetUserValue() + "OnButtonA");


        if(!isTrapped)     //囲まれてなかったら
        {
            ThrowBomb();    //Aボタン押したらWaterbombをプレイヤの現在位置に置く
        }
    }

    protected override void UpButtonA() { }

    protected override void OnButtonB()
    {
        if(isTrapped)  //囲まれたら
        {
            nowEscapeClick++;   //Bボタン押すたびに +1

            if(nowEscapeClick >= maxEscapeClick)    //3秒内に10回以上押したら
            {
                Escape();   //水風船から脱出！
            }
        }
    }

    protected override void UpButtonB() { }

    protected override void OnButtonX() { }

    protected override void UpButtonX() { }

    protected override void OnButtonY() { }

    protected override void UpButtonY() { }


    void ThrowBomb()    //Waterbombを置く関数
    {
        if(waterbombPrefab != null)
        {
            //今のプレイヤの位置に水風船配置
            GameObject waterbomb = Instantiate(waterbombPrefab, transform.position, Quaternion.identity);

            //誰がwaterbombを配置したのか
            Ooo_Waterbomb ooo_waterbomb = waterbomb.GetComponent<Ooo_Waterbomb>();  //Waterbombスクリプト
            if(ooo_waterbomb != null)
            {
                ooo_waterbomb.Initialize(playerData.GetUserValue());
            }
            
        }
    }

    void Escape()
    {
        isTrapped = false;  //水風船から脱出
        nowEscapeClick = 0; //Bボタン押した回数初期化
    }
}