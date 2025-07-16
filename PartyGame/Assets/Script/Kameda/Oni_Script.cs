using UnityEngine;
using UnityEngine.AI;
using System;
using Unity.VisualScripting;

public class Oni_Script : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField]public Transform[] playersPos = new Transform[4];
    private void Awake()
    {
        gameObject.name = "Oni";
        transform.position = new Vector3(-5, -0.75f, 4);
        agent = gameObject.GetOrAddComponent<NavMeshAgent>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playersPos[0] == null) { return; }
        float[] ranges = new float[4];
        for (int i = 0; i < playersPos.Length; i++)
        {
            if (playersPos[i] != null)
            {
                ranges[i] = (playersPos[i].position - transform.position).sqrMagnitude;
            }
            else
            {
                ranges[i] = -1.0f;
            }
        }
        int selectnum = 0;
        for (int i = 0; i < ranges.Length; i++)
        {
            if (ranges[i] == -1.0f)
            {
                break;
            }
            if (ranges[i] > ranges[selectnum])
            {
                selectnum = i;
            }
        }
        agent.SetDestination(playersPos[selectnum].position);

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("捕まった！");
        }
    }
}