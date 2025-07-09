using UnityEngine;
using System.Collections;

public class Ooo_Waterbomb : MonoBehaviour
{
    //プレイヤーが水風船を置くと、点滅して爆発した範囲内の相手プレイヤー囲む

    public float explodeTime = 3f;      //3秒経ったら爆発
    public float explodeRadius = 3f;    //爆発範囲

    public GameObject waterbombPrefab;
    public GameObject explodeEffectPrefab;    //爆発エフェクトPrefab

    private int waterBombPlayerId;      //誰がwaterbombを置いたのか保存

    void Start()
    {
        waterbombPrefab = Resources.Load<GameObject>("Ooo/waterbomb");
        explodeEffectPrefab = Resources.Load<GameObject>("Ooo/explodeEffect");
    }

    public void Initialize(int playerId)    //waterbombがおかれれば
    {
        waterBombPlayerId = playerId;       //誰が置いたのか保存
        StartCoroutine(ExplodeRoutine());   //爆発課程スタート
        StartCoroutine(ExplodeEffectRoutine());
    }

    IEnumerator ExplodeRoutine()
    {
        float blinkTime = 1.5f;     //1.5秒間点滅
        yield return new WaitForSeconds(blinkTime);

        //置いたら1.5秒後点滅 → また1.5秒経ったら爆発
        Explode();
    }

    void Explode()
    {
        Destroy(waterbombPrefab);
        GameObject explodeEffect = Instantiate(explodeEffectPrefab, transform.position, Quaternion.identity);

        //StartCoroutine(ExplodeEffectRoutine());

    }

    IEnumerator ExplodeEffectRoutine()
    {
        yield return new WaitForSeconds (2f);

        Destroy(explodeEffectPrefab);
    }


}
