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
    [SerializeField]
    private InventorySystem inventorySystem;
    private bool inventoryEnabled = true;
    private PlayerInputs inputs;

    #region Properties
    public InventorySystem Inventory 
    {
        get
        {
            return inventorySystem;
        } 
    }
    public bool InventoryEnabled 
    {
        get
        {
            return inventoryEnabled;
        } 
    }
    #endregion

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

        // Movement controls
        inputs.OnFoot.Jump.performed += ctx => player.Jump();
        inputs.OnFoot.KnockDown.performed += ctx => player.KnockDown();
        inputs.OnFoot.CrouchHold.started += ctx => player.Crouch();
        inputs.OnFoot.CrouchHold.canceled += ctx => player.Crouch();
        inputs.OnFoot.Slide.performed += ctx => player.Slide();

        // Interaction system
        inputs.OnFoot.Interact.performed += ctx => interactionSystem.Interact();

        // Inventory controls
        inputs.ItemManipulation.Use_InInventory.performed += ctx => inventorySystem.UseItem();
        inputs.ItemManipulation.Remove_InInventory.performed += ctx => inventorySystem.RemoveItem();
        inputs.ItemManipulation.Swap_InInventory.started += ctx => inventorySystem.SwapSequence();
        inputs.ItemManipulation.Swap_InInventory.canceled += ctx => inventorySystem.SwapSequence();
        inputs.ItemManipulation.Inventory.performed += ctx => EnableInventoryMode();

        // Asignment
        inputs.ItemManipulation.NorthSlotAsignment.performed += ctx => inventorySystem.AsignOrDeasignItemToQuickItemAccessSystem(0);
        inputs.ItemManipulation.EastSlotAsignment.performed += ctx => inventorySystem.AsignOrDeasignItemToQuickItemAccessSystem(1);
        inputs.ItemManipulation.WestSlotAsignment.performed += ctx => inventorySystem.AsignOrDeasignItemToQuickItemAccessSystem(2);
        inputs.ItemManipulation.SouthSlotAsignment.performed += ctx => inventorySystem.AsignOrDeasignItemToQuickItemAccessSystem(3);

        // hotkeys
        inputs.ItemManipulation.NorthSlot.performed += ctx => QIAsystem.UseAsignedItem(0);
        inputs.ItemManipulation.EastSlot.performed += ctx => QIAsystem.UseAsignedItem(1);
        inputs.ItemManipulation.WestSlot.performed += ctx => QIAsystem.UseAsignedItem(2);
        inputs.ItemManipulation.SouthSlot.performed += ctx => QIAsystem.UseAsignedItem(3);

        SwapQuickAccessToAsignment(false);
    }
    private void Start()
    {
        EnableInventoryMode();
        EnableInventoryMode();
    }
    public void EnableInventoryMode()
    {
        inventoryEnabled = !inventoryEnabled;
        if (inventoryEnabled)
        {
            SwapQuickAccessToAsignment(true);
            inventorySystem.transform.gameObject.SetActive(true);

            inputs.OnFoot.Disable();
            inputs.OnFoot.Movement.Enable();
            inputs.OnFoot.Look.Enable();

            inputs.ItemManipulation.Use_InInventory.Enable();
            inputs.ItemManipulation.Remove_InInventory.Enable();
            inputs.ItemManipulation.Swap_InInventory.Enable();
        }
        else
        {
            SwapQuickAccessToAsignment(false);
            inventorySystem.transform.gameObject.SetActive(false);

            inputs.OnFoot.Enable();

            inputs.ItemManipulation.Use_InInventory.Disable();
            inputs.ItemManipulation.Remove_InInventory.Disable();
            inputs.ItemManipulation.Swap_InInventory.Disable();
        }
    }

    public void SwapQuickAccessToAsignment(bool swap)
    {
        if (swap)
        {
            inputs.ItemManipulation.NorthSlot.Disable();
            inputs.ItemManipulation.EastSlot.Disable();
            inputs.ItemManipulation.WestSlot.Disable();
            inputs.ItemManipulation.SouthSlot.Disable();

            inputs.ItemManipulation.NorthSlotAsignment.Enable();
            inputs.ItemManipulation.EastSlotAsignment.Enable();
            inputs.ItemManipulation.WestSlotAsignment.Enable();
            inputs.ItemManipulation.SouthSlotAsignment.Enable();
        }
        else
        {
            inputs.ItemManipulation.NorthSlotAsignment.Disable();
            inputs.ItemManipulation.EastSlotAsignment.Disable();
            inputs.ItemManipulation.WestSlotAsignment.Disable();
            inputs.ItemManipulation.SouthSlotAsignment.Disable();

            inputs.ItemManipulation.NorthSlot.Enable();
            inputs.ItemManipulation.EastSlot.Enable();
            inputs.ItemManipulation.WestSlot.Enable();
            inputs.ItemManipulation.SouthSlot.Enable();
        }
        
    }

    private void Update()
    {
        player.ProcessMove(inputs.OnFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        if (!inventoryEnabled)
            look.processLook(inputs.OnFoot.Look.ReadValue<Vector2>());
        else
            inventorySystem.SelectedItemTracking();
    }
}
