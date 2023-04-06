using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EventItem : HandCompatibleItem
{
    [SerializeField]
    private GameObject player;

    public override void Use()
    {
        base.Use();
        
        player.GetComponent<EventCoreSystem>().enabled = false;
        player.GetComponent<EventCoreSystem>().ActiveEventItem = activeHandCompatibleItem;
        player.GetComponent<EventCoreSystem>().enabled = true;
    }
}
