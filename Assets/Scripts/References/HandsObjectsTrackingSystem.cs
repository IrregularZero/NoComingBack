using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsObjectsTrackingSystem : MonoBehaviour
{
    private Transform rightHand;
    private Transform leftHand;

    #region Properties
    public Transform RightHand 
    {
        get
        {
            return rightHand;
        }
    }
    public Transform LeftHand 
    {
        get
        {
            return leftHand;
        }
    }
    public bool HasFreeHand 
    {
        get
        {
            if (rightHand.childCount <= 0)
                return true;
            else if (leftHand.childCount <= 0)
                return true;
            else 
                return false;
        } 
    }
    public Transform FreeHand
    {
        get
        {
            if (rightHand.childCount <= 0)
                return rightHand;
            else
                return leftHand;
        }
    }
    #endregion
    private void Start()
    {
        rightHand = transform.GetChild(0);
        leftHand = transform.GetChild(1);
    }
}
