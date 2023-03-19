using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventItemSystem : MonoBehaviour
{
    [SerializeField]
    protected string areaTag;
    protected bool eventActive;

    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == areaTag)
        {
            Event(true);
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == areaTag)
        {
            Event(false);
        }
    }

    protected virtual void Event(bool status)
    {
        animator.SetBool("IsActive", status);

        eventActive = status;
    }
}
