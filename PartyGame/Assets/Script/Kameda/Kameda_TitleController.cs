using UnityEngine;

public class Kameda_TitleController : MonoBehaviour
{
    float timer;
    private void Awake()
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.zero;
        transform.Rotate(new Vector3(0, 0, -5));
        timer = 0;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0.5f)
        {
            transform.localScale += new Vector3(3, 3, 0) * Time.deltaTime;
        }
        if(timer <= 1.0f)
        {
            transform.Rotate(new Vector3(0, 0, 15) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -15) * Time.deltaTime);
        }
        if(timer >= 1.8f)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }
}
