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


    private void InitializeEnemy()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        switch (enemyClass)
        {
            case EnemyClass.Crab:
                if (transform.childCount > 1)
                    groundDetection = transform.GetChild(1);

                direction.y = 0f;
                break;

            case EnemyClass.Octopus:

                groundDetectionTop = null;
                groundDetectionBottom = null;

                foreach (var child in children)
                {
                    if (child == this.transform) continue;

                    if (groundDetectionTop == null || child.position.y > groundDetectionTop.position.y)
                    {
                        groundDetectionTop = child;
                    }

                    if (groundDetectionBottom == null || child.position.y < groundDetectionBottom.position.y)
                    {
                        groundDetectionBottom = child;
                    }
                }

                direction.x = 0f;
                break;


            default:
                Debug.LogWarning("Enemy class not handled in InitializeEnemy()");
                break;
        }
    }

    /// <summary>
    /// Detects edges and obstacles using raycasting (ground collision).
    /// </summary>
    /// <param name="rayLenght"></param>
    /// <param name="direction"></param>
    /// <param name="color"></param>
    /// <returns></returns>
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
    /// Controls the crab movement and direction change upon reaching edges.
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
