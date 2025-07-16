using UnityEngine;
using TMPro;
using System.Collections;

public class Onishi_DoTweenTest : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        StartCoroutine(Simple());
    }

    private IEnumerator Simple()
    {
        text.maxVisibleCharacters = 0;

        for(var i = 0; i < text.text.Length; ++i)
        {
            yield return new WaitForSeconds(0.2f);
            text.maxVisibleCharacters = i + 1;
        }
    }
}