using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuickItemAccessSystem : MonoBehaviour
{
    [SerializeField]
    private Dictionary<int, Item> items;
    [SerializeField]
    private int maxSize = 4;

    #region Properties
    public Dictionary<int, Item> Items
    {
        set
        {
            foreach (int key in items.Keys)
            {
                if (key >= maxSize) // keys can be from 0 to 3
                {
                    return;
                }
            }

            if (value.Count <= maxSize) // size can't be bigger or equals to 4
            {
                items = value;
            }
        }
    }
    #endregion

    private void Start()
    {
        items = new Dictionary<int, Item>();
    }

    public void AsignItemToSlot(int slot, Item asigningItem)
    {
        if (slot >= maxSize)
            return;

        // If slot is taken, the asigning item will replace previous one, or create slot for itself
        if (items.ContainsKey(slot))
            items[slot] = asigningItem;
        else
            items.Add(slot, asigningItem);
    }
    public void UseAsignedItem(int slot)
    {
        if (slot >= maxSize)
            return;

        items[slot].Use();
    }
}
