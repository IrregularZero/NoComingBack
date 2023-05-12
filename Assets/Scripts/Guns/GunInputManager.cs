using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInputManager : MonoBehaviour
{
    private GunSystem gunSystem;
    private PlayerInputs gunActions;
    private InputManager playerInputs;

    [SerializeField]
    private bool holdToFireOrCharge = false;
    [SerializeField]
    private bool holdToSpecialFireOrCharge = false;

    private void OnEnable()
    {
        gunActions.Enable();
    }
    private void OnDisable()
    {
        gunActions.Disable();
    }

    private void Awake()
    {
        gunActions = new PlayerInputs();
        gunSystem = GetComponent<GunSystem>();
        playerInputs = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();

        if (!holdToFireOrCharge)
            gunActions.Gun.Fire.performed += ctx => gunSystem.Fire();
        else
        {
            gunActions.Gun.Fire.started += ctx => gunSystem.Fire();
            gunActions.Gun.Fire.canceled += ctx => gunSystem.Fire();
        }
        if (!holdToSpecialFireOrCharge)
            gunActions.Gun.SpecialFire.performed += ctx => gunSystem.SpecialFire();
        else
        {
            gunActions.Gun.SpecialFire.started += ctx => gunSystem.SpecialFire();
            gunActions.Gun.SpecialFire.canceled += ctx => gunSystem.SpecialFire();
        }
        gunActions.Gun.Reload.performed += ctx => StartCoroutine(gunSystem.Reload());
        gunActions.Gun.Overview.performed += ctx => StartCoroutine(gunSystem.Overview());
    }
    private void Update()
    {
        if (playerInputs.InventoryEnabled)
        {
            gunActions.Gun.Disable();
        }
        else
        {
            gunActions.Gun.Enable();
        }
    }
}
