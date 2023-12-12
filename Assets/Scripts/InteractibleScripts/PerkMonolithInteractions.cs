using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerkMonolithInteractions : Interactible
{
    private void Start()
    {
        promptMessage = "Interact with monolith: F";

        AppearanceSetup();
    }

    private void AppearanceSetup()
    {
        BasePerk perk = transform.GetChild(0).GetComponent<BasePerk>();

        transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite = perk.Logo;
        transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = perk.Title;
        transform.GetChild(1).GetChild(0).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = perk.Description;
        transform.GetChild(1).GetChild(0).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text.Replace("\\n", "\n");
    }

    protected override void Interact()
    {
        GameObject.FindGameObjectWithTag("PerkHolder").GetComponent<PerkHoldingSystem>().AddPerk(transform.GetChild(0).gameObject);

        Destroy(gameObject);
    }
}
