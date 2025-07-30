using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

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
        transform.position = spawnPos;  // 위치 명확히 지정
        explodeEffectPrefab = Resources.Load<GameObject>("Ooo/explodeEffect");
    }

    IEnumerator ExplodeRoutine()
    {
        float totalDelay = 3f;
        float blinkStartTime = 2f;
        float blinkInterval = 0.2f;
        float timer = 0f;

        

        // 깜빡이기
        while (timer < blinkStartTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // 1초 동안 깜빡이기 시작
        float blinkTimer = 0f;
        while (timer < totalDelay)
        {
            foreach (var r in rend)
                r.enabled = !r.enabled;

            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
            blinkTimer += blinkInterval;
        }

        // 폭발!
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        audioSource.PlayOneShot(explodeSound);
        Debug.Log($"Explode called at position: {transform.position}");
        if (explodeEffectPrefab == null)
        {
            Debug.LogWarning("Explode Effect Prefab not assigned.");
            return;
        }

        // 십자 방향 + 본인 위치
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
                if (hit.CompareTag("Wall"))  //タグがWallのオブジェクトと重なると生成しない
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

