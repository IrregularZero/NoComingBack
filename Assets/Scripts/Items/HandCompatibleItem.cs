using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class HandCompatibleItem : Item
{
    [SerializeField]
    private bool isEquipped;
    [SerializeField]
    private GameObject handCompatibleItemPrefab;
    private GameObject activeHandCompatibleItem;
    [SerializeField]
    private Transform handCompatibleItemPlacement;
    [SerializeField]
    private Transform hands; // Should be set as Parent

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
    public Transform Hands 
    {
        get
        {
            return hands;
        }
    }
    #endregion

    // Use() make weapon toggle it's state from equipped to deequipped
    public override void Use()
    {
        if (!isEquipped)
        {
            activeHandCompatibleItem = Instantiate(handCompatibleItemPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            activeHandCompatibleItem.transform.parent = hands.transform;
            activeHandCompatibleItem.transform.position = handCompatibleItemPlacement.position;
            activeHandCompatibleItem.transform.rotation = handCompatibleItemPlacement.rotation;

            isEquipped = true;
        }
        else
        {
            Deequip();
        }
    }
    public void Deequip()
    {
        Destroy(activeHandCompatibleItem);

        isEquipped = false;
    }
}
