using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnSystem : MonoBehaviour
{
    [SerializeField]
    private Vector3 returnPosition;
    [SerializeField]
    private float returnPositionUpdateTimer;

    private CharacterController characterController;

    #region Properties
    public Vector3 ReturnPosition 
    {
        get
        {
            return returnPosition;
        }
        set
        {
            if (value.y <= 50)
            {
                returnPosition = value;
            }
        }
    }
    public float ReturnPositionUpdateTimer 
    {
        get
        {
            return returnPositionUpdateTimer;
        }
        set
        {
            if (value > 0)
            {
                returnPositionUpdateTimer = value;
            }
        }
    }
    #endregion

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        returnPosition = transform.position;

        StartCoroutine(SetReturnPosition());
    }

    public IEnumerator SetReturnPosition()
    {
        yield return new WaitForSeconds(returnPositionUpdateTimer);

        if (characterController.isGrounded)
        {
            returnPosition = transform.position;
        }

        StartCoroutine(SetReturnPosition());
    }
}
