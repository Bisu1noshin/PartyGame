using UnityEngine;
using System.Collections;

public class Ooo_ExplodeEffect : MonoBehaviour
{
    GameObject explodeEffectPrefab;

    public void Initialize()
    {
        StartCoroutine(ExplodeEffectRoutine());   //爆発課程スタート
    }

    IEnumerator ExplodeEffectRoutine()
    {
        //2秒間エフェクト出て消える
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

}
