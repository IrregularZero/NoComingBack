using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMotor player;
    private PlayerLook look;
    private PlayerInteractionSystem interactionSystem;
    [SerializeField]
    private QuickItemAccessSystem QIAsystem;
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
        interactionSystem = GetComponent<PlayerInteractionSystem>();
        inputs = new PlayerInputs();

        inputs.OnFoot.Jump.performed += ctx => player.Jump();
        inputs.OnFoot.KnockDown.performed += ctx => player.KnockDown();
        inputs.OnFoot.CrouchHold.started += ctx => player.Crouch();
        inputs.OnFoot.CrouchHold.canceled += ctx => player.Crouch();
        inputs.OnFoot.Slide.performed += ctx => player.Slide();
        inputs.OnFoot.Interact.performed += ctx => interactionSystem.Interact();

        SwapQuickAccessToAsignment(false);
    }

    public void SwapQuickAccessToAsignment(bool swap)
    {
        if (swap)
        {
            // Should change controls to asignment through Inventory
        }
        else
        {
            inputs.ItemManipulation.NorthSlot.performed += ctx => QIAsystem.UseAsignedItem(0);
            inputs.ItemManipulation.EastSlot.performed += ctx => QIAsystem.UseAsignedItem(1);
            inputs.ItemManipulation.WestSlot.performed += ctx => QIAsystem.UseAsignedItem(2);
            inputs.ItemManipulation.SouthSlot.performed += ctx => QIAsystem.UseAsignedItem(3);
        }
        
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
