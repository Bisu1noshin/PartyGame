using UnityEngine;
using System.Collections;

public class Ooo_ExplodeEffect : MonoBehaviour
{
    public float lifeTime = 1f;
    public int ownerId;  // 이펙트를 생성한 플레이어 ID

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // 폭발 이펙트끼리는 무시하고, 플레이어만 처리
        Ooo_TestPlayer player = other.GetComponent<Ooo_TestPlayer>();
        if (player != null)
        {
            player.GetTrapped(ownerId);
        }
        else
        {
            // 플레이어가 아닐 때는 무시
            Debug.Log($"Collided with non-player object: {other.name}");
        }
    }
}
