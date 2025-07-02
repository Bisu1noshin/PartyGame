using UnityEngine;
using UnityEngine.UIElements;

public class Onishi_BombShoot : MonoBehaviour
{
    [SerializeField]float explosionTime = 9.0f; //爆発までの時間
    float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        //一定時間経過で爆発
        timer += Time.deltaTime;
        if (timer > explosionTime)
        {
            Explosion();
        }
    }

    //爆発時の処理
    public void Explosion()
    {
        Debug.Log("爆発");
        Destroy(gameObject);
    }
}
