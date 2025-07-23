using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

public class Ooo_TestPlayer : PlayerParent
{
    Vector3 moveVec;

    //プレイヤー関連設定
    [Header("Player Settings")]
    float plSpeed = 10.0f;
    public GameObject waterbombPrefab;
    public GameObject explodeEffectPrefab;

    //囲まれたら時の設定
    [Header("Trap Settings")]
    public float trapTime = 3f;    //基本爆発時間は3秒(囲まれたら3秒）
    public int maxEscapeClick = 10;     //最大連打可能回数（3秒内に10回押したら脱出可能）

    public bool isTrapped = false;    //相手のWaterbombに囲まれたか
    public static int nowEscapeClick = 0;   //現在脱出ボタンを押した回数
    public int playerId;
    public int score = 0;




    protected void Start()
    {
        waterbombPrefab = Resources.Load<GameObject>("Ooo/waterbomb");
        explodeEffectPrefab = Resources.Load<GameObject>("Ooo/explodeEffect");

        playerId = playerInput.playerIndex;




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
        //Debug.Log("user" + playerData.GetUserValue() + "OnButtonA");
    }

    protected override void UpButtonA() { }

    protected override void OnButtonB()
    {
        if (isTrapped)  //囲まれたら
        {
            nowEscapeClick++;   //Bボタン押すたびに +1

            if (nowEscapeClick >= maxEscapeClick)    //3秒内に10回以上押したら
            {
                ForceEscape();   //水風船から脱出！
            }
        }
    }

    protected override void UpButtonB() { }

    protected override void OnButtonX()
    {
        if (!isTrapped)     //囲まれてなかったら
        {
            ThrowBomb();    //Aボタン押したらWaterbombをプレイヤの現在位置に置く
        }
    }

    protected override void UpButtonX() { }

    protected override void OnButtonY() { }

    protected override void UpButtonY() { }


    void ThrowBomb()    //Waterbombを置く関数
    {
        if (waterbombPrefab != null)
        {
            //今のプレイヤの位置に水風船配置
            GameObject waterbomb = Instantiate(waterbombPrefab, transform.position, Quaternion.identity);

            //誰がwaterbombを配置したのか(waterbombのIDを与える)
            Ooo_Waterbomb ooo_waterbomb = waterbomb.GetComponent<Ooo_Waterbomb>();  //Waterbombスクリプト
            if (ooo_waterbomb != null)
            {
                ooo_waterbomb.Initialize(playerInput.playerIndex);
            }

        }
    }


    IEnumerator TrapSequence()
    {
        isTrapped = true;   //囲まれた状態で更新
        moveVec = Vector3.zero; //動けない
        nowEscapeClick = 0;     //escapeボタン初期化

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
        //Debug.Log("Escapeできました");
    }

    void Escape()
    {
        isTrapped = false;  //水風船から脱出
        nowEscapeClick = 0; //Bボタン押した回数初期化
    }

    public bool IsTrapped()
    {
        return isTrapped;
    }

    void AddScore()
    {
        score++;
        Debug.Log(score.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("explodeEffect") && !isTrapped)
        {
            GetTrapped(playerId);
        }
    }

    public void GetTrapped(int ownerPlayerId)
    {
        if (isTrapped = true)
        {
            moveVec = Vector3.zero; //動けない

            if (ownerPlayerId != playerId)  //自分のwaterBombじゃなかったら Score +1
            {
                //PlayerIdの中でownerPlayerIdを持ってるplayer
                Ooo_TestPlayer[] players = FindObjectsOfType<Ooo_TestPlayer>();
                foreach (var player in players)
                {
                    if (player.playerId == ownerPlayerId)
                    {
                        player.AddScore();
                        break;
                    }
                }

            }
        }
    }
}
