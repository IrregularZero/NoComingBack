using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EventItem : HandCompatibleItem
{
    [SerializeField]
    private GameObject player;

    private void Start()
    {
        backgroundColor = new Color(49, 53, 175, 1);
        type = "Event Item";
    }
    public override void Use()
    {
        base.Use();
        
        player.GetComponent<EventCoreSystem>().enabled = false;
        player.GetComponent<EventCoreSystem>().ActiveEventItem = activeHandCompatibleItem;
        player.GetComponent<EventCoreSystem>().enabled = true;
    }
}
