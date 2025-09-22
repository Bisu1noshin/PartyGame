using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Kameda_CntDnController : MonoBehaviour
{
    public static Kameda_CntDnController Instance { get; private set; }
    string txt;
    TextMeshProUGUI tm;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        tm = GetComponent<TextMeshProUGUI>();
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localPosition = new Vector3(0, 220, 0);
        transform.localScale = new Vector3(1, 2, 1);
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
