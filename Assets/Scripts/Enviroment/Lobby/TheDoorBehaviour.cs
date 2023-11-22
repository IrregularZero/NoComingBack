using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TheDoorBehaviour : Interactible
{
    [SerializeField]
    protected bool canBeOpened = true;
    protected bool isOpened = false;
    [SerializeField]
    private bool useMaxDistance = true;
    [SerializeField]
    protected float maxDistanceToThePlayer;

    [SerializeField]
    private bool roomEntrance = false;
    [SerializeField]
    private bool entrance = true;

    protected Animator animator;
    private ChapterGameController_l1 gameController;
    private LevelController levelController;

    #region Props
    public bool CanBeOpened 
    {
        get
        {
            return canBeOpened;
        }
        set
        {
            canBeOpened = value;
        }
    }
    public bool IsOpened 
    {
        get
        {
            return isOpened;
        }
        set
        {
            isOpened = value;
        }
    }
    public float MaxDistanceToThePlayer
    {
        get
        {
            return maxDistanceToThePlayer;
        }
        set
        {
            if (value >= 0)
            {
                maxDistanceToThePlayer = value;
            }
        }
    }
    public bool RoomEntrance
    {
        get
        {
            return roomEntrance;
        }
        set
        {
            roomEntrance = value;
        }
    }
    public bool Entrance
    {
        get
        {
            return entrance;
        }
        set
        {
            entrance = value;
        }
    }
    #endregion

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();

        if (roomEntrance)
            gameController = transform.parent.parent.GetChild(1).gameObject.GetComponent<ChapterGameController_l1>();
        else
            levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }
    protected virtual void Update()
    {
        if (isOpened)
        {
            if (useMaxDistance)
            {
                if ((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude > maxDistanceToThePlayer)
                {
                    Close();
                }
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
            {
                gameController.Initiate();
            }
            else
            {
                Debug.Log($"Completed {transform.parent.parent.name}");
            }
        }
        else
        {
            Debug.Log($"Player has started the game!");
            levelController.Initiate();
        }

        Open();
    }

    public void Open()
    {
        isOpened = true;
        animator.SetTrigger("Open");
    }
    public void Close()
    {
        isOpened = false;
        animator.SetTrigger("Close");
    }
}
