using Unity.VisualScripting;
using UnityEngine;

public enum EnemyClass { Crab, Octopus, Jumper }

public class EnemyController : MonoBehaviour
{
    // COMPONENTS
    private SpriteRenderer spriteRenderer;
    private float sinCenterY;

    // DETECTORS
    private Transform groundDetection;

    // Ground Check
    private LayerMask groundLayer;

    // MOVEMENTS
    [Header("Enemy Movement Settings")]
    [SerializeField] private EnemyClass enemyClass;
    [SerializeField] private Vector2 direction = Vector2.right;
    [Range(0.5f, 2f)]
    [SerializeField] private float moveSpeed = 1f;

    [ShowHeaderIf("Jumper Settings", "enemyClass", (int)EnemyClass.Jumper)]
    [ShowIf("enemyClass", (int)EnemyClass.Jumper)]
    [Range(0.5f, 3f)]
    [SerializeField] private float sinFrequency = 2f;

    [ShowIf("enemyClass", (int)EnemyClass.Jumper)]
    [Range(0.5f, 3f)]
    [SerializeField] private float sinAmplitude = 0.5f;

    private void Awake()
    {
        groundDetection = transform.GetChild(1);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Start()
    {
        sinCenterY = transform.position.y;
    }

    private void Update()
    {
        EnemyMove();
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
    /// Controls the enemy movement according to the enemy class.
    /// </summary>
    private void EnemyMove()
    {
        switch (enemyClass)
        {
            case EnemyClass.Crab:
                CrabMove();
                break;

            case EnemyClass.Octopus:
                OctopusMove();
                break;

            case EnemyClass.Jumper:
                JumperMove();
                break;
            default:
                Debug.LogWarning("Enemy class not handled in Update()");
                break;
        }
    }

    /// <summary>
    /// Controls the crab's movement and direction change upon reaching edges.
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
    /// Controls the octopus's movement and direction change upon reaching edges.
    /// </summary>
    private void OctopusMove()
    {
        if (groundDetection == null) return;

        if (GeneralDetection(groundDetection.position, 1f, Vector2.up, Color.red) || GeneralDetection(groundDetection.position, 1f, Vector2.down, Color.blue))
        {
            direction = -direction;
        }

        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Controls the jumper's movement using sine wave frequency and amplitude.
    /// </summary>
    private void JumperMove()
    {

        if (groundDetection == null)
        {
            Debug.LogError("GroundDetection is null!");
            return;
        }

        if (GeneralDetection(groundDetection.position, 1f, direction, Color.red))
        {
            direction = -direction;
        }

        Vector3 enemyPosition = transform.position;

        float sin = Mathf.Sin(enemyPosition.x * sinFrequency) * sinAmplitude;

        enemyPosition.y = sinCenterY + sin;
        enemyPosition.x += direction.x * moveSpeed * Time.deltaTime;

        transform.position = enemyPosition;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
     if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage();
        }
    }
}
