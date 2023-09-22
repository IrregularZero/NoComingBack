using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterGameController_l1 : MonoBehaviour
{
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

    private GameObject activeLayout;

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

    public void Initiate()
    {
        Transform initializationPoint = transform.parent.GetChild(0).GetChild(0);

        activeLayout = Instantiate(layouts[Random.Range(0, layouts.Count - 1)], initializationPoint);
    }
    public void Deactivate()
    {
        Destroy(activeLayout);
    }
}
