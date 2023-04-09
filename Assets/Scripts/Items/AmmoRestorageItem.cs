using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class AmmoRestorageItem : Item
{
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private int ammoAmount;

    #region Properties
    public GameObject Gun
    {
        get
        {
            return gun;
        }
        set
        {
            gun = value;
        }
    }
    public int AmmoAmount
    {
        get
        {
            return ammoAmount;
        }
        set
        {
            if (value > 0)
            {
                ammoAmount = value;
            }
        }
    }
    #endregion

    private void Start()
    {
        backgroundColor = new Color(218, 204, 19, 1);
        type = "Ammo Restorage Consumable";
    }
    public override void Use()
    {
        if (ammoAmount > 0)
        {
            int difference = gun.GetComponent<GunSystem>().MaxAmmoInStorage - gun.GetComponent<GunSystem>().AmmoInStorage;
            gun.GetComponent<GunSystem>().AmmoInStorage = Mathf.Clamp(ammoAmount + gun.GetComponent<GunSystem>().AmmoInStorage, 0, gun.GetComponent<GunSystem>().MaxAmmoInStorage);
            ammoAmount = Mathf.Clamp(ammoAmount - difference, 0, ammoAmount);
        }
    }
}
