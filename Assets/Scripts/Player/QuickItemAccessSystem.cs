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

    #region Properties
    public Dictionary<int, GameObject> Items
    {
        get
        {
            return items;
        }
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
