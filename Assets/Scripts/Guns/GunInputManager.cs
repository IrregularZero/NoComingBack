using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInputManager : MonoBehaviour
{
    [SerializeField]
    private GunSystem gunSystem;
    private PlayerInputs.GunActions gunActions;

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
        gunActions = new PlayerInputs.GunActions();
        
        gunActions.Fire.performed += ctx => gunSystem.Fire();
        gunActions.SpecialFire.performed += ctx => gunSystem.SpecialFire();
        gunActions.Reload.performed += ctx => StartCoroutine(gunSystem.Reload());
        gunActions.Overview.performed += ctx => gunSystem.Overview();
    }
}
