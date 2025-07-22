using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class Onishi_DoTweenTest : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        //テキストを透明に
        Color color = text.color;
        color.a = 0f;
        text.color = color;

        var seqText = DOTween.Sequence();
        seqText.Append(text.DOFade(1f, 2f).SetEase(Ease.OutQuart));
        seqText.Append(text.DOFade(0f, 1f).SetEase(Ease.OutQuart).OnComplete(Complete));
    }

    void Complete()
    {
        Destroy(this.gameObject);
    }
}
