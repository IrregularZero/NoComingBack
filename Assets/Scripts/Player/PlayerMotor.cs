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
    private bool isKnockingDown = false;
    [SerializeField]
    private float knockDownSpeedMultyplier = 5f;

    // Crouch
    private bool isCrouching = false;
    private bool lerpCrouch;
    private float crouchTimer;

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
        RaycastHit hitInfo; // Used to store info about raycast hit

        #region Lerp Crouch
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;

            if (isCrouching)
                characterController.height = Mathf.Lerp(characterController.height, 1, p);
            else
                characterController.height = Mathf.Lerp(characterController.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
        #endregion
        #region Is Powerslidable?
        // If player is power sliding he deals damage to an enemy on collision
        if (characterController.isGrounded)
        {
            Debug.Log(1);
            isPowerSliding = true;
        }
        else
        {
            Debug.Log(2);
            isPowerSliding = false;
        }
        #endregion
        #region Can drop down further?
        if (characterController.isGrounded)
        {
            isKnockingDown = false;
        }
        #endregion
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 movedirection = new Vector3(input.x, 0, input.y);

        // if sliding player can't change his direction by rotating camera
        if (!isSliding)
            characterController.Move(transform.TransformDirection(movedirection) * speed * Time.deltaTime);
        else
            characterController.Move(slideDirection * speed * (slideSpeedMultiplier * (isPowerSliding ? powerSlideSpeedMultiplier : 1)) * Time.deltaTime);

        playerVelocity.y += (gravity + weight * -1) * (isKnockingDown?knockDownSpeedMultyplier: 1) * Time.deltaTime;
        // Adding gravity
        if (characterController.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        characterController.Move(playerVelocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (amountOfJumps != maxAmountOfJumps && characterController.isGrounded)
            amountOfJumps = maxAmountOfJumps;

        if (amountOfJumps > 0)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);

            amountOfJumps--;
        }
    }
    public void KnockDown()
    {
        if (!isKnockingDown && !characterController.isGrounded && amountOfJumps <= 0)
        {
            isKnockingDown = true;
        }
    }
    public void Crouch()
    {
        isCrouching = !isCrouching;
        lerpCrouch = true;
        crouchTimer = 0;

        if (isSliding && !isCrouching)
        {
            isSliding = !isSliding;
        }
    }
    public void Slide()
    {
        isSliding = !isSliding;
        slideDirection = transform.forward;

        Crouch();
    }
}
