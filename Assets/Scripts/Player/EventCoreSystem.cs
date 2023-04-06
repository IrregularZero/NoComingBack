using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCoreSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject activeEventItem;
    private Animator activeEventItemAnimator;

    public GameObject ActiveEventItem 
    {
        get
        {
            return activeEventItem;
        }
        set
        {
            activeEventItem = value;
        }
    }

    private void OnEnable()
    {
        activeEventItemAnimator = activeEventItem.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activeEventItem != null)
        {
            if (other.tag == activeEventItem.tag)
            {
                other.GetComponent<EventArea>().Event(true);
                activeEventItemAnimator.SetBool("IsActive", true);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (activeEventItem == null || other.tag != activeEventItem.tag)
        {
            other.GetComponent<EventArea>().Event(false);
            activeEventItemAnimator.SetBool("IsActive", false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (activeEventItem != null)
        {
            if (other.tag == activeEventItem.tag)
            {
                other.GetComponent<EventArea>().Event(false);
                activeEventItemAnimator.SetBool("IsActive", false);
            }
        }
    }
}
