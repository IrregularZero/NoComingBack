using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReferenceCenter : MonoBehaviour
{
    // This script contains references for newly initiated prefabs
    // References that can't be added to prefabs should be contained here
    // Component should be contained in Playeground/Arena

    [SerializeField]
    private GameObject player;

    #region Properties
    public GameObject Player 
    {
        get
        {
            return player;
        } 
    }
    #endregion
}
