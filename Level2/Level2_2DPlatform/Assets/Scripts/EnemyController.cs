using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // COMPONENTS
    private Transform groundDetection;
    private SpriteRenderer spriteRenderer;

    // Ground Check
    private LayerMask groundLayer;

    // MOVEMENTS
    [Header("Enemy Movement Settings")]
    [SerializeField] private Vector2 direction = Vector2.right;
    [Range(0.5f, 2f)]
    [SerializeField] private float moveSpeed = 1f;


    private void Awake()
    {
        groundDetection = transform.GetChild(1);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        CrabMove();
    }

    /// <summary>
    /// Detects edges and obstacles using raycasting (ground collision).
    /// </summary>
    /// <param name="rayLenght"></param>
    /// <param name="direction"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    private bool GeneralDetection(float rayLenght, Vector3 direction, Color color)
    {
        Debug.DrawRay(groundDetection.position, direction * rayLenght, color);
        return Physics2D.Raycast(groundDetection.position, direction, rayLenght, groundLayer);
    }

    /// <summary>
    /// Controls the crab movement and direction change upon reaching edges.
    /// </summary>
    private void CrabMove()
    {
        // If ground below its not detected or the wall is detected, flip the direction
        if (!GeneralDetection(1.5f, Vector2.down, Color.yellow) || GeneralDetection(0.5f, direction, Color.green))
        {
            direction = -direction; // Reverse direction
            spriteRenderer.flipX = !spriteRenderer.flipX; // Flip sprite

            //Invert the groundDetection X pointer
            groundDetection.localPosition = new Vector3(-groundDetection.localPosition.x, groundDetection.localPosition.y, groundDetection.localPosition.z);
        }

        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }
}
