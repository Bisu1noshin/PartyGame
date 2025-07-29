using UnityEngine;

public class WaterbombScript : MonoBehaviour
{
    public float explodeDelay = 3f;
    public float blinkSpeed = 5f;
    public GameObject explodeEffectPrefab;

    private int ownerId;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        explodeEffectPrefab = Resources.Load<GameObject>("Ooo/explodeEffect");

        StartCoroutine(BlinkAndExplode());
    }

    public void Initialize(int playerIndex)
    {
        ownerId = playerIndex;
    }

    private System.Collections.IEnumerator BlinkAndExplode()
    {
        float timer = 0f;

        while (timer < explodeDelay)
        {
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);
            if (meshRenderer != null)
            {
                Color color = meshRenderer.material.color;
                color.a = Mathf.Lerp(0.3f, 1.0f, alpha);
                meshRenderer.material.color = color;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        Explode();
        Destroy(gameObject); // 본체 제거
    }

    private void Explode()
    {
        Vector3[] directions = {
            Vector3.forward, Vector3.back, Vector3.left, Vector3.right
        };

        foreach (var dir in directions)
        {
            Vector3 spawnPos = transform.position + dir;
            GameObject effect = Instantiate(explodeEffectPrefab, spawnPos, Quaternion.identity);

            // 이펙트 소유자 정보 전달
            Ooo_ExplodeEffect effectScript = effect.GetComponent<Ooo_ExplodeEffect>();
            if (effectScript != null)
            {
                effectScript.Initialize(ownerId);
            }
        }
    }
}