using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkMonolithInteractions : Interactible
{
    private void Start()
    {
        promptMessage = "Interact with monolith: F";
    }

    protected override void Interact()
    {
        GameObject.FindGameObjectWithTag("PerkHolder").GetComponent<PerkHoldingSystem>().AddPerk(transform.GetChild(0).gameObject);

        Destroy(gameObject);
    }
}
