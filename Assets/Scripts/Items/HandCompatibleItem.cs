using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class HandCompatibleItem : Item
{
    [SerializeField]
    protected bool isEquipped;
    [SerializeField]
    protected GameObject handCompatibleItemPrefab;
    protected GameObject activeHandCompatibleItem;
    protected Transform handCompatibleItemPlacement; // Should be set as Parent
    protected InventorySystem invSys;

    [SerializeField]
    private int standardAmmoInStorage;

    #region Properties
    public bool IsEquipped 
    {
        get
        {
            return isEquipped;
        } 
    }
    public GameObject HandCompatibleItemPrefab
    {
        get
        {
            return handCompatibleItemPrefab;
        }
        set
        {
            handCompatibleItemPrefab = value;
        }
    }
    public GameObject ActiveHandCompatibleItem
    {
        get
        {
            return activeHandCompatibleItem;
        } 
    }
    public Transform HandCompatibleItemPlacement
    {
        get
        {
            return handCompatibleItemPlacement;
        }
        set
        {
            handCompatibleItemPlacement = value;
        }
    }
    public int StandardAmmoInStorage 
    {
        get
        {
            return standardAmmoInStorage;
        } 
        set
        {
            if (value >= 0)
                standardAmmoInStorage = value;
        }
    }
    #endregion

    private void Start()
    {
        backgroundColor = new Color(0.596f, 0f, 0.662f, 1f);
        type = "Weapon";
    }
    private void Update()
    {
        if (invSys == null)
        {
            invSys = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventorySystem>();
        }
    }
    // Use() make weapon toggle it's state from equipped to deequipped
    public override void Use()
    {
        if (!isEquipped)
        {
            HandsObjectsTrackingSystem hands = GameObject.FindGameObjectWithTag("Hands").GetComponent<HandsObjectsTrackingSystem>();

            if (type == "Weapon")
            {
                if (hands.RightHand.childCount > 0)
                {
                    hands.RightHand.GetChild(0).gameObject.GetComponent<GunSystem>().ItemReference.Use();
                }
                handCompatibleItemPlacement = hands.RightHand;
            }
            else if (type == "Event Item")
            {
                if (hands.LeftHand.childCount <= 0)
                    handCompatibleItemPlacement = hands.LeftHand;
                else
                    return;
            }

            activeHandCompatibleItem = Instantiate(handCompatibleItemPrefab, handCompatibleItemPlacement.transform.position, Quaternion.identity, handCompatibleItemPlacement) as GameObject;

            if (type == "Weapon")
            {
                activeHandCompatibleItem.name = name;
                activeHandCompatibleItem.GetComponent<GunSystem>().AmmoInStorage = invSys.AmmoInStorageOfGuns[activeHandCompatibleItem.name];
                activeHandCompatibleItem.GetComponent<GunSystem>().ItemReference = this;
            }

            isEquipped = true;
        }
        else
        {
            Deequip();
        }
    }
    public void Deequip()
    {
        if (type == "Weapon")
        {
            invSys.AmmoInStorageOfGuns[activeHandCompatibleItem.name] = activeHandCompatibleItem.GetComponent<GunSystem>().AmmoInStorage;
        }

        Destroy(activeHandCompatibleItem);

        isEquipped = false;
    }
}
