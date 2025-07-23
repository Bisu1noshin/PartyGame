using UnityEngine;
using UnityEngine.InputSystem;

public class Onishi_TestPlayer : PlayerParent
{
    Vector3 moveVec;
    float plSpeed = 10.0f;
    GameObject text_pt; //自分の現在の手榴弾入手数を表示するテキストボックスのオブジェクト

    int bombCnt = 0; //手榴弾
    public GameObject AtkBomb_Prefab; //爆弾のプレファブ
    private GameObject SetBomb; //実体化した爆弾 自発的に爆発させる
    Onishi_TestSceneManager sceneManager; //シーンマネージャー

    const float xRange = 13.0f;
    const float zRange = 9.0f; //xとzの範囲
    const float zOffset = 7.0f; //zのオフセット

    protected void Start()
    {
        AtkBomb_Prefab = Resources.Load<GameObject>("Onishi/AtkBombPrefab");

        //自分の名前を設定
        this.gameObject.name = "player" + playerInput.playerIndex.ToString();

        //自分の名前に応じたTextを探す
        text_pt = GameObject.Find("text_" + this.gameObject.name);

        //UI表示 名前を出す目的
        text_pt.GetComponent<Onishi_UIManager>().textUpdate(this.gameObject.name, bombCnt);

        //シーンマネージャーをバインド
        sceneManager = GameObject.Find("TestSceneManager").GetComponent<Onishi_TestSceneManager>();
    }

    private void Update()
    {
        if (sceneManager.isPlaying() == true) 
        {
            //移動
            Vector3 vec = transform.position;
            vec += moveVec * plSpeed * Time.deltaTime;
            if (vec.x > xRange || vec.x < -xRange)
            {
                vec.x = transform.position.x;
            }
            if(vec.z > zRange + zOffset || vec.z < -zRange + zOffset)
            {
                vec.z = transform.position.z;
            }
            transform.position = vec;
        }

        if (sceneManager.isFinish() == true) 
        {
            //ゲーム終了後、獲得爆弾数をシーンに渡す
            sceneManager.getBombCnt(playerInput.playerIndex, bombCnt);
        }
    }

    protected override void MoveUpdate(Vector2 vec)
    {
        //移動方向を決定
        moveVec = new Vector3(vec.x, 0, vec.y);
    }

    protected override void LookUpdate(Vector2 vec)
    {
        
    }

    protected override void OnButtonA()
    {
        if (sceneManager.isPlaying() == true)
        {
            if (bombCnt >= 1 && SetBomb == null)
            {
                //爆弾設置
                Vector3 pos = transform.position;
                GameObject go = AtkBomb_Prefab;
                SetBomb = Instantiate(go, pos, Quaternion.identity);
                bombCnt--;
                text_pt.GetComponent<Onishi_UIManager>().textUpdate(this.gameObject.name, bombCnt); //UI更新
            }

            else if (SetBomb != null)
            {
                //起爆
                SetBomb.GetComponent<Onishi_BombShoot>().Explosion();
                SetBomb = null;
            }
        }
    }

    protected override void UpButtonA() { }

    protected override void OnButtonB() { }

    protected override void UpButtonB() { }

    protected override void OnButtonX() { }

    protected override void UpButtonX() { }

    protected override void OnButtonY() { }

    protected override void UpButtonY() { }

    private void OnTriggerEnter(Collider other) //アイテム獲得処理
    {
        if (other.TryGetComponent<Onishi_PopBomb>(out Onishi_PopBomb val) == true && sceneManager.isPlaying() == true)  //爆弾の種類を指定
        {
            //回収
            bombCnt++;
            Destroy(other.gameObject);
            text_pt.GetComponent<Onishi_UIManager>().textUpdate(this.gameObject.name, bombCnt); //UI更新
        }
    }

    public void Damage() //被弾時処理
    {
        if (sceneManager.isPlaying() == true)
        {
            bombCnt -= 2;
            if (bombCnt <= 0)
            {
                bombCnt = 0;
            }
            text_pt.GetComponent<Onishi_UIManager>().textUpdate(this.gameObject.name, bombCnt); //UI更新
        }
    }
}
