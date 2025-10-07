using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]

    [Tooltip("Speed at which the player moves.")]
    [Range(1f, 10f)]
    [SerializeField] private float moveSpeed = 1f;

    [Tooltip("Force applied when the player jumps.")]
    [Range(1f, 3f)]
    [SerializeField] private float jumpForce = 1f;

    // INPUTS
    private float vAxis;   // Vertical axis input
    private float hAxis;   // Horizontal axis input
    private bool jumpInput; // Jump input

    private Vector3 jumpVector = Vector3.zero;

    // GAMEOBJECTS
    private GameObject playerFeet;

    // COMPONENTS
    private SpriteRenderer playerSprite;
    private Rigidbody2D playerRb;

    // GROUND CHECK LAYER
    private LayerMask groundLayer;

    private void Awake()
    {
        // Initializations
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");
        playerFeet = GameObject.Find("PlayerFeet");
    }

    void Update()
    {
        GetInputs();
        MovePlayer();
        FlipSprite();

        CalculateJump();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    /// <summary>
    /// Reads vertical and horizontal input axes.
    /// </summary>
    private void GetInputs()
    {
        // Read Axes Inputs
        vAxis = Input.GetAxisRaw("Vertical");
        hAxis = Input.GetAxisRaw("Horizontal");
        // Debug.Log($"Vertical Axis: {vAxis}, Horizontal Axis: {hAxis}");

        // Read Jump Input
        jumpInput = Input.GetKeyDown(KeyCode.Space);
    }

    /// <summary>
    /// Change player position based on input axes and move speed.
    /// </summary>
    private void MovePlayer()
    {
        transform.position += Vector3.right * hAxis * moveSpeed * Time.deltaTime;
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
        if (hAxis > 0)
            playerSprite.flipX = false; // Facing right
        else if (hAxis < 0)
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
    /// Calculate the Vector3 for the jump
    /// </summary>
    private void CalculateJump()
    {
        if (jumpInput && IsGrounded())
            jumpVector = Vector3.up * jumpForce;
        else if (!IsGrounded())
            jumpVector = Vector3.zero;
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

}