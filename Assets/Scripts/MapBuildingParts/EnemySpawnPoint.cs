using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField]
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
}
