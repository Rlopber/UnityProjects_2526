using UnityEngine;

/// <summary>
/// Handles the ball collision and creates an explosion effect when it hits the ground.
/// In this version, the explosion is instantiated as a child of the ball.
/// </summary>
public class ExplodeBall : MonoBehaviour
{
    [Tooltip("Prefab for the explosion effect.")]
    public GameObject explosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 1. Disable the ball's mesh so it becomes invisible
            // The ball object still exists, which allows the explosion prefab to be its child
            gameObject.GetComponent<MeshRenderer>().enabled = false;

            // 2. Instantiate the explosion prefab at the ball's position
            // The 'transform' parameter sets the ball as the parent of the explosion
            Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity,
                transform
            );

            // 3. Destroy the ball object after a short delay (0.1 seconds)
            // The short delay ensures the explosion prefab has time to initialize
            Destroy(gameObject, 0.1f);
        }
    }
}
