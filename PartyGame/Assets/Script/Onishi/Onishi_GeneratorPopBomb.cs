using UnityEngine;

public class Onishi_GeneratorPopBomb : MonoBehaviour
{
    [SerializeField] GameObject PopBombPrefab; //ジェネレート対象のプレファブ
    Onishi_TestSceneManager sceneManager; //シーンマネージャー
    const float xRange = 13.0f;
    const float zRange = 9.0f; //xとzの範囲
    const float zOffset = 7.0f; //zのオフセット

    float spawnRate = 5.0f;//スポーン間隔
    float timer;
    
    private void Start()
    {
        timer = spawnRate;
        sceneManager = GameObject.Find("TestSceneManager").GetComponent<Onishi_TestSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneManager.isPlaying() == true)
        {
            timer += Time.deltaTime;
            if (timer >= spawnRate)
            {
                int bombCnt = Random.Range(5, 9); //爆弾の設置個数を決定
                for (int i = 0; i < bombCnt; ++i)
                {
                    Vector3 pos = new Vector3(Random.Range(-xRange, xRange), 0, Random.Range(-zRange, zRange) + zOffset); //範囲を絞って位置決定
                    Instantiate(PopBombPrefab, pos, Quaternion.identity); //生成
                }
                timer = 0.0f;
            }
        }
    }
}
