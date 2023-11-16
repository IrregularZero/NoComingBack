using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    private QuickItemAccessSystem quickItemAccess;
    [SerializeField]
    private GameObject infoPanel;

    private List<Transform> slots;
    private Dictionary<int, GameObject> items;
    private Dictionary<string, int> ammoInStorageOfGuns;
    private int maxItems = 6;

    private int selectedSlot = 0;
    private int swapingItemSlot = -1;

    private bool swapSequenceInitiated = false;

    private Color defaultSelectionColor = new Color(1f, 1f, 1f, 0.3137f);

    private float maxAnimationTimer = 0.25f;
    private List<float> animationTimers;

    #region Properties
    public Dictionary<string, int> AmmoInStorageOfGuns 
    {
        get
        {
            return ammoInStorageOfGuns;
        }
    }
    public Dictionary<int, GameObject> Items 
    {
        get
        {
            return items;
        } 
    }
    public int MaxItems 
    {
        get
        {
            return maxItems;
        }
    }
    public int SelectedSlot 
    {
        get
        {
            return selectedSlot;
        }
        set
        {
            if (value >= 0 && value < maxItems)
                selectedSlot = value;
        }
    }
    #endregion

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
        ammoInStorageOfGuns = new Dictionary<string, int>();

        slots = new List<Transform>();
        animationTimers = new List<float>();
        for (int i = 0; i < 6; i++)
        {
            slots.Add(transform.GetChild(3).GetChild(i));
            animationTimers.Add(0f);
        }
    }
    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (animationTimers[i] > 0)
                animationTimers[i] -= Time.deltaTime;
            else
                slots[i].GetComponent<Animator>().SetBool("Interacted", false);
        }
    }

    #region Item Manipulation
    public void AddItem(GameObject item)
    {
        if (ammoInStorageOfGuns.ContainsKey(item.name))
            return;

        int slotForNewItem = -1;
        bool hasSpareSlot = false;

        for (int i = 0; i < items.Count && !hasSpareSlot; i++)
        {
            if (items[i] == null)
            {
                slotForNewItem = i;
                hasSpareSlot = true;
            }
        }

        if (!hasSpareSlot)
        {
            if (items.Count != maxItems)
            {
                slotForNewItem = items.Count;
            }
            else
            {
                Debug.Log($"Player has no spare slot for {item.name}");
                return;
            }
        }

        if (items.ContainsKey(slotForNewItem))
            items[slotForNewItem] = item;
        else
            items.Add(slotForNewItem, item);

        // We have to actually change item's hierarchy here in inventory
        GameObject oldItemsParent = item.transform.parent.gameObject;
        item.transform.parent = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>().Inventory.transform.GetChild(4);
        Destroy(oldItemsParent);

        if (item.GetComponent<Item>().Type == "Weapon")
            ammoInStorageOfGuns.Add(item.name, item.GetComponent<HandCompatibleItem>().StandardAmmoInStorage);

        slots[slotForNewItem].GetChild(0).gameObject.GetComponent<Image>().color = item.GetComponent<Item>().BackgroundColor;
        slots[slotForNewItem].GetChild(1).gameObject.GetComponent<Image>().sprite = item.GetComponent<Item>().ItemIcon;
        slots[slotForNewItem].GetChild(1).gameObject.SetActive(true);
    }
    public void UseItem()
    {
        slots[selectedSlot].GetComponent<Animator>().SetBool("Interacted", true);
        animationTimers[selectedSlot] = maxAnimationTimer;

        if (!items.ContainsKey(selectedSlot) || items[selectedSlot] == null)
            return;

        items[selectedSlot].GetComponent<Item>().Use();
    }
    public GameObject FindItem(string itemTitle)
    {
        foreach (int item in items.Keys)
            if (items[item].name == itemTitle)
                return items[item];

        return null;
    }
    public void RemoveItem()
    {
        slots[selectedSlot].GetComponent<Animator>().SetBool("Interacted", true);
        animationTimers[selectedSlot] = maxAnimationTimer;

        if (!items.ContainsKey(selectedSlot))
            return;

        if (items[selectedSlot].GetComponent<Item>().Type == "Weapon")
            ammoInStorageOfGuns.Remove(items[selectedSlot].name);

        if (items[selectedSlot].GetComponent<Item>().Type == "Weapon" || items[selectedSlot].GetComponent<Item>().Type == "Event Item")
        {
            if (((HandCompatibleItem)items[selectedSlot].GetComponent<Item>()).IsEquipped)
                items[selectedSlot].GetComponent<Item>().Use();
        }

        for (int i = 0; i < 4; i++)
        {
            if (quickItemAccess.Items.ContainsKey(i) && quickItemAccess.Items[i] != null)
            {
                if (quickItemAccess.Items[i].GetComponent<Item>() == items[selectedSlot].GetComponent<Item>())
                {
                    quickItemAccess.DeasignItemFromSlot(i);
                }
            }
        }

        items[selectedSlot] = null;
        slots[selectedSlot].GetChild(1).gameObject.SetActive(false);
        slots[selectedSlot].GetChild(0).GetComponent<Image>().color = defaultSelectionColor;
    }
    public void SwapItems()
    {
        if (swapingItemSlot < 0)
            return;

        slots[selectedSlot].GetComponent<Animator>().SetBool("Interacted", true);
        animationTimers[selectedSlot] = maxAnimationTimer;

        if (!items.ContainsKey(selectedSlot))
        {
            items.Add(selectedSlot, null);
        }

        GameObject tmp = items[selectedSlot];
        items[selectedSlot] = items[swapingItemSlot];
        items[swapingItemSlot] = tmp;

        slots[swapingItemSlot].GetChild(1).gameObject.SetActive(false);
        slots[swapingItemSlot].GetChild(0).GetComponent<Image>().color = defaultSelectionColor;

        slots[selectedSlot].GetChild(0).gameObject.GetComponent<Image>().color = items[selectedSlot].GetComponent<Item>().BackgroundColor;
        slots[selectedSlot].GetChild(1).gameObject.GetComponent<Image>().sprite = items[selectedSlot].GetComponent<Item>().ItemIcon;
        slots[selectedSlot].GetChild(1).gameObject.SetActive(true);

    }
    public void AsignOrDeasignItemToQuickItemAccessSystem(int QIASslotIndex)
    {
        slots[selectedSlot].GetComponent<Animator>().SetBool("Interacted", true);
        animationTimers[selectedSlot] = maxAnimationTimer;



        if (quickItemAccess.Items.ContainsKey(QIASslotIndex))
        {
            if (quickItemAccess.Items[QIASslotIndex] != null && quickItemAccess.Items[QIASslotIndex].GetComponent<Item>() == items[selectedSlot].GetComponent<Item>())
            {
                quickItemAccess.DeasignItemFromSlot(QIASslotIndex);
                return;
            }
        }

        quickItemAccess.AsignItemToSlot(QIASslotIndex, items[selectedSlot]);
    }
    #endregion

    #region Selected slot
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

        SetDescription();
    }
    #endregion

    #region Input sequences
    public void SwapSequence()
    {
        if (!swapSequenceInitiated)
        {
            slots[selectedSlot].GetComponent<Animator>().SetBool("Interacted", true);
            animationTimers[selectedSlot] = maxAnimationTimer;

            swapingItemSlot = selectedSlot;
        }
        else
        {
            SwapItems();
        }

        swapSequenceInitiated = !swapSequenceInitiated;
    }
    #endregion

    private void SetDescription()
    {
        if (!items.ContainsKey(selectedSlot) || items[selectedSlot] == null)
        {
            infoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Item";
            infoPanel.transform.GetChild(2).gameObject.SetActive(false);
            infoPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "None";

            return;
        }

        infoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = items[selectedSlot].GetComponent<Item>().Title;
        infoPanel.transform.GetChild(2).gameObject.SetActive(true);
        infoPanel.transform.GetChild(2).GetComponent<Image>().sprite = items[selectedSlot].GetComponent<Item>().ItemIcon;
        infoPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = items[selectedSlot].GetComponent<Item>().Desription;
    }
}
