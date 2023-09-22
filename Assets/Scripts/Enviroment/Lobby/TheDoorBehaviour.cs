using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TheDoorBehaviour : Interactible
{
    [SerializeField]
    protected bool canBeOpened = true;
    protected bool isOpened = false;
    [SerializeField]
    protected float maxDistanceToThePlayer;

    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        if (isOpened)
        {
            if ((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude > maxDistanceToThePlayer)
            {
                isOpened = false;
                animator.SetTrigger("Close");
            }
        }
    }

    protected override void Interact()
    {
        if (!canBeOpened)
            return;

        isOpened = true;
        animator.SetTrigger("Open");
    }
}
