using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkHoldingSystem : MonoBehaviour
{
    private List<BasePerk> perks;
    [SerializeField]
    private int maxPerksLength;

    private void Start()
    {
        perks = new List<BasePerk>();
    }

    #region Perks manipulation
    public void AddPerk(GameObject perk)
    {
        perk.transform.parent = transform.GetChild(0);

        perks.Add(perk.GetComponent<BasePerk>());
        perks[perks.Count - 1].GetComponent<BasePerk>().EnableEffect();

        Debug.Log($"Added {perks[perks.Count - 1].GetComponent<BasePerk>().Title}");
    }
    #endregion
}
