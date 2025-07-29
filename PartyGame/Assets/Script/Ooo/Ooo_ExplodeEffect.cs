using UnityEngine;
using System.Collections;

public class Ooo_ExplodeEffect : MonoBehaviour
{
    public float lifeTime = 1f;
    public int ownerId;  // 이펙트를 생성한 플레이어 ID

    public void Initialize(int playerId)
    {
        ownerId = playerId;
        Destroy(gameObject, lifeTime);
    }
}
