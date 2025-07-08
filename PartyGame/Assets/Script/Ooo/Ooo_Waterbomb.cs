using UnityEngine;
using System.Collections;

public class Ooo_Waterbomb : MonoBehaviour
{
    //プレイヤーが水風船を置くと、点滅して爆発した範囲内の相手プレイヤー囲む

    public float explodeTime = 3f;      //3秒経ったら爆発
    public float explodeRadius = 3f;    //爆発範囲

    public GameObject expoldeEffect;    //爆発エフェクトPrefab

    private int waterBombPlayerId;      //誰がwaterbombを置いたのか保存

    void Start()
    {
        
    }

    public void Initialize(int playerId)    //waterbombがおかれれば
    {
        waterBombPlayerId = playerId;       //誰が置いたのか保存
        StartCoroutine(ExplodeRoutine());   //爆発課程スタート
    }

    void Update()
    {
        
    }

    IEnumerator ExplodeRoutine()
    {
        float blinkTime = 1.5f;     //1.5秒間点滅
        yield return new WaitForSeconds(blinkTime);

        //置いたら1.5秒後点滅→1.5秒間点滅後爆発
        Explode();
    }

    void Explode()
    {

    }
}
