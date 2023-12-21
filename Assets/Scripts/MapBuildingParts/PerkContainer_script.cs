using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkContainer_script : MonoBehaviour
{
    private List<int> alreadyTakenPerks;
    private List<GameObject> perkMonoliths;
    #region Perk slots
    [SerializeField]
    private GameObject perkMonolith1;
    [SerializeField]
    private GameObject perkMonolith2;
    [SerializeField]
    private GameObject perkMonolith3;
    [SerializeField]
    private GameObject perkMonolith4;
    [SerializeField]
    private GameObject perkMonolith5;
    [SerializeField]
    private GameObject perkMonolith6;
    [SerializeField]
    private GameObject perkMonolith7;
    [SerializeField]
    private GameObject perkMonolith8;
    [SerializeField]
    private GameObject perkMonolith9;
    [SerializeField]
    private GameObject perkMonolith10;
    [SerializeField]
    private GameObject perkMonolith11;
    [SerializeField]
    private GameObject perkMonolith12;
    [SerializeField]
    private GameObject perkMonolith13;
    [SerializeField]
    private GameObject perkMonolith14;
    [SerializeField]
    private GameObject perkMonolith15;
    [SerializeField]
    private GameObject perkMonolith16;
    [SerializeField]
    private GameObject perkMonolith17;
    [SerializeField]
    private GameObject perkMonolith18;
    [SerializeField]
    private GameObject perkMonolith19;
    [SerializeField]
    private GameObject perkMonolith20;
    [SerializeField]
    private GameObject perkMonolith21;
    [SerializeField]
    private GameObject perkMonolith22;
    [SerializeField]
    private GameObject perkMonolith23;
    [SerializeField]
    private GameObject perkMonolith24;
    [SerializeField]
    private GameObject perkMonolith25;
    [SerializeField]
    private GameObject perkMonolith26;
    [SerializeField]
    private GameObject perkMonolith27;
    [SerializeField]
    private GameObject perkMonolith28;
    [SerializeField]
    private GameObject perkMonolith29;
    [SerializeField]
    private GameObject perkMonolith30;
    #endregion
    [SerializeField]
    private int amountOfPreks;

    private void Start()
    {
        alreadyTakenPerks = new List<int>();
        perkMonoliths = new List<GameObject>();
        for (int i = 0; i < amountOfPreks; i++)
        {
            switch (i)
            {
                case 0: perkMonoliths.Add(perkMonolith1); break;
                case 1: perkMonoliths.Add(perkMonolith2); break;
                case 2: perkMonoliths.Add(perkMonolith3); break;
                case 3: perkMonoliths.Add(perkMonolith4); break;
                case 4: perkMonoliths.Add(perkMonolith5); break;
                case 5: perkMonoliths.Add(perkMonolith6); break;
                case 6: perkMonoliths.Add(perkMonolith7); break;
                case 7: perkMonoliths.Add(perkMonolith8); break;
                case 8: perkMonoliths.Add(perkMonolith9); break;
                case 9: perkMonoliths.Add(perkMonolith10); break;
                case 10: perkMonoliths.Add(perkMonolith11); break;
                case 11: perkMonoliths.Add(perkMonolith12); break;
                case 12: perkMonoliths.Add(perkMonolith13); break;
                case 13: perkMonoliths.Add(perkMonolith14); break;
                case 14: perkMonoliths.Add(perkMonolith15); break;
                case 15: perkMonoliths.Add(perkMonolith16); break;
                case 16: perkMonoliths.Add(perkMonolith17); break;
                case 17: perkMonoliths.Add(perkMonolith18); break;
                case 18: perkMonoliths.Add(perkMonolith19); break;
                case 19: perkMonoliths.Add(perkMonolith20); break;
                case 20: perkMonoliths.Add(perkMonolith21); break;
                case 21: perkMonoliths.Add(perkMonolith22); break;
                case 22: perkMonoliths.Add(perkMonolith23); break;
                case 23: perkMonoliths.Add(perkMonolith24); break;
                case 24: perkMonoliths.Add(perkMonolith25); break;
                case 25: perkMonoliths.Add(perkMonolith26); break;
                case 26: perkMonoliths.Add(perkMonolith27); break;
                case 27: perkMonoliths.Add(perkMonolith28); break;
                case 28: perkMonoliths.Add(perkMonolith29); break;
                case 29: perkMonoliths.Add(perkMonolith30); break;
                default: Debug.Log($"PerkContainer exceeds max by {amountOfPreks + i}"); break;
            }
        }
    }

    public GameObject ReturnPerkInRange(int minRangeIncluding, int maxRangeIncluding, bool setAsUsed = true)
    {
        if (minRangeIncluding < 0)
        {
            Debug.Log($"Min range < 0");
            return new GameObject();
        }
        else if (maxRangeIncluding > amountOfPreks)
        {
            Debug.Log("Max range exceeds amountOfPerks");
            return new GameObject();
        }

        // Generating perk index never used before
        GameObject perkToBeSent = new GameObject();
        int index = -1;
        while (index < 0)
        {
            bool validIndex = true;
            int newIndex = Random.Range(minRangeIncluding, maxRangeIncluding - 1);
            for (int i = 0; i < alreadyTakenPerks.Count; i++)
            {
                if (alreadyTakenPerks[i] == newIndex)
                {
                    validIndex = false;
                    break;
                }
            }

            if (validIndex)
            {
                index = newIndex;
            }
        }

        // Setting perk's index as used
        if (setAsUsed)
        {
            alreadyTakenPerks.Add(index);
        }

        return perkMonoliths[index];
    }
}
