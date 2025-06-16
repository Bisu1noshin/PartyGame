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
    }

    private void Update()
    {
        if (Input.anyKeyDown) {

            for (int i = 0; i < player.Length; i++) {

                if (player[i] == null) {

                    player[i] = CreatePlayer();
                    break;
                }
            }
        }
    }

    private GameObject CreatePlayer() {

        if (player == null) { }

        GameObject go = playerprefab;
        go.AddComponent<TestPlayer>();

        return Instantiate(go);
    }
}
