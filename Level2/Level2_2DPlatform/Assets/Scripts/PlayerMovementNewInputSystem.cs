using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementNewInputSystem : MonoBehaviour
{
    [Header("Movement Settings")]

    [Tooltip("Speed at which the player moves.")]
    [Range(1f, 10f)]
    [SerializeField] private float moveSpeed = 1f;

    [Tooltip("Force applied when the player jumps.")]
    [Range(1f, 3f)]
    [SerializeField] private float jumpForce = 1f;

    [Space]
    public TMP_Text inputDeviceText;

    // INPUTS
    private Vector2 movementInput;
    private bool jumpInput;
    private bool attackInput;

    // INPUTS ACTIONS
    private InputSystem_Actions playerControls;
    private Vector3 jumpVector = Vector3.zero;

    // GAMEOBJECTS
    private GameObject playerFeet;

    // COMPONENTS
    private SpriteRenderer playerSprite;
    private Rigidbody2D playerRb;
    private Animator animator;

    // GROUND CHECK LAYER
    private LayerMask groundLayer;

    // Player was in ground the previous frame
    private bool wasGrounded = true;

    private void Awake()
    {
        // Initializations
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");
        playerFeet = GameObject.Find("PlayerFeet");

        animator = GetComponentInChildren<Animator>();

        playerControls = new InputSystem_Actions();

        // Initially check the device being use
        UpdateDeviceText();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Update()
    {
        GetInputs();
        MovePlayer();
        FlipSprite();

        CalculateJump();

        // Check if input device has changed
        UpdateDeviceText();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    /// <summary>
    /// Disable actions when the object is disable
    /// </summary>
    private void OnDisable()
    {
        playerControls.Disable();
    }

    /// <summary>
    /// Reads vertical and horizontal input axes.
    /// </summary>
    private void GetInputs()
    {
        // Movement
        movementInput = playerControls.Player.Move.ReadValue<Vector2>();

        // Jump
        jumpInput = playerControls.Player.Jump.triggered;

        // Attack
        attackInput = playerControls.Player.Attack.triggered;

        // Trigger vibration if attack button is pressed
        if (attackInput && Gamepad.current != null)
        {
            StartCoroutine(VibrateGamepad());
        }
    }

    /// <summary>
    /// Change player position based on input axes and move speed.
    /// </summary>
    private void MovePlayer()
    {
        transform.position += Vector3.right * movementInput.x * moveSpeed * Time.deltaTime;

        // De/active Animation
        animator.SetFloat("Speed", Mathf.Abs(movementInput.x));
    }

    /// <summary>
    /// Flips the player's sprite horizontally based on the current horizontal input.
    /// </summary>
    /// <remarks>
    /// This method is commonly used to change the direction the player character is facing 
    /// in a 2D game. If the horizontal input is positive, the sprite faces right; 
    /// if negative, it faces left.
    /// </remarks>
    private void FlipSprite()
    {
        if (movementInput.x > 0)
            playerSprite.flipX = false; // Facing right
        else if (movementInput.x < 0)
            playerSprite.flipX = true;  // Facing left
    }


    /// <summary>
    /// Determines whether the player is currently standing on the ground.
    /// Uses a 2D raycast from the player's feet downward to detect collisions with objects on the ground layer.
    /// </summary>
    /// <returns>Returns true if the player is grounded; otherwise, false.</returns>
    private bool IsGrounded()
    {
        return Physics2D.Raycast(playerFeet.transform.position, Vector3.down, 0.5f, groundLayer);
    }

    /// <summary>
    /// Calculates the Vector3 for the jump movement.
    /// </summary>
    private void CalculateJump()
    {
        if (jumpInput && IsGrounded())
        {
            jumpVector = Vector3.up * jumpForce;
            animator.SetBool("IsJumping", true);
        }
        else if (!IsGrounded())
        {
            jumpVector = Vector3.zero;
        }

        bool grounded = IsGrounded();
        if (grounded && !wasGrounded)
        {
            animator.SetBool("IsJumping", false);
        }
        wasGrounded = grounded;
    }

    /// <summary>
    /// Applies an instantaneous upward force to the player to perform a jump.
    /// </summary>
    /// <remarks>
    /// Uses a 2D physics impulse to propel the player according to the configured jump vector.
    /// Typically called when the jump input is detected and the player is grounded.
    /// </remarks>
    private void Jump()
    {
        playerRb.AddForce(jumpVector, ForceMode2D.Impulse);
    }


    /// <summary>
    /// Updates the on-screen text to show whether the player is using a keyboard or a gamepad.
    /// </summary>
    void UpdateDeviceText()
    {
        if (Keyboard.current != null && Keyboard.current.anyKey.isPressed)
        {
            inputDeviceText.text = "Using Keyboard";
        }
        // TODO check gamepad active with all controls correctly
        else if (Gamepad.current != null && Gamepad.current.leftStick.ReadValue().magnitude > 0)
        {
            inputDeviceText.text = "Using Gamepad";

        }
    }

    /// <summary>
    /// Draws gizmos in the Unity Editor to visualize the ground check raycast.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (playerFeet != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 start = playerFeet.transform.position;
            Vector3 end = start + Vector3.down * 0.5f;

            Gizmos.DrawLine(start, end);
            Gizmos.DrawSphere(end, 0.05f);
        }
    }

    private IEnumerator VibrateGamepad()
    {
        if (Gamepad.current != null)
        {
            // Vibrate gamepad during 0.23sec, intensity medium
            Gamepad.current.SetMotorSpeeds(0.5f, 0.5f);
            yield return new WaitForSeconds(0.25f);
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }
}