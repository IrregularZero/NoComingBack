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
    [SerializeField]
    private Transform PerkPlacement;
    private bool perkCanBeTaken = false;

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
        /*
        if (perkCanBeTaken)
        {
            try
            {
                for (int i = 0; i < length; i++)
                {

                }
            }
            catch (System.Exception)
            {
                Destroy(PerkPlacement);
            }
        }
        */
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
                for (int i = 0; i < 3; i++)
                {
                    GameObject perk = GameObject.FindGameObjectWithTag("PerkContainer").GetComponent<PerkContainer_script>().ReturnPerkInRange(0, 25);
                    Instantiate(perk, PerkPlacement.GetChild(i));
                }
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
