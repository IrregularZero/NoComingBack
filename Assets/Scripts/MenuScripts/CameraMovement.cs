using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float initialRotation;

    [SerializeField]
    private float rotationSpeed = 1f;
    [SerializeField]
    private float turnMultiplier = 1f;

    private void Start()
    {
        initialRotation = transform.rotation.y * 100;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, initialRotation * Mathf.Sin(Time.time * rotationSpeed) * turnMultiplier, 0);
    }
}
