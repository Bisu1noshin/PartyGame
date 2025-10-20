using UnityEngine;
public class Light_Script : MonoBehaviour
{
    public GameObject player;
    Light mLight;
    public float lightColor;
    private void Awake()
    {
        gameObject.name = "Light_Player";
        lightColor = 1;
        transform.position = new Vector3(0, 8, 0);
        mLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        if((int)(Kameda_SceneManager.Instance.State) < 2) { return; }

        mLight.enabled = true;
        transform.LookAt(player.transform.position);

        GetComponent<Light>().color = new Color(1, lightColor, lightColor, 1);

    }
}
