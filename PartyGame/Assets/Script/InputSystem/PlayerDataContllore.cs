using System.Collections.Generic;
using UnityEngine;

public class PlayerDataContllore : MonoBehaviour
{

    public static PlayerDataContllore PlayerDataContllore_instance;
    public int PlayerLengh;

    private List<PlayerData> playerDatas;

    private void Awake()
    {
        CheckInstance();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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

    public void InitializePlayerDatas(List<PlayerData> datas)
    {

        // データの全削除
        playerDatas.Clear();

        // プレイヤーの人数
        PlayerLengh = datas.Count;
        
        // データの登録
        playerDatas = datas;
    }

    public List<PlayerData> GetPlayerDate()
    {
        return playerDatas;
    }
}
