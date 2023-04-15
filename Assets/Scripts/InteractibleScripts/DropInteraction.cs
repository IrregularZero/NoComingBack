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
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Getting player GM

        // Here we add Item to inventory
        player.GetComponent<InputManager>().Inventory.AddItem(transform.GetChild(0).gameObject);
        // Then we put Item into safe container for garbage collector not to delete our reference
        transform.GetChild(0).parent = player.GetComponent<InputManager>().Inventory.transform.GetChild(4);

        // And we delete, now empty shell
        Destroy(transform.gameObject);
    }
}
