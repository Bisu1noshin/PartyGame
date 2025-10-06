using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Kameda_CntController : MonoBehaviour
{
    public static Kameda_CntController Instance { get; private set; }
    string txt;
    TextMeshProUGUI tm;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        tm = GetComponent<TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {
        tm.text = txt;
    }
    public void SetText(string t)
    {
        txt = t;
    }
}
