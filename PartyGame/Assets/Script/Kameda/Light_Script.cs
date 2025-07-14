using UnityEngine;
using UnityEngine.LightTransport;
public class Light_Script : MonoBehaviour
{
    public GameObject player;
    public float lightColor;
    private void Awake()
    {
        gameObject.name = "Light_Player1";
        lightColor = 1;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        Vector3 Ppos = player.transform.position;
        transform.LookAt(player.transform.position);

        GetComponent<Light>().color = new Color(1, lightColor, lightColor, 1);

    }
}
