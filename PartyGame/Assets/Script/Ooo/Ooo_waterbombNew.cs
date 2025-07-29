using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ooo_waterbombNew : MonoBehaviour
{
    public GameObject explodeEffectPrefab;
    private int ownerId;

    private Renderer rend;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        StartCoroutine(ExplodeRoutine());
    }

    public void Initialize(int ownerPlayerId)
    {
        ownerId = ownerPlayerId;
        explodeEffectPrefab = Resources.Load<GameObject>("Ooo/explodeEffect"); // 예시 경로
    }

    IEnumerator ExplodeRoutine()
    {
        float blinkDuration = 3f;
        float blinkInterval = 0.2f;
        float timer = 0f;

        // 깜빡이기
        while (timer < blinkDuration)
        {
            rend.enabled = !rend.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        // 폭발!
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
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
                0.5f,
                Mathf.Round(transform.position.z + dir.z)
            );

            GameObject effect = Instantiate(explodeEffectPrefab, spawnPos, Quaternion.identity);

            if (effect.TryGetComponent<Ooo_ExplodeEffect>(out var explode))
            {
                explode.ownerId = ownerId;
            }
        }
    }
}

