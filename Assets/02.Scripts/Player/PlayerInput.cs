using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }	// 우리가 만든 Actions

    private void Awake()
    {
        InputActions = new PlayerInputActions();

        PlayerActions = InputActions.Player;
    }

    // Input 기능을 켬
    private void OnEnable()
    {
        InputActions.Enable();
    }

    // Input 기능을 끔
    private void OnDisable()
    {
        InputActions.Disable();
    }

}
