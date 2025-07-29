using UnityEngine;
using UnityEngine.AI;
using System;
using Unity.VisualScripting;

public class Oni_Script : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField]public Transform[] playersPos = new Transform[4];
    Kameda_TestSceneManager parent;
    public static Oni_Script instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

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
        if(parent.GetGameState() != GameState.Play) { return; }
        agent.SetDestination(playersPos[SelectTargetPlayer()].position);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Player_Instant>(out var p))
        {
            p.OnCaught();
        }
    }
    int SelectTargetPlayer()
    {
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
                continue;
            }
            if (ranges[i] > ranges[selectnum])
            {
                selectnum = i;
            }
        }
        return selectnum;
    }
    public void SetParentManager(Kameda_TestSceneManager kt)
    {
        parent = kt;
    }
}