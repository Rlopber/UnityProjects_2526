using UnityEngine;

/// <summary>
/// Manages the instantiation of ball prefabs in the scene.
/// Allows creating one or multiple balls at random positions within defined bounds.
/// </summary>
public class BallManager : MonoBehaviour
{
    [Header("Ball Configuration")]
    [Tooltip("Prefab used to instantiate new balls in the scene.")]
    [SerializeField] private GameObject ballPrefab;

    [Range(1, 10)]
    [Tooltip("Maximum number of balls that can be instantiated using the space key.")]
    [SerializeField] private int numberOfBalls = 1;

    [Header("Spawn Position")]
    [SerializeField] private Vector3 minPosition;
    [SerializeField] private Vector3 maxPosition;
    private float minY = 0.5f;



    //private int ballCount = 0;

    private void Start()
    {
        // Optional: uncomment if you want to create all balls automatically at start
        // NewBalls();
    }

    private void Update()
    {
        // ----------------------------------------------------------------------
        // Press SPACE to create new balls in the scene.
        // Two approaches to limit the number of balls:
        //   (1) Use an internal counter variable (ballCount)
        //   (2) Count how many balls exist as children of this container
        // ----------------------------------------------------------------------

        // ======================
        // (1) Version with counter
        // ======================
        //if (Input.GetKeyDown(KeyCode.Space) && ballCount < numberOfBalls)
        //{
        //    NewSingleBall();
        //    ballCount++;
        //}

        // ==============================
        // (2) Version using child count
        // ==============================
        if (Input.GetKeyDown(KeyCode.Space) && transform.childCount < numberOfBalls)
        {
            NewSingleBall();
        }
    }

    /// <summary>
    /// Called automatically by Unity when values are changed in the Inspector.
    /// Ensures that minPosition.y is never below minY and warns the user if corrected.
    /// Also ensures maxPosition.y is not lower than minPosition.y.
    /// </summary>
    private void OnValidate()
    {
        // Force minPosition.y to be at least minY
        if (minPosition.y < minY)
        {
            Debug.LogWarning($"[BallManager] minPosition.y ({minPosition.y}) is lower than {minY}. It will be automatically set to {minY}.");
            minPosition.y = minY;
        }

        // Ensure maxPosition.y is not below minPosition.y
        if (maxPosition.y < minPosition.y)
        {
            Debug.LogWarning($"[BallManager] maxPosition.y ({maxPosition.y}) is lower than minPosition.y ({minPosition.y}). It will be automatically set to {minPosition.y}.");
            maxPosition.y = minPosition.y;
        }
    }


    /// <summary>
    /// Instantiates a single ball at a random position within predefined bounds.
    /// </summary>
    private void NewSingleBall()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("[BallManager] Cannot create ball: no prefab assigned in the Inspector.");
            return;
        }

        //Vector3 randomPosition = new Vector3(
        //    Random.Range(-5f, 5f),
        //    Random.Range(0.5f, 5f),
        //    Random.Range(-5f, 5f)
        //);

        Vector3 randomPosition = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(Mathf.Max(minPosition.y, minY), maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z)
        );

        // Instantiate the ball as a child of this container
        Instantiate(ballPrefab, randomPosition, Quaternion.identity, this.transform);
    }

    /// <summary>
    /// Instantiates multiple balls based on the 'numberOfBalls' variable.
    /// </summary>
    private void NewBalls()
    {
        if (numberOfBalls <= 1)
        {
            NewSingleBall();
            return;
        }

        for (int i = 0; i < numberOfBalls; i++)
        {
            NewSingleBall();
        }
    }
}
