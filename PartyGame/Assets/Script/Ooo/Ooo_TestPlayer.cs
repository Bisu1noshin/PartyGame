using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using DG.Tweening.Core.Easing;

public class Ooo_TestPlayer : PlayerParent
{
    //---------------プレイヤー関連設定---------------
    public GameObject waterbombPrefab;
    public GameObject explodeEffectPrefab;
    float plSpeed = 10.0f;

   

    public float rayLength = 1.0f;  //Wallあたり判定RayCastの長さ


    public int playerId;
    public int score = 0;

    Vector3 moveVec;
    private Vector3 lastPosition;
    private bool isHit = false;
    //------------------------------------------------


    //---------------Trap関連関連設定------------------
    private int maxEscapeClick = 10;    //最大連打可能回数（3秒内に10回押したら脱出可能）
    public int nowEscapeClick = 0;      //現在脱出ボタンを押した回数
    public bool isTrapped = false;      //相手のWaterbombに囲まれたか
    //------------------------------------------------

    protected void Start()
    {
        waterbombPrefab = Resources.Load<GameObject>("Ooo/waterbomb_Prefab");
        explodeEffectPrefab = Resources.Load<GameObject>("Ooo/explodeEffect");

        playerId = playerInput.playerIndex;

        lastPosition = transform.position;
    }

    private void Update()
    {
        if (!isTrapped)    
        {
            WallRaycast();  //Wall当たり判定（移動不可）
        }
    }

    private void WallRaycast()
    {
        if (moveVec == Vector3.zero) return;

        Vector3 direction = moveVec.normalized;
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        if (!Physics.Raycast(origin, direction, rayLength))
        {
            transform.position += direction * plSpeed * Time.deltaTime;
        }

    }
    protected override void MoveUpdate(Vector2 vec)
    {
        if (!isTrapped)
        {
            moveVec = new Vector3(vec.x, 0, vec.y);
        }
    }

    protected override void LookUpdate(Vector2 vec)
    {

    }

    protected override void OnButtonA()
    {
        //Debug.Log("user" + playerData.GetUserValue() + "OnButtonA");

    }

    protected override void UpButtonA() { }

    protected override void OnButtonB()
    {
        if (isTrapped)          //Trapの場合
        {
            nowEscapeClick++;   //BでEscapeClick回数 +1
                                
            if (nowEscapeClick >= maxEscapeClick)   //Escape条件回数超えたら(今は10回)
            {
                Escape();                           //Trapから脱出(動ける）
            }
        }
    }

    protected override void UpButtonB() { }

    protected override void OnButtonX()
    {
        if (!isTrapped)     //Trap状況ではない場合
        {
            ThrowBomb();    //Xでwaterbomb設置
        }
    }

    protected override void UpButtonX() { }

    protected override void OnButtonY() { }

    protected override void UpButtonY() { }



    //---------------waterbomb設置・爆発関数---------------
    void ThrowBomb()
    {
        if (waterbombPrefab != null)
        {
            //現在位置にwaterbomb配置
            GameObject waterbomb = Instantiate(waterbombPrefab, transform.position, Quaternion.identity);

            //誰がwaterbombを配置したのか(waterbombのIDを保存)
            Ooo_waterbombNew ooo_waterbomb = waterbomb.GetComponent<Ooo_waterbombNew>();
            if (ooo_waterbomb != null)
            {
                ooo_waterbomb.Initialize(playerInput.playerIndex,transform.position);
            }

        }
    }
    //--------------------------------------------------------


    //---------------Trap処理---------------------------------
    public void Trap(int ownerplayerId)
    {
        if (!isTrapped)
        {
            isTrapped = true;
            moveVec = Vector3.zero; //動けない
            nowEscapeClick = 0;     //Bボタン回数初期化しとく

            if (ownerplayerId != playerId)                  //自分のwaterbombじゃなかったら
            {
                Ooo_SceneManager.AddScore(ownerplayerId);   //得点
            }
        }
    }

    void Escape()
    {
        isTrapped = false;  //waterbombから脱出
        nowEscapeClick = 0; //Bボタン初期化
    }

    public bool IsTrapped()
    {
        return isTrapped;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("explodeEffect"))
        {
            Ooo_ExplodeEffect effect = other.GetComponent<Ooo_ExplodeEffect>();
            if (effect != null)
            {
                Trap(effect.ownerId);
            }
        }

        if(other.CompareTag("Wall"))
        {
            transform.position -= moveVec * plSpeed * Time.deltaTime;
        }
    }
    //--------------------------------------------------------

}
