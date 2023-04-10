using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuickItemAccessSystem : MonoBehaviour
{
    [SerializeField]
    private Dictionary<int, GameObject> items; // Game object should contain Item script descendand
    [SerializeField]
    private List<Transform> slots; // Game object should contain Item script descendand
    [SerializeField]
    private int maxSize = 4;

    private float maxAnimationTimer = 0.2f;
    private List<float> animationTimers;

    private Color slotDefaultColor = new Color(0.1603774f, 0.1603774f, 0.1603774f);

    // Test constructions
    public bool updateItems = false;
    public GameObject GOSlot0;
    public GameObject GOSlot1;
    public GameObject GOSlot2;
    public GameObject GOSlot3;

    #region Properties
    public Dictionary<int, GameObject> Items
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
        items = new Dictionary<int, GameObject>();

        slots = new List<Transform>();
        animationTimers = new List<float>();
        for (int i = 0; i < 4; i++)
        {
            slots.Add(transform.GetChild(i));
            animationTimers.Add(0);
        }
    }

    private void Update()
    {
        // Test construction
        if (updateItems)
        {
            if (GOSlot0 != null)
                AsignItemToSlot(0, GOSlot0);
            else if (GOSlot1 == null && items.ContainsKey(0) && items[0] != null)
                DeasignItemFromSlot(0);

            if (GOSlot1 != null)
                AsignItemToSlot(1, GOSlot1);
            else if (GOSlot1 == null && items.ContainsKey(1) && items[1] != null)
                DeasignItemFromSlot(1);

            if (GOSlot2 != null)
                AsignItemToSlot(2, GOSlot2);
            else if (GOSlot1 == null && items.ContainsKey(2) && items[2] != null)
                DeasignItemFromSlot(2);

            if (GOSlot3 != null)
                AsignItemToSlot(3, GOSlot3);
            else if (GOSlot1 == null && items.ContainsKey(3) && items[3] != null)
                DeasignItemFromSlot(3);

            updateItems = false;
        }

        for (int i = 0; i < 4; i++)
        {
            if (animationTimers[i] > 0)
                animationTimers[i] -= Time.deltaTime;
            else
                slots[i].GetComponent<Animator>().SetBool("UseAnimationEnabled", false);
        }
    }

    public void AsignItemToSlot(int slot, GameObject asigningItem)
    {
        if (slot >= maxSize)
            return;

        // If slot is taken, the asigning item will replace previous one, or create slot for itself
        if (items.ContainsKey(slot))
            items[slot] = asigningItem;
        else
            items.Add(slot, asigningItem);

        slots[slot].GetComponent<Animator>().SetBool("UseAnimationEnabled", true);
        animationTimers[slot] = maxAnimationTimer;

        slots[slot].GetChild(0).GetComponent<Image>().color = asigningItem.GetComponent<Item>().BackgroundColor;
        slots[slot].GetChild(1).GetComponent<Image>().sprite = asigningItem.GetComponent<Item>().ItemIcon;
        slots[slot].GetChild(1).gameObject.SetActive(true);
    }
    public void DeasignItemFromSlot(int slot)
    {
        if (slot >= maxSize) return;

        if (items.ContainsKey(slot))
            items[slot] = null;

        slots[slot].GetComponent<Animator>().SetBool("UseAnimationEnabled", true);
        animationTimers[slot] = maxAnimationTimer;

        slots[slot].GetChild(0).GetComponent<Image>().color = slotDefaultColor;
        slots[slot].GetChild(1).gameObject.SetActive(false);
    }
    public void UseAsignedItem(int slot)
    {
        slots[slot].GetComponent<Animator>().SetBool("UseAnimationEnabled", true);
        animationTimers[slot] = maxAnimationTimer;

        if (slot >= maxSize)
            return;
        else if (items[slot] == null)
            return;

        items[slot].GetComponent<Item>().Use();
    }
}
