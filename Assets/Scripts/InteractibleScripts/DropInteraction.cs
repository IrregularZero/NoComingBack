using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInteraction : Interactible
{
    private void Start()
    {
        promptMessage = $"Take {transform.GetChild(0).GetComponent<Item>().Title}";
    }
    protected override void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Here we simply add Item to inventory
        player.GetComponent<InputManager>().Inventory.AddItem(transform.GetChild(0).gameObject);
    }
}
