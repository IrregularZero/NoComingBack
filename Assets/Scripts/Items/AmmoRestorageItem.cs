using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class AmmoRestorageItem : Item
{
    [SerializeField]
    private int ammoAmount;

    #region Properties
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
        backgroundColor = new Color(0.854f, 0.8f, 0.074f, 1f);
        type = "Ammo Restorage Consumable";
    }
    public override void Use()
    {
        GameObject gun;
        HandsObjectsTrackingSystem hands = GameObject.FindGameObjectWithTag("Hands").GetComponent<HandsObjectsTrackingSystem>();
        if (hands.RightHand.childCount > 0 && hands.RightHand.GetChild(0).GetComponent<GunSystem>() != null)
        {
            gun = hands.RightHand.GetChild(0).gameObject;
        }
        else if (hands.LeftHand.childCount > 0 && hands.LeftHand.GetChild(0).GetComponent<GunSystem>() != null)
        {
            gun = hands.LeftHand.GetChild(0).gameObject;
        }
        else
        {
            Debug.Log($"Gun was not found to use {name}");
            return;
        }

        if (ammoAmount > 0)
        {
            int difference = gun.GetComponent<GunSystem>().MaxAmmoInStorage - gun.GetComponent<GunSystem>().AmmoInStorage;
            gun.GetComponent<GunSystem>().AmmoInStorage = Mathf.Clamp(ammoAmount + gun.GetComponent<GunSystem>().AmmoInStorage, 0, gun.GetComponent<GunSystem>().MaxAmmoInStorage);
            ammoAmount = Mathf.Clamp(ammoAmount - difference, 0, ammoAmount);
        }
    }
}
