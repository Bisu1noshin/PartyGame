using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Onishi_UIManager : MonoBehaviour
{
    public void textUpdate(string plName, int point)
    {
        string text = plName + ":" + point.ToString();
        this.gameObject.GetComponent<TextMeshProUGUI>().SetText(text);
    }
}
