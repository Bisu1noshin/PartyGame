using UnityEngine;
public class Light_Script : MonoBehaviour
{
    public GameObject player;
    public float lightColor;
    private void Awake()
    {
        gameObject.name = "Light_Player";
        lightColor = 1;
        transform.position = new Vector3(0, 8, 0);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        transform.LookAt(player.transform.position);

        GetComponent<Light>().color = new Color(1, lightColor, lightColor, 1);

    }
}
