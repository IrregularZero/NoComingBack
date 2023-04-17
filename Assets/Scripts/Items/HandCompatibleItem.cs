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
    #endregion

    private void Start()
    {
        backgroundColor = new Color(0.596f, 0f, 0.662f, 1f);
        type = "Weapon";
    }
    // Use() make weapon toggle it's state from equipped to deequipped
    public override void Use()
    {
        if (!isEquipped)
        {
            GameObject hands = GameObject.FindGameObjectWithTag("Hands");
            if (hands.GetComponent<HandsObjectsTrackingSystem>().HasFreeHand)
                handCompatibleItemPlacement = hands.GetComponent<HandsObjectsTrackingSystem>().FreeHand;
            else
                return;

            activeHandCompatibleItem = Instantiate(handCompatibleItemPrefab, handCompatibleItemPlacement.transform.position, Quaternion.identity, handCompatibleItemPlacement) as GameObject;

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
