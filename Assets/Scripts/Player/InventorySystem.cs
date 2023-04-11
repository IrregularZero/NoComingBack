using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    private QuickItemAccessSystem quickItemAccess;

    private Dictionary<int, GameObject> items;
    private int maxItems = 10;

    private int selectedSlot = 0;
    private int swapingItemSlot = -1;

    private void Start()
    {
        items = new Dictionary<int, GameObject>();
    }

    public void AddItem(GameObject item)
    {
        int slotForNewItem = -1;

        // Case in which player had used every slot, so there are maxItems amount of items were initialised
        if (items.Count == maxItems)
        {
            bool hasNoSpareSlot = true;
            for (int i = 0; i < maxItems && hasNoSpareSlot; i++)
            {
                if (items[i] == null)
                {
                    slotForNewItem = i;
                    hasNoSpareSlot = true;
                }
            }

            if (hasNoSpareSlot)
            {
                Debug.Log($"Player has no spare slot for {item.name}");
                return;
            }
        }
        else
            slotForNewItem = items.Count;

        if (items.ContainsKey(slotForNewItem))
            items[slotForNewItem] = item;
        else
            items.Add(slotForNewItem, item);
    }

    public void RemoveItem() 
    {
        if (!items.ContainsKey(selectedSlot))
            return;

        items[selectedSlot] = null;
    }

    public void SwapItems()
    {
        if (swapingItemSlot < 0)
            return;

        GameObject tmp = items[selectedSlot];
        items[selectedSlot] = items[swapingItemSlot];
        items[swapingItemSlot] = tmp;
    }

    public void AsignOrDeasignItemToQuickItemAccessSystem(int QIASslotIndex)
    {
        quickItemAccess.AsignItemToSlot(QIASslotIndex, items[selectedSlot]);
    }
}
