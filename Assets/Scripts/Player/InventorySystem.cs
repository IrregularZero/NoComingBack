using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    private QuickItemAccessSystem quickItemAccess;

    private List<Transform> slots;
    private Dictionary<int, GameObject> items;
    private int maxItems = 6;

    private int selectedSlot = 0;
    private int swapingItemSlot = -1;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        items = new Dictionary<int, GameObject>();

        slots = new List<Transform>();
        for (int i = 0; i < 6; i++)
        {
            slots.Add(transform.GetChild(3).GetChild(i));
        }
    }

    #region Item Manipulation
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
    #endregion

    private void SelectSlotAndDeselectPrevious(int slot)
    {
        slots[selectedSlot].GetChild(0).gameObject.SetActive(false);
        selectedSlot = slot;
        slots[selectedSlot].GetChild(0).gameObject.SetActive(true);
    }
    public void SelectedItemTracking()
    {
        #region Getting mouse angle from center of the screen
        Vector2 mouseInput = Vector2.zero;
        mouseInput.x = Input.mousePosition.x - (Screen.width * 0.5f);
        mouseInput.y = Input.mousePosition.y - (Screen.height * 0.5f);
        mouseInput.Normalize();

        float angle = 0;
        if (mouseInput != Vector2.zero)
        {
            angle = Mathf.Atan2(mouseInput.y, -mouseInput.x) * Mathf.Rad2Deg;
            if (angle <= 0) angle += 360;
        }
        #endregion

        if (angle >= 60 && angle < 120)
        {
            SelectSlotAndDeselectPrevious(0);
        }
        else if (angle >= 120 && angle < 180)
        {
            SelectSlotAndDeselectPrevious(1);
        }
        else if (angle >= 180 && angle < 240)
        {
            SelectSlotAndDeselectPrevious(2);
        }
        else if (angle >= 240 && angle < 300)
        {
            SelectSlotAndDeselectPrevious(3);
        }
        else if (angle >= 300 && angle <= 360)
        {
            SelectSlotAndDeselectPrevious(4);
        }
        else if (angle >= 0 && angle < 60)
        {
            SelectSlotAndDeselectPrevious(5);
        }
    }
}
