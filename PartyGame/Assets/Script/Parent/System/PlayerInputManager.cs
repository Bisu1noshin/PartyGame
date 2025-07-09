using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerInputManager : MonoBehaviour
{
    private PartyGame inputAction;

    private void Awake()
    {
        inputAction = new PartyGame();

        
        //InputUser.PerformPairingWithDevice()
    }
}
