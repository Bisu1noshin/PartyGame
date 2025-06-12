using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    private PartyGame inputAction;
    private int userNumber;

    private void Awake()
    {
        inputAction = new PartyGame();

        inputAction.Test.Test.started += OnTestInput;

        inputAction.Enable();
    }

    // “ü—ÍƒCƒxƒ“ƒg

    public void OnTestInput(InputAction.CallbackContext context)
    {

        Debug.Log("userNumber:" + userNumber);
    }

    public void SetUserNumber(int num) {

        userNumber = num + 1;
    }
}
