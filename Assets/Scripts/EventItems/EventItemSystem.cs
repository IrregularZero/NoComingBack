using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventItemSystem : MonoBehaviour
{
    protected string areaTag;
    protected bool eventActive;

    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == areaTag)
        {
            Event(true);
        }
    }
    protected void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == areaTag)
        {
            Event(false);
        }
    }

    protected virtual void Event(bool status)
    {
        eventActive = status;
    }
}
