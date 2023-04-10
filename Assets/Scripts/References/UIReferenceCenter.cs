using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReferenceCenter : MonoBehaviour
{
    // This script contains references for newly initiated prefabs
    // References that can't be added to prefabs should be contained here

    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private GameObject hands;

    #region Properties
    public Transform CameraTransform 
    {
        get
        {
            return cameraTransform;
        }
    }
    public GameObject Hands 
    {
        get
        {
            return Hands;
        }
    }
    #endregion
}
