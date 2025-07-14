using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using System.Threading.Tasks;

public class Onishi_BombShoot : MonoBehaviour
{
    [SerializeField]float explosionTime = 5.0f; //爆発までの時間
    float timer = 0.0f;
    bool isBomb = false;
    SphereCollider collider;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
        collider.enabled = false;
    }

    void Update()
    {
        //一定時間経過で爆発
        timer += Time.deltaTime;
        if (timer > explosionTime && isBomb == false) 
        {
            Explosion();
            isBomb = true;
        }
    }

    //爆発時の処理
    public async void Explosion()
    {
        collider.enabled = true;
        await Task.Delay(25);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Onishi_TestPlayer>(out Onishi_TestPlayer pl) == true) //プレイヤーだった時
        {
            pl.Damage();
        }
    }
}
