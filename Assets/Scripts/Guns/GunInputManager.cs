using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInputManager : MonoBehaviour
{
    [SerializeField]
    private GunSystem gunSystem;
    private PlayerInputs gunActions;
    private InputManager playerInputs;

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
        playerInputs = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        
        gunActions.Gun.Fire.performed += ctx => gunSystem.Fire();
        gunActions.Gun.SpecialFire.performed += ctx => gunSystem.SpecialFire();
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
