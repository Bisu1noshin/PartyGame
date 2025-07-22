using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class Onishi_TextMovement : MonoBehaviour
{
    private TMP_Text text;

    private void Start()
    {
        text = this.GetComponent<TMP_Text>();

        //テキストを透明に
        Color color = text.color;
        color.a = 0f;
        text.color = color;
        text.transform.localScale = Vector3.zero;

        //Sequenceで管理
        var seqText = DOTween.Sequence();

        //文字表示
        seqText.Append(text.DOFade(1f, 2f)
                           .SetEase(Ease.OutQuart));
        //文字拡大(上と同時)
        seqText.Join(text.transform.DOScale(1f, 2f)
                                   .SetEase(Ease.OutQuart));
        //文字を消す 終了時に関数呼び出し
        seqText.Append(text.DOFade(0f, 1f)
                           .SetEase(Ease.OutQuart)
                           .OnComplete(Complete));
        //文字拡大(上と同時)
        seqText.Join(text.transform.DOScale(1.5f, 1f)
                                   .SetEase(Ease.OutQuart)); 
    }

    void Complete()
    {
        Destroy(this.gameObject); //完全に消えたらGameObjectごとさよなら
    }
}
