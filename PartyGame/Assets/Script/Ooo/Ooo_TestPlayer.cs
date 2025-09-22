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

    public float rayLength = 1.0f;


    public int playerId;
    public int score = 0;

    Vector3 moveVec;
    private Vector3 lastPosition;
    private bool isHit = false;
    //------------------------------------------------


    //---------------Trap関連関連設定------------------
    public float trapTime = 3f;             //基本爆発時間は3秒(囲まれたら3秒間動けない）
    private int maxEscapeClick = 10;         //最大連打可能回数（3秒内に10回押したら脱出可能）
    public int nowEscapeClick = 0;   //現在脱出ボタンを押した回数

    public bool isTrapped = false;    //相手のWaterbombに囲まれたか
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
        if (!isTrapped)    //Trap状況ではない場合移動不可能
        {
            TryMoveWithRaycast();
        }
    }

    private void TryMoveWithRaycast()
    {
        if (moveVec == Vector3.zero) return;

        Vector3 direction = moveVec.normalized;
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        if (!Physics.Raycast(origin, direction, rayLength))
        {
            transform.position += direction * plSpeed * Time.deltaTime;
        }
        else
        {
            //Debug.Log("Blocked by something.");
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
        if (isTrapped)  //Trapの場合
        {
            nowEscapeClick++;   //Bボタン押したらEscapeClick回数1増える
                                

            if (nowEscapeClick >= maxEscapeClick)
            {
                ForceEscape();
            }
        }
    }

    protected override void UpButtonB() { }

    protected override void OnButtonX()
    {
        if (!isTrapped)     //Trap状況ではない場合現在位置にwaterbomb設置
        {
            ThrowBomb();
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
            //今のプレイヤの位置に水風船配置
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


    IEnumerator TrapSequence()
    {
        isTrapped = true;
        moveVec = Vector3.zero; //動けない
        nowEscapeClick = 0;     //escapeボタン初期化
        trapTime = 3.0f;



        while (trapTime > 0 && isTrapped)
        {
            trapTime -= Time.deltaTime;

            yield return null;
        }

        if (isTrapped)
        {
            Escape();
        }
    }

    void ForceEscape()
    {
        if (isTrapped)
        {
            StopAllCoroutines();
            Escape();
        }
    }

    void Escape()
    {
        isTrapped = false;  //waterbombから脱出
        nowEscapeClick = 0; //escape Button初期化

       
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
                GetTrapped(effect.ownerId);
            }
        }

        if(other.CompareTag("Wall"))
        {
            transform.position -= moveVec * plSpeed * Time.deltaTime;
           
        }
    }

    public void GetTrapped(int ownerplayerId)
    {
        if (!isTrapped)
        {
            
            StartCoroutine(TrapSequence());

            if (ownerplayerId != playerId)  //自分のWATERBOMBじゃなかったら
            {
                Ooo_SceneManager.AddScore(ownerplayerId);
            }
        }
    }

   
}

 //--------------------------------------------------------
