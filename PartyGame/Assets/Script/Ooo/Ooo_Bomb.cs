using UnityEngine;
using UnityEngine.InputSystem;

public class Ooo_Bomb : MonoBehaviour
{
    GameObject bomb;
    Vector3 spawnPos;
    

    void Start()
    {

        Instantiate(bomb);
        spawnPos = transform.position;
    }

    void Update()
    {
        
    }
}
 