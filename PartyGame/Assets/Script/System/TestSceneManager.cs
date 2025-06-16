using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject playerprefab;
    [SerializeField] private Component playerScript;

    private List<PlayerData> playerDatas;
    private GameObject[] player;

    private void Start()
    {
        playerDatas = PlayerDataContllore.PlayerDataContllore_instance.GetPlayerDate();
        player = new GameObject[playerDatas.Count];

        for (int i = 0; i < player.Length; i++)
        {

            if (player[i] == null)
            {

                player[i] = CreatePlayer(playerDatas[i]);
            }
        }
    }

    private void Update()
    {
       
    }

    private GameObject CreatePlayer(PlayerData pd) {

        GameObject go = playerprefab;
        if (!go.GetComponent<TestPlayer>()) {
            go.AddComponent<TestPlayer>();
        }
        return PlayerParent.CreatePlayer(go, pd);
    }
}
