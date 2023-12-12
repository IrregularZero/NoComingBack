using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerkHoldingSystem : MonoBehaviour
{
    private bool isActive = false;
    private List<BasePerk> perks;
    private List<Transform> perkDisplays;
    [SerializeField]
    private int maxPerksLength;

    private void Start()
    {
        perks = new List<BasePerk>();

        perkDisplays = new List<Transform>();
        for (int i = 0; i < transform.GetChild(0).GetChild(1).childCount; i++)
        {
            perkDisplays.Add(transform.GetChild(0).GetChild(1).GetChild(i));
        }
    }

    #region Properties
    public bool IsActive 
    {
        get
        {
            return isActive;
        }
    }
    public int MaxPerkLength 
    {
        get
        {
            return maxPerksLength;
        }
        set
        {
            if (value > 0)
            {
                maxPerksLength = value;
            }
        }
    }
    #endregion

    #region Perks manipulation
    public void AddPerk(GameObject perk)
    {
        perk.transform.parent = transform.GetChild(0);

        perks.Add(perk.GetComponent<BasePerk>());
        perks[perks.Count - 1].GetComponent<BasePerk>().EnableEffect();

        Debug.Log($"Added {perks[perks.Count - 1].GetComponent<BasePerk>().Title}");
    }
    #endregion

    #region Visuals
    public void EnablePerkScreen()
    {
        for (int i = 0; i < perks.Count; i++)
        {
            perkDisplays[i].gameObject.SetActive(true);
            perkDisplays[i].GetChild(1).GetComponent<Image>().sprite = perks[i].Logo;
            perkDisplays[i].GetChild(2).GetComponent<TextMeshProUGUI>().text = perks[i].Title;
            perkDisplays[i].GetChild(3).GetComponent<TextMeshProUGUI>().text = perks[i].Description;
            perkDisplays[i].GetChild(3).GetComponent<TextMeshProUGUI>().text = perkDisplays[i].GetChild(3).GetComponent<TextMeshProUGUI>().text.Replace("\\n", "\n");
        }
        transform.GetChild(0).gameObject.SetActive(true);

        Time.timeScale = 0f;
    }
    public void DisablePerkScreen()
    {
        for (int i = 0; i < perks.Count; i++)
        {
            perkDisplays[i].gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(false);

        Time.timeScale = 1f;
    }
    #endregion
}
