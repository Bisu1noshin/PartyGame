using UnityEngine;
using UnityEngine.InputSystem;

public class Onishi_TestPlayer : PlayerParent
{
    Vector3 moveVec;
    float plSpeed = 10.0f;

    int bombCnt = 0; //手榴弾
    public GameObject AtkBomb_Prefab; //爆弾のプレファブ
    private GameObject SetBomb; //実体化した爆弾 自発的に爆発させる用

    protected override void Start()
    {
        base.Start();
        AtkBomb_Prefab = Resources.Load<GameObject>("Onishi/AtkBombPrefab");
    }

    private void Update()
    {
        //移動
        transform.position += moveVec * plSpeed * Time.deltaTime;

        //Escapeでデバッグモードを抜けるだけ
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
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
        if (bombCnt >= 1 && SetBomb == null) 
        {
            //爆弾設置
            Vector3 pos = transform.position;
            GameObject go = AtkBomb_Prefab;
            SetBomb = Instantiate(go, pos, Quaternion.identity);
        }

        else if (SetBomb != null)
        {
            //起爆
            SetBomb.GetComponent<Onishi_BombShoot>().Explosion();
            SetBomb = null;
        }
    }

    protected override void UpButtonA() { }

    protected override void OnButtonB() { }

    protected override void UpButtonB() { }

    protected override void OnButtonX() { }

    protected override void UpButtonX() { }

    protected override void OnButtonY() { }

    protected override void UpButtonY() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Onishi_PopBomb>(out Onishi_PopBomb val) == true) //爆弾の種類を指定
        {
            //回収
            bombCnt++;
            Destroy(other.gameObject);
        }
    }
}
