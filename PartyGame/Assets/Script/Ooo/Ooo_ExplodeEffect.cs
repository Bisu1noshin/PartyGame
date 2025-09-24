using UnityEngine;
using System.Collections;

public class Ooo_ExplodeEffect : MonoBehaviour
{
    public float lifeTime = 1f;
    public int ownerId;  // エフェクトを生成したプレイヤー

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Ooo_TestPlayer player = other.GetComponent<Ooo_TestPlayer>();
        if (player != null)
        {
            player.Trap(ownerId);
        }
        else
        {
            Debug.Log($"Collided with non-player object: {other.name}");
        }
    }
}
