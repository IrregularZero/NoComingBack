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

    [SerializeField]
    private bool roomEntrance = false;
    [SerializeField]
    private bool entrance = true;

    protected Animator animator;
    private ChapterGameController_l1 gameController;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        if (roomEntrance)
            gameController = transform.parent.parent.GetChild(1).gameObject.GetComponent<ChapterGameController_l1>();
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

        if (roomEntrance)
        {
            if (entrance)
                gameController.Initiate();
            else
                gameController.Deactivate();
        }

        isOpened = true;
        animator.SetTrigger("Open");
    }
}
