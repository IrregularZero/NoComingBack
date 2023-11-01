using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    private bool isTaken = false;

    #region Properties
    public bool IsTaken 
    {
        get
        {
            return isTaken;
        }
        set
        {
            isTaken = value;
        }
    }
    #endregion

    private void Update()
    {
        if (transform.GetChild(0).childCount > 0)
            isTaken = true;
        else
            isTaken = false;
    }
}
