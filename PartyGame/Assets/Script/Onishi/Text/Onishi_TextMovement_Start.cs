using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class Onishi_TextMovement_Start : MonoBehaviour
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
        text.text = "3";

        //Sequenceで管理
        var seqText = DOTween.Sequence();

        //3
        {
            //文字表示
            seqText.Append(text.DOFade(1f, 0.5f)
                               .SetEase(Ease.OutQuart));
            //文字拡大(上と同時)
            seqText.Join(text.transform.DOScale(1f, 0.5f)
                                       .SetEase(Ease.OutQuart));
            //文字を消す 終了時に関数呼び出し
            seqText.Append(text.DOFade(0f, 0.5f)
                               .SetEase(Ease.OutQuart)
                               .OnComplete(() => text.text = "2"));
            //文字拡大(上と同時)
            seqText.Join(text.transform.DOScale(1.5f, 0.5f)
                                       .SetEase(Ease.OutQuart));
        }
        //2
        {
            //文字表示
            seqText.Append(text.DOFade(1f, 0.5f)
                               .SetEase(Ease.OutQuart));
            //文字拡大(上と同時)
            seqText.Join(text.transform.DOScale(1f, 0.5f)
                                       .SetEase(Ease.OutQuart));
            //文字を消す 終了時に関数呼び出し
            seqText.Append(text.DOFade(0f, 0.5f)
                               .SetEase(Ease.OutQuart)
                               .OnComplete(() => text.text = "1"));
            //文字拡大(上と同時)
            seqText.Join(text.transform.DOScale(1.5f, 0.5f)
                                       .SetEase(Ease.OutQuart));
        }
        //1
        {
            //文字表示
            seqText.Append(text.DOFade(1f, 0.5f)
                               .SetEase(Ease.OutQuart));
            //文字拡大(上と同時)
            seqText.Join(text.transform.DOScale(1f, 0.5f)
                                       .SetEase(Ease.OutQuart));
            //文字を消す 終了時に関数呼び出し
            seqText.Append(text.DOFade(0f, 0.5f)
                               .SetEase(Ease.OutQuart)
                               .OnComplete(() => text.text = "Start!!"));
            //文字拡大(上と同時)
            seqText.Join(text.transform.DOScale(1.5f, 0.5f)
                                       .SetEase(Ease.OutQuart));
        }
        //Start
        {
            //文字表示
            seqText.Append(text.DOFade(1f, 0.5f)
                               .SetEase(Ease.OutQuart));
            //文字拡大(上と同時)
            seqText.Join(text.transform.DOScale(1f, 0.5f)
                                       .SetEase(Ease.OutQuart));
            //文字を消す 終了時に関数呼び出し
            seqText.Append(text.DOFade(0f, 0.5f)
                               .SetEase(Ease.OutQuart)
                               .OnComplete(Complete));
            //文字拡大(上と同時)
            seqText.Join(text.transform.DOScale(1.5f, 0.5f)
                                       .SetEase(Ease.OutQuart));
        }
    }

    void Complete()
    {
        Destroy(this.gameObject); //完全に消えたらGameObjectごとさよなら
    }
}
