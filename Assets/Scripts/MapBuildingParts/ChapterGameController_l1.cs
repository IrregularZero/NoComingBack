using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChapterGameController_l1 : MonoBehaviour
{
    private bool initialized = false;
    private bool isActive = false;

    [SerializeField]
    private int enemyLayoutVariantsNumber;
    [SerializeField]
    private GameObject enemyLayout0;
    [SerializeField]
    private GameObject enemyLayout1;
    [SerializeField]
    private GameObject enemyLayout2;
    [SerializeField]
    private GameObject enemyLayout3;
    [SerializeField]
    private GameObject enemyLayout4;

    private List<GameObject> layouts;
    [SerializeField]
    private string lockdownPrompt = "Finish the dungeon first";
    [SerializeField]
    private TheDoorBehaviour entranceDoor;
    private string entranceDoorPrompt;
    [SerializeField]
    private TheDoorBehaviour exitDoor;
    private string exitDoorPrompt;
    [SerializeField]
    private GameObject pocket;

    private GameObject activeLayout;
    private NPCVitality[] enemiesLives;
    [SerializeField]
    private float timeToStartScanning;
    private bool shouldBeScanned = false;

    private void Start()
    {
        #region Layouts_definition
        layouts = new List<GameObject>();

        for (int i = 0; i < enemyLayoutVariantsNumber; i++)
        {
            switch (i)
            {
                case 0: layouts.Add(enemyLayout0); break;
                case 1: layouts.Add(enemyLayout1); break;
                case 2: layouts.Add(enemyLayout2); break;
                case 3: layouts.Add(enemyLayout3); break;
                case 4: layouts.Add(enemyLayout4); break;
                default: Debug.Log($"Enemy variants number exceeds maximum by {i - i + 1}"); break;
            }
        }
        #endregion
    }
    private void Update()
    {
        if (isActive && shouldBeScanned)
        {
            bool enemiesDead = true;
            for (int i = 0; i < enemiesLives.Length; i++)
            {
                if (enemiesLives[i].Health > 0)
                {
                    enemiesDead = false;
                    break;
                }
            }

            if (enemiesDead)
            {
                Deactivate();
            }
        }
    }

    public void Initiate()
    {
        if (initialized)
            return;

        Transform initializationPoint = transform.parent.GetChild(0).GetChild(0);

        activeLayout = Instantiate(layouts[Random.Range(0, layouts.Count - 1)], initializationPoint);

        initialized = true;

        entranceDoor.CanBeOpened = false;
        entranceDoorPrompt = entranceDoor.promptMessage;
        entranceDoor.promptMessage = lockdownPrompt;

        exitDoor.CanBeOpened = false;
        exitDoorPrompt = exitDoor.promptMessage;
        exitDoor.promptMessage = lockdownPrompt;

        pocket.SetActive(true);
        enemiesLives = activeLayout.GetComponentsInChildren<NPCVitality>().Where(child => child.tag == "Enemy").ToArray();
        isActive = true;

        StartCoroutine(delayTillScan());
    }

    private IEnumerator delayTillScan()
    {
        yield return new WaitForSeconds(timeToStartScanning);

        shouldBeScanned = true;
    }
    
    public void Deactivate()
    {
        isActive = false;

        DropItems();

        entranceDoor.CanBeOpened = true;
        entranceDoor.promptMessage = entranceDoorPrompt;
        exitDoor.CanBeOpened = true;
        exitDoor.promptMessage = exitDoorPrompt;

        pocket.SetActive(false);

        Destroy(activeLayout);
    }

    public void DropItems()
    {
        DropList[] bodies = activeLayout.GetComponentsInChildren<DropList>().Where(child => child.tag == "Enemy").ToArray();
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].DropItem(Random.Range(1f, 100f), transform.parent);
        }
    }
}
