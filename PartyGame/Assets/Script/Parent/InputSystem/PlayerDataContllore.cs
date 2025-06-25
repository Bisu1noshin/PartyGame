using System.Collections.Generic;
using UnityEngine;

public class PlayerDataContllore : MonoBehaviour
{
    public static PlayerDataContllore PlayerDataContllore_instance;
    public int PlayerLength;

    private List<PlayerDate> playerDates;

    private void Awake()
    {
        CheckInstance();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerDates = new List<PlayerDate>();
    }

    private void CheckInstance()
    {
        if (PlayerDataContllore_instance == null)
        {
            PlayerDataContllore_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializePlayerDates(List<PlayerDate> dates)
    {   
        // データの登録
        playerDates = dates;

        // プレイヤーの人数
        PlayerLength = dates.Count;
    }

    public List<PlayerDate> GetPlayerDate()
    {
        return playerDates;
    }
}
