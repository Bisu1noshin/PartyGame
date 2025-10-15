using UnityEngine;
using System.Collections;

public class Ooo_waterbombNew : MonoBehaviour
{
    public GameObject explodeEffectPrefab;
    private int ownerId;

    private Renderer[] rend;

    AudioSource audioSource;
    AudioClip explodeSound;

    void Start()
    {
        rend = GetComponentsInChildren<Renderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        
        explodeSound = Resources.Load<AudioClip>("Ooo/explode");
        StartCoroutine(ExplodeRoutine());
    }

    public void Initialize(int ownerPlayerId, Vector3 spawnPos)
    {
        ownerId = ownerPlayerId;
        transform.position = spawnPos;  //生成位置
        explodeEffectPrefab = Resources.Load<GameObject>("Ooo/explodeEffect");
    }

    IEnumerator ExplodeRoutine()
    {
        float totalDelay = 3f;      //total生成時間３秒
        float blinkStartTime = 2f;  //生成２秒後点滅 Start
        float blinkInterval = 0.2f; //0.2秒間隔で点滅
        float timer = 0f;           //生成後経過時間

        //点滅
        while (timer < blinkStartTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // 1秒間点滅
        float blinkTimer = 0f;
        while (timer < totalDelay)
        {
            foreach (var r in rend)
                r.enabled = !r.enabled;

            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
            blinkTimer += blinkInterval;
        }

        //waterbomb爆発
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        audioSource.PlayOneShot(explodeSound);
        if (explodeEffectPrefab == null)
        {
            Debug.Log("Explode Effect null.");
            return;
        }

        //Effect 十字方向生成
        Vector3[] directions = new Vector3[]
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right,
            Vector3.zero
        };

        foreach (var dir in directions)
        {
            Vector3 spawnPos = new Vector3(
                Mathf.Round(transform.position.x + dir.x),
                0f,
                Mathf.Round(transform.position.z + dir.z)
            );

            Collider[] hits = Physics.OverlapBox(spawnPos, Vector3.one * 0.4f);
            bool hitWall = false;

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Wall"))  //Wallあったら生成しない
                {
                    hitWall = true;
                    break;
                }
            }

            if (hitWall) continue;  //Wallならスキップ

            GameObject effect = Instantiate(explodeEffectPrefab, spawnPos, Quaternion.identity);

            if (effect.TryGetComponent<Ooo_ExplodeEffect>(out var explode))
            {
                explode.ownerId = ownerId;
            }
            Destroy(effect, 4f);
        }
    }
}

