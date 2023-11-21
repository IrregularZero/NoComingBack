using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropList : MonoBehaviour
{
    private bool droppedItems = false;

    [SerializeField]
    private int amountofDrops = 5;
    [SerializeField]
    private GameObject drop0;
    [SerializeField]
    private GameObject drop1;
    [SerializeField]
    private GameObject drop2;
    [SerializeField]
    private GameObject drop3;
    [SerializeField]
    private GameObject drop4;

    [SerializeField]
    private float drop0Cap;
    [SerializeField]
    private float drop1Cap;
    [SerializeField]
    private float drop2Cap;
    [SerializeField]
    private float drop3Cap;
    [SerializeField]
    private float drop4Cap;

    public void DropItem(float points, Transform level) // function needs Level to be set as a parent, for easier deletion
    {
        if (droppedItems)
            return;

        // Basically last drop need the most of points, therefore it will be checked the first
        if (points >= drop4Cap)
        {
            Instantiate(drop4, transform.position + Vector3.up, Quaternion.identity, level);
        }
        else if (points >= drop3Cap)
        {
            Instantiate(drop3, transform.position + Vector3.up, Quaternion.identity, level);
        }
        else if (points >= drop2Cap)
        {
            Instantiate(drop2, transform.position + Vector3.up, Quaternion.identity, level);
        }
        else if (points >= drop1Cap)
        {
            Instantiate(drop1, transform.position + Vector3.up, Quaternion.identity, level);
        }
        else if (points >= drop0Cap)
        {
            Instantiate(drop0, transform.position + Vector3.up, Quaternion.identity, level);
        }

        droppedItems = true;
    }
}
