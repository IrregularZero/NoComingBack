using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    [SerializeField]
    public float xSensitivity = 30f;
    [SerializeField]
    public float ySensitivity = 30f;

    public void processLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //Calculating camera rotation for looking up and down
        this.xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        this.xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // Applying this to camra transform
        this.cam.transform.localRotation = Quaternion.Euler(xRotation, 0, this.cam.transform.localRotation.z);

        // Rotate player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
