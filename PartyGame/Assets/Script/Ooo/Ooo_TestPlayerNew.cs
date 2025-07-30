using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class Ooo_TestPlayerNew : PlayerParent
{
    float plSpeed = 10.0f;
    Vector3 moveVec;

    public GameObject waterbombPrefab;
    public GameObject explodeEffectPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterbombPrefab = Resources.Load<GameObject>("Ooo/waterbomb_Prefab");
        explodeEffectPrefab = Resources.Load<GameObject>("Ooo/explodeEffect");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void MoveUpdate(Vector2 vec)
    {
      
    }

    protected override void LookUpdate(Vector2 vec)
    {

    }

    protected override void OnButtonA()
    {
        //Debug.Log("user" + playerData.GetUserValue() + "OnButtonA");

    }

    protected override void UpButtonA() { }

    protected override void OnButtonB()
    {
       
    }

    protected override void UpButtonB() { }

    protected override void OnButtonX()
    {
       
    }

    protected override void UpButtonX() { }

    protected override void OnButtonY() { }

    protected override void UpButtonY() { }
}
