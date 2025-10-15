using Unity.VisualScripting;
using UnityEngine;
public enum EnemyClass { Crab, Octopus }

public class EnemyController : MonoBehaviour
{
    // COMPONENTS
    private SpriteRenderer spriteRenderer;

    // DETECTORS
    private Transform groundDetection;
    private Transform groundDetectionTop;
    private Transform groundDetectionBottom;

    // Ground Check
    private LayerMask groundLayer;

    // MOVEMENTS
    [Header("Enemy Movement Settings")]
    [SerializeField] private EnemyClass enemyClass;
    [SerializeField] private Vector2 direction = Vector2.right;
    [Range(0.5f, 2f)]
    [SerializeField] private float moveSpeed = 1f;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("Ground");

        InitializeEnemy();
    }

    private void Update()
    {

        switch (enemyClass)
        {
            case EnemyClass.Crab:
                CrabMove();
                break;

            case EnemyClass.Octopus:
                OctopusMove();
                break;

            default:
                Debug.LogWarning("Enemy class not handled in Update()");
                break;
        }
    }

    /// <summary>
    /// Initializes the enemy's internal components and movement settings based on its class.
    /// </summary>
    /// <remarks>
    /// For <see cref="EnemyClass.Crab"/>, sets <c>groundDetection</c> to the second child and resets vertical direction.
    /// For <see cref="EnemyClass.Octopus"/>, finds the topmost and bottommost child transforms for ground detection
    /// and resets horizontal direction.
    /// Logs a warning if the enemy class is not handled.
    /// </remarks>
    private void InitializeEnemy()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        switch (enemyClass)
        {
            case EnemyClass.Crab:
                if (transform.childCount > 1)
                    groundDetection = transform.GetChild(1);

                break;

            case EnemyClass.Octopus:
                groundDetectionTop = transform.childCount > 1 ? transform.GetChild(1) : null;
                groundDetectionBottom = transform.childCount > 2 ? transform.GetChild(2) : null;

                break;


            default:
                Debug.LogWarning("Enemy class not handled in InitializeEnemy()");
                break;
        }
    }

    /// <summary>
    /// Detects edges and obstacles in the 2D environment using a raycast.
    /// Draws a debug ray for visualization.
    /// </summary>
    /// <param name="origin">The starting point of the ray.</param>
    /// <param name="rayLenght">The length of the ray.</param>
    /// <param name="direction">The direction in which the ray is cast.</param>
    /// <param name="color">The color of the debug ray to visualize the raycast in the Scene view.</param>
    /// <returns>True if the ray hits an object in the groundLayer; otherwise, false.</returns>
    private bool GeneralDetection(Vector3 origin, float rayLenght, Vector3 direction, Color color)
    {
        Debug.DrawRay(origin, direction * rayLenght, color);
        return Physics2D.Raycast(origin, direction, rayLenght, groundLayer);
    }


    /// <summary>
    /// Controls the crab movement and direction change upon reaching edges.
    /// </summary>
    private void CrabMove()
    {
        if (groundDetection == null) return;

        // If ground below its not detected or the wall is detected, flip the direction
        if (!GeneralDetection(groundDetection.position, 1.5f, Vector2.down, Color.yellow) || GeneralDetection(groundDetection.position, 0.5f, direction, Color.green))
        {
            direction = -direction; // Reverse direction
            spriteRenderer.flipX = !spriteRenderer.flipX; // Flip sprite

            //Invert the groundDetection X pointer
            groundDetection.localPosition = new Vector3(-groundDetection.localPosition.x, groundDetection.localPosition.y, groundDetection.localPosition.z);
        }

        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Controls the octopus movement and direction change upon reaching edges.
    /// </summary>
    private void OctopusMove()
    {
        if (groundDetectionTop == null || groundDetectionBottom == null) return;

        if (GeneralDetection(groundDetectionTop.position, 0.2f, Vector2.up, Color.red) || GeneralDetection(groundDetectionBottom.position, 0.2f, Vector2.down, Color.blue))
        {
            direction = -direction;
        }

        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }
}
