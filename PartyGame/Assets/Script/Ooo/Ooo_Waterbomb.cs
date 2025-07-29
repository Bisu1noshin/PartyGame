using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Ooo_Waterbomb : MonoBehaviour
{
    [Header("waterbomb Settings")]
    public float explodeRange = 2f;    //爆発範囲

    [Header("Visual Effects")]
    public GameObject waterbombPrefab;
    public GameObject explodeEffectPrefab;
    private MeshRenderer meshRenderer;
    public Color[] blinkColors = { Color.white, Color.red }; //点滅時の色変更

    private int waterBombPlayerId;      //設置したプレイヤーId
    private SphereCollider explodeCollider;   //爆発範囲当たり判定
    public int playerIndex;

    void Start()
    {
        waterbombPrefab = Resources.Load<GameObject>("Ooo/waterbomb_Prefab");
        explodeEffectPrefab = Resources.Load<GameObject>("Ooo/explodeEffect");


        explodeCollider = GetComponent<SphereCollider>();
        if(explodeCollider == null)
        {
            explodeCollider = gameObject.AddComponent<SphereCollider>();
        }

        explodeCollider.isTrigger = true;   //範囲内当たり判定
        explodeCollider.radius = explodeRange;
        explodeCollider.enabled = false;    //爆発する時のみOn

        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    //設置時初期化
    public void Initialize(int playerId)    
    {
        waterBombPlayerId = playerId;       //設置したプレイヤーId保存
        StartCoroutine(ExplodeRoutine());   //爆発課程スタート
    }

    IEnumerator ExplodeRoutine()        //爆発前点滅過程
    {
        yield return new WaitForSeconds(1.5f);  //waterbomb設置直後1.5秒はただ待機

        float blinkTime = 1.5f;
        float timeCnt = 0f;
        float minBlinkTime = 0.02f;
        float maxBlinkTime = 0.2f;
        int colorIndex = 0;

        while(timeCnt < blinkTime)      //残り1.5秒間どんどん早く点滅
        {
            meshRenderer.material.color = blinkColors[colorIndex % blinkColors.Length];
            colorIndex++;

            //点滅間隔が0.5秒→0.1秒でどんどん早くなる
            float t = timeCnt / blinkTime; //0.0 ～ 1.0の間の値
            float blinkSpeed = Mathf.Lerp(maxBlinkTime, blinkTime, t);

            yield return new WaitForSeconds(blinkSpeed);
            timeCnt += blinkSpeed;
        }
        //置いたら1.5秒後点滅 → また1.5秒経ったら爆発
        Explode();
    }

    void Explode()
    {
        CreateExplodeEffect(transform.position);    //中心エフェクト生成

        //爆発範囲方向配列
        Vector3[] directions =
        {
            Vector3.forward,    //+Z
            Vector3.back,       //-Z
            Vector3.right,      //+X
            Vector3.left        //-X
        };

        foreach(Vector3 dir in directions)  
        {
            RaycastHit hit;

            for(int i = 1; i <= explodeRange; i++)  //爆発範囲
            {
                Vector3 offset = dir * i;
                Vector3 targetPos = new Vector3(
                    transform.position.x + offset.x,
                    transform.position.y,
                    transform.position.z + offset.z);

                if(Physics.Raycast(transform.position, dir, out hit, i))//障害物(壁)あったらEffect生成しない
                {
                    if(hit.collider.CompareTag("Wall"))
                    {
                        break;
                    }
                }
                CreateExplodeEffect(targetPos);
            }
        }

        StartCoroutine(DestroyAfterExplode());
    }

    void CreateExplodeEffect(Vector3 pos)
    {
        if(explodeEffectPrefab != null)
        {
            GameObject eff = Instantiate(explodeEffectPrefab, pos, Quaternion.identity);
            eff.tag = "explodeEffect";
        }
    }

    IEnumerator DestroyAfterExplode()
    {
        yield return new WaitForSeconds (0.1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) //爆発時プレイヤ当たり判定
    {
        Ooo_TestPlayer player = other.GetComponent<Ooo_TestPlayer>();

        if(player != null && player.IsTrapped())
        {
            player.GetTrapped(waterBombPlayerId);

            if(player.playerId != waterBombPlayerId)
            {
                Ooo_SceneManager sceneManager = FindObjectOfType<Ooo_SceneManager>();
                if(sceneManager != null)
                {
                    sceneManager.AddScore(waterBombPlayerId);
                }
                else
                {
                    Debug.LogWarning("SceneManager not found");
                }
            }
        }
        
    }
}
