using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMotor player;
    private PlayerUltimateMotor ultimate;
    private PlayerLook look;
    private PlayerInteractionSystem interactionSystem;
    [SerializeField]
    private QuickItemAccessSystem QIAsystem;
    [SerializeField]
    private InventorySystem inventorySystem;
    private bool inventoryEnabled = true;
    private PerkHoldingSystem perkHoldingSystem;
    private MeleeSystem meleeSystem;
    private UltimateSystem ultimateSystem;
    private UltimateMelee ultimateMelee;
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
        ultimate = GetComponent<PlayerUltimateMotor>();
        look = GetComponent<PlayerLook>();
        perkHoldingSystem = GameObject.FindGameObjectWithTag("PerkHolder").GetComponent<PerkHoldingSystem>();
        interactionSystem = GetComponent<PlayerInteractionSystem>();
        meleeSystem = GetComponent<MeleeSystem>();
        ultimateSystem = GetComponent<UltimateSystem>();
        ultimateMelee = GameObject.FindGameObjectWithTag("UltimateMelee").GetComponent<UltimateMelee>();
        inputs = new PlayerInputs();

        // Movement controls
        inputs.OnFoot.Jump.performed += ctx => player.Jump();
        inputs.OnFoot.KnockDown.performed += ctx => player.KnockDown();
        inputs.OnFoot.CrouchHold.started += ctx => player.Slide();
        inputs.OnFoot.CrouchHold.canceled += ctx => player.Slide();
        inputs.OnFoot.Slide.performed += ctx => player.Slide();

        // Interaction system
        inputs.OnFoot.Interact.performed += ctx => interactionSystem.Interact();

        // Inventory controls
        inputs.ItemManipulation.Use_InInventory.performed += ctx => inventorySystem.UseItem();
        inputs.ItemManipulation.Remove_InInventory.performed += ctx => inventorySystem.RemoveItem();
        inputs.ItemManipulation.Swap_InInventory.started += ctx => inventorySystem.SwapSequence();
        inputs.ItemManipulation.Swap_InInventory.canceled += ctx => inventorySystem.SwapSequence();
        inputs.ItemManipulation.Inventory.performed += ctx => EnableInventoryMode();

        // Perk holding system
        inputs.OnFoot.PerkScreen.started += ctx => perkHoldingSystem.EnablePerkScreen();
        inputs.OnFoot.PerkScreen.canceled += ctx => perkHoldingSystem.DisablePerkScreen();

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

        // Melee
        inputs.OnFoot.Melee.performed += ctx => meleeSystem.useEmbededMelee();

        // Ultimate
        inputs.OnFoot.ActivateUltimate.performed += ctx => ChangeUltimateModeStatus();
        inputs.UltimateMode.DectivateUltimate.performed += ctx => ChangeUltimateModeStatus();
        inputs.UltimateMode.Jump.performed += ctx => ultimate.Jump();
        inputs.UltimateMode.KnockDown.performed += ctx => ultimate.KnockDown();
        inputs.UltimateMode.CrouchHold.started += ctx => ultimate.Slide();
        inputs.UltimateMode.CrouchHold.canceled += ctx => ultimate.Slide();
        inputs.UltimateMode.Slide.performed += ctx => ultimate.Slide();
        inputs.UltimateMode.HorizontalHit.performed += ctx => ultimateMelee.PerformHorizontalHit();
        inputs.UltimateMode.VerticalHit.performed += ctx => ultimateMelee.PerformVerticalHit();
        inputs.UltimateMode.ShieldUp.started += ctx => StartCoroutine(ultimateMelee.ToggleDefensiveState());
        inputs.UltimateMode.ShieldUp.canceled += ctx => StartCoroutine(ultimateMelee.ToggleDefensiveState());

        SwapQuickAccessToAsignment(false);
    }
    private void Start()
    {
        EnableInventoryMode();
        EnableInventoryMode();
        ChangeUltimateModeStatus();
    }
    public void EnableInventoryMode()
    {
        inventoryEnabled = !inventoryEnabled;
        if (inventoryEnabled)
        {
            SwapQuickAccessToAsignment(true);
            inventorySystem.transform.gameObject.SetActive(true);

            inputs.OnFoot.Disable();
            inputs.UltimateMode.Disable();
            player.enabled = true;
            ultimate.enabled = false;
            ultimateSystem.UltimateModeEnabled = false;
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
            inputs.UltimateMode.Disable();

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

    public void ChangeUltimateModeStatus()
    {
        if (ultimateSystem.Energy >= ultimateSystem.MaxEnergy && inputs.OnFoot.enabled) // Enable Ult mode
        {
            inputs.OnFoot.Disable();
            inputs.UltimateMode.Enable();

            player.enabled = false;
            ultimate.enabled = true;
            ultimateSystem.UltimateModeEnabled = true;

            meleeSystem.EmbededMelee.GetComponent<EmbededMelee>().enabled = false;

            ultimateMelee.enabled = true;

            GameObject.FindGameObjectWithTag("Hands").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Hands").transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            inputs.OnFoot.Enable();
            inputs.UltimateMode.Disable();

            player.enabled = true;
            ultimate.enabled = false;
            ultimateSystem.UltimateModeEnabled = false;

            meleeSystem.EmbededMelee.GetComponent<EmbededMelee>().enabled = true;

            ultimateMelee.enabled = false;

            GameObject.FindGameObjectWithTag("Hands").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Hands").transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (inputs.OnFoot.enabled)
            player.ProcessMove(inputs.OnFoot.Movement.ReadValue<Vector2>());
        else
            ultimate.ProcessMove(inputs.UltimateMode.Movement.ReadValue<Vector2>());

        if (ultimateSystem.UltimateModeEnabled && ultimateSystem.Energy <= 0) 
            ChangeUltimateModeStatus();
    }
    private void LateUpdate()
    {
        if (!inventoryEnabled)
        {
            if (inputs.OnFoot.enabled)
                look.processLook(inputs.OnFoot.Look.ReadValue<Vector2>());
            else
                look.processLook(inputs.UltimateMode.Look.ReadValue<Vector2>());
        }
        else
            inventorySystem.SelectedItemTracking();
    }
}
