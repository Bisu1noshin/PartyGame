using UnityEngine;

public class Kameda_UIManager : MonoBehaviour
{
    public static GameObject Create(GameObject gameObject)
    {
        if (gameObject == null) { return null; }
        GameObject go = Instantiate(gameObject);
        go.transform.SetParent(GameObject.Find("Canvas").transform, false);
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localPosition = Vector3.zero;
        return go;
    }
    public static GameObject Create(GameObject gameObject, string name)
    {
        GameObject go = Create(gameObject);
        go.name = name;
        return go;
    }

    public static void Destroy(string name)
    {
        if (GameObject.Find(name) == null) { return; }
        Destroy(GameObject.Find(name));
    }

}
