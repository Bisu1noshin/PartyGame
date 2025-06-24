using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerParent : MonoBehaviour
{
    private PartyGame inputAction;
    protected PlayerData playerData;

    private void Awake()
    {
        // inputManagerをインスタンス化
        inputAction = new PartyGame();

        // 関数をバインド
        {
            inputAction.Player.ButtonA.started += EnterButtonA;
            inputAction.Player.ButtonA.canceled += ExitButtonA;
            inputAction.Player.ButtonB.started += EnterButtonB;
            inputAction.Player.ButtonB.canceled += ExitButtonB;
            inputAction.Player.ButtonX.started += EnterButtonX;
            inputAction.Player.ButtonX.canceled += ExitButtonX;
            inputAction.Player.ButtonY.started += EnterButtonY;
            inputAction.Player.ButtonY.canceled += ExitButtonY;
            inputAction.Player.LeftStick.started += OnLeftStic;
            inputAction.Player.LeftStick.performed += OnLeftStic;
            inputAction.Player.RightStick.started += OnRightStic;
            inputAction.Player.RightStick.performed += OnRightStic;
        }
        
        // Actionを有効化
        inputAction.Enable();
    }

    protected virtual void Start() {


    }

    private void OnDestroy()
    {
        inputAction.Disable();
    }

    // 抽象メソッド

    abstract protected void OnButtonA();
    abstract protected void UpButtonA();
    abstract protected void OnButtonB();
    abstract protected void UpButtonB();
    abstract protected void OnButtonX();
    abstract protected void UpButtonX();
    abstract protected void OnButtonY();
    abstract protected void UpButtonY();
    abstract protected void MoveUpdata(Vector2 vec);
    abstract protected void LookUpdata(Vector2 vec);

    // 入力イベント

    // 左スティックの入力時関数
    protected virtual void OnLeftStic(InputAction.CallbackContext context) {

        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        if (playerData.JudgeInputControl(device) == false)
        {
            return;
        }

        Vector2 vec = context.ReadValue<Vector2>();

        MoveUpdata(vec);
    }

    // 右スティックの入力時関数
    protected virtual void OnRightStic(InputAction.CallbackContext context)
    {

        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        if (playerData.JudgeInputControl(device) == false)
        {
            return;
        }

        Vector2 vec = context.ReadValue<Vector2>();

        LookUpdata(vec);
    }

    // Aボタンが押されたときに呼び出される関数
    protected virtual void EnterButtonA(InputAction.CallbackContext context) {

        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        if (playerData.JudgeInputControl(device) == false) {

            return;
        }

        OnButtonA();
    }

    // Aボタンが離れたときに呼び出される関数
    protected virtual void ExitButtonA(InputAction.CallbackContext context) {

        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        if (playerData.JudgeInputControl(device) == false)
        {

            return;
        }

        UpButtonA();
    }

    // Bボタンが押されたときに呼び出される関数
    protected virtual void EnterButtonB(InputAction.CallbackContext context)
    {

        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        if (playerData.JudgeInputControl(device) == false)
        {
            return;
        }

        OnButtonB();
    }

    // Bボタンが離れたときに呼び出される関数
    protected virtual void ExitButtonB(InputAction.CallbackContext context)
    {
        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        if (playerData.JudgeInputControl(device) == false)
        {

            return;
        }

        UpButtonB();
    }

    // Xボタンが押されたときに呼び出される関数
    protected virtual void EnterButtonX(InputAction.CallbackContext context)
    {
        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        if (playerData.JudgeInputControl(device) == false)
        {
            return;
        }

        OnButtonX();
    }

    // Xボタンが離れたときに呼び出される関数
    protected virtual void ExitButtonX(InputAction.CallbackContext context)
    {
        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        if (playerData.JudgeInputControl(device) == false)
        {

            return;
        }

        UpButtonX();
    }

    // Yボタンが押されたときに呼び出される関数
    protected virtual void EnterButtonY(InputAction.CallbackContext context)
    {
        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        if (playerData.JudgeInputControl(device) == false)
        {
            return;
        }

        OnButtonY();
    }

    // Yボタンが押されたときに呼び出される関数
    protected virtual void ExitButtonY(InputAction.CallbackContext context)
    {
        // CallbackContextからControlを取得
        var control = context.control;

        // Controlからデバイスを取得
        var device = control.device;

        if (playerData.JudgeInputControl(device) == false)
        {

            return;
        }

        UpButtonY();
    }

    // 参照可能メソッド

    public void SetPlayerData(PlayerData p_)
    {
        playerData = p_;
    }
    public static PlayerParent CreatePlayer(GameObject prefab,PlayerData pd,Type type,Vector3 position,Quaternion roatation) {

        GameObject pp = Instantiate(prefab, position, roatation);
        pp.AddComponent(type);
        PlayerParent p = pp.GetComponent<PlayerParent>();
        p.playerData = pd;
        return p;
    }
}
