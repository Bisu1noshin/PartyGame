using UnityEngine;
using UnityEngine.InputSystem;
using System;

public abstract class PlayerParent2 : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction _LeftStick;
    private InputAction _RightStick;
    private InputAction _ButtonA;
    private InputAction _ButtonB;
    private InputAction _ButtonX;
    private InputAction _ButtonY;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        // アクションを取得
        {
            _LeftStick = playerInput.actions["LeftStick"];
            _RightStick = playerInput.actions["RightStick"];
            _ButtonA = playerInput.actions["ButtonA"];
            _ButtonB = playerInput.actions["ButtonB"];
            _ButtonX = playerInput.actions["ButtonX"];
            _ButtonY = playerInput.actions["ButtonY"];
        }
    }

    private void OnEnable()
    {
        if (playerInput == null) { return; }

        // デリゲート登録
        _LeftStick.started    += OnLeftStick;
        _LeftStick.performed  += OnLeftStick;
        _LeftStick.canceled   += OnLeftStick;
        _RightStick.started   += OnRightStick;
        _RightStick.performed += OnRightStick;
        _RightStick.canceled  += OnRightStick;
        _ButtonA.started      += EnterButtonA;
        _ButtonA.canceled     += ExitButtonA;
        _ButtonB.started      += EnterButtonB;
        _ButtonB.canceled     += ExitButtonB;
        _ButtonX.started      += EnterButtonX;
        _ButtonX.canceled     += ExitButtonX;
        _ButtonY.started      += EnterButtonY;
        _ButtonY.canceled     += ExitButtonY;
    }

    private void OnDisable()
    {
        if (playerInput == null) return;

        // デリゲート登録解除
        _LeftStick.started    -= OnLeftStick;
        _LeftStick.performed  -= OnLeftStick;
        _LeftStick.canceled   -= OnLeftStick;
        _RightStick.started   -= OnRightStick;
        _RightStick.performed -= OnRightStick;
        _RightStick.canceled  -= OnRightStick;
        _ButtonA.started      -= EnterButtonA;
        _ButtonA.canceled     -= ExitButtonA;
        _ButtonB.started      -= EnterButtonB;
        _ButtonB.canceled     -= ExitButtonB;
        _ButtonX.started      -= EnterButtonX;
        _ButtonX.canceled     -= ExitButtonX;
        _ButtonY.started      -= EnterButtonY;
        _ButtonY.canceled     -= ExitButtonY;
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
    abstract protected void MoveUpdate(Vector2 vec);
    abstract protected void LookUpdate(Vector2 vec);

    // Actionのメソッド

    private void OnLeftStick(InputAction.CallbackContext context) {

        Vector2 vec = context.ReadValue<Vector2>();

        MoveUpdate(vec);
    }

    private void OnRightStick(InputAction.CallbackContext context)
    {
        Vector2 vec = context.ReadValue<Vector2>();

        LookUpdate(vec);
    }

    private void EnterButtonA(InputAction.CallbackContext context)
    {
        OnButtonA();
    }

    private void ExitButtonA(InputAction.CallbackContext context) {

        UpButtonA();
    }

    private void EnterButtonB(InputAction.CallbackContext context)
    {
        OnButtonB();
    }

    private void ExitButtonB(InputAction.CallbackContext context)
    {
        UpButtonB();
    }

    private void EnterButtonY(InputAction.CallbackContext context)
    {
        OnButtonY();
    }

    private void ExitButtonY(InputAction.CallbackContext context)
    {
        UpButtonY();
    }

    private void EnterButtonX(InputAction.CallbackContext context)
    {
        OnButtonX();
    }

    private void ExitButtonX(InputAction.CallbackContext context)
    {

        UpButtonX();
    }

    public static PlayerParent2 CreatePlayer(
        GameObject prefab,
        Type type,
        InputDevice device,
        int playerIndex,
        Vector3 position,
        Quaternion roatation
        )
    {
        if (!prefab.GetComponent<PlayerInput>()) {

            Debug.Log("PlayerInputコンポーネントがアタッチされていません");
            return null;    
        }

        PlayerInput pi = PlayerInput.Instantiate(
            prefab: prefab,
            playerIndex: playerIndex,
            pairWithDevice: device
            );

        pi.gameObject.transform.localPosition = position;
        pi.gameObject.transform.rotation = roatation;

        pi.gameObject.AddComponent(type);
        PlayerParent2 p = pi.gameObject.GetComponent<PlayerParent2>();
        return p;
    }
}
