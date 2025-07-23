using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class TitleController : MonoBehaviour
{
    [SerializeField] GameObject text_Title;
    [SerializeField] GameObject text_Next;

    bool isFinishTitleMove = false; //タイトルの動きを最後まで見せるためのフラグ
    private void Start()
    {
        //最初はPressAnyKeyを透明に
        Color c = text_Next.GetComponent<TextMeshProUGUI>().color;
        c.a = 0f;
        text_Next.GetComponent<TextMeshProUGUI>().color = c;

        //動かす
        var seq = DOTween.Sequence();
        //タイトルを下から上へ(ワンバウンド)
        seq.Append(text_Title.transform.DOLocalMoveY(170f, 2f)
                                       .SetEase(Ease.OutBack)
                                       .OnComplete(() => isFinishTitleMove = true));
        //PressAnyKeyを点滅(無限に)
        seq.Append(text_Next.GetComponent<TextMeshProUGUI>().DOFade(1f, 2f)
                                                            .SetEase(Ease.Flash, 2)
                                                            .SetLoops(-1, LoopType.Restart)); 
    }
    void Update()
    {

        if (Input.anyKeyDown && isFinishTitleMove == true) 
        {
            SceneManager.LoadScene("SetPlayerScene");
        }
    }
}
