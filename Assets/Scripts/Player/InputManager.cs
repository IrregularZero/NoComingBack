using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMotor player;
    private PlayerLook look;
    private PlayerInputs inputs;

    private void OnEnable()
    {
        inputs.Enable();
    }
    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Awake()
    {
        player = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        inputs = new PlayerInputs();

        inputs.OnFoot.Jump.performed += ctx => player.Jump();
    }

    private void Update()
    {
        player.ProcessMove(inputs.OnFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.processLook(inputs.OnFoot.Look.ReadValue<Vector2>());
    }
}
