using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Vector3 playerVelocity;
    private CharacterController characterController;

    // Move
    [SerializeField]
    private float speed = 10f;

    // Jump
    [SerializeField]
    private int maxAmountOfJumps = 2;
    private int amountOfJumps;
    [SerializeField]
    private float jumpHeight = 1.5f;
    [SerializeField]
    private float weight = 0.5f;
    [SerializeField]
    private float gravity = -9.8f;
    [SerializeField]
    private float knockDownSpeed = 30f;

    // Crouch
    private bool isCrouching = false;
    private bool lerpCrouch;

    // Slide
    private bool isSliding = false;
    private bool isPowerSliding = false;
    private Vector3 slideDirection;
    [SerializeField]
    private float slideSpeedMultiplier = 1.5f;
    [SerializeField]
    private float powerSlideSpeedMultiplier = 2f;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerVelocity);
        RaycastHit hitInfo; // Used to store info about raycast hit

        if (characterController.isGrounded && amountOfJumps < maxAmountOfJumps)
            amountOfJumps = maxAmountOfJumps;
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 movedirection = new Vector3(input.x, 0, input.y);

        // if sliding player can't change his direction by rotating camera
        if (!isSliding)
            characterController.Move(transform.TransformDirection(movedirection) * speed * Time.deltaTime);
        else
            characterController.Move(slideDirection * speed * (slideSpeedMultiplier * (isPowerSliding ? powerSlideSpeedMultiplier : 1)) * Time.deltaTime);

        playerVelocity.y += (gravity + weight * -1) * Time.deltaTime;
        // Adding gravity
        if (characterController.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        characterController.Move(playerVelocity * Time.deltaTime);
    }
    public void Jump()
    {
        Debug.Log(1);
        if (amountOfJumps > 0)
        {
            Debug.Log(2);
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);

            amountOfJumps--;
        }
    }
}
