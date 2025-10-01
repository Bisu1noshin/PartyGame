using UnityEngine;
using UnityEngine.AI;
using System;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class Oni_Script : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] public Transform[] playersPos = new Transform[4];
    [SerializeField] Kameda_SceneManager parent;// 追記
    public static Oni_Script instance;
    int catchCnt;
    AudioSource biteSound;
    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        gameObject.name = "Oni";
        catchCnt = 0;
        transform.position = new Vector3(-5, -0.75f, 4);
        agent = gameObject.GetOrAddComponent<NavMeshAgent>();
        biteSound = GetComponent<AudioSource>();
        parent = Kameda_SceneManager.Instance.GetComponent<Kameda_SceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playersPos[0] == null) { return; }
        if(parent.State != GameState.Play) { return; }
        agent.SetDestination(playersPos[SelectTargetPlayer()].position);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Player_Instant>(out var p))
        {
            parent.Caughts.Add(p);
            parent.points[parent.PlayerNum[p]] = 3 - catchCnt++;
            biteSound.Play();
            p.OnCaught();
        }
    }
    int SelectTargetPlayer()
    {
        float currentRange;
        (int num, float range) currentTarget = new(0, 1e6f);
        for (int i = 0; i < playersPos.Length; i++)
        {
            if (playersPos[i] != null)
            {
                currentRange = (playersPos[i].position - transform.position).sqrMagnitude;
            }
            else
            {
                currentRange = 1e6f;
            }

            if (currentRange < currentTarget.range) { currentTarget = new(i, currentRange); }
        }
        return currentTarget.num;
    }
    // 追記

    //public void SetParentManager(Kameda_TestSceneManager kt)
    //{
    //    parent = kt;
    //}
}