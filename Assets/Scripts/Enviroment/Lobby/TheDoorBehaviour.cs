using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TheDoorBehaviour : Interactible
{
    [SerializeField]
    private bool canBeOpened = true;
    private bool isOpened = false;
    [SerializeField]
    private float maxDistanceToThePlayer;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
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
