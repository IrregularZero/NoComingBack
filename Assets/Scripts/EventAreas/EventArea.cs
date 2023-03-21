using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventArea : MonoBehaviour
{
    [SerializeField]
    protected bool eventActive = false;

    public virtual void Event(bool status)
    {
        eventActive = status;
    }
}
