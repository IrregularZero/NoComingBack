using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EventItem : HandCompatibleItem
{

    private void Start()
    {
        backgroundColor = new Color(0.192f, 0.207f, 0.686f, 1f);
        type = "Event Item";
    }
    public override void Use()
    {
        base.Use();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<EventCoreSystem>().enabled = false;
        player.GetComponent<EventCoreSystem>().ActiveEventItem = activeHandCompatibleItem;
        player.GetComponent<EventCoreSystem>().enabled = true;
    }
}
