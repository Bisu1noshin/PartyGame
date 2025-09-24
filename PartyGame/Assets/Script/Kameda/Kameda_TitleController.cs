using UnityEngine;

public class Kameda_TitleController : MonoBehaviour
{
    float timer = 0.0f;
    void Start()
    {
        transform.localScale = Vector3.one * 0.5f;
        transform.Rotate(new Vector3(0, 0, -5));
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
