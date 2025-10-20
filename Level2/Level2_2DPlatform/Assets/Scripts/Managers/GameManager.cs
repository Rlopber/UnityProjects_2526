using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; } // Capitalized 'I' for property naming convention in static instances

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            // Assign the instance and make it persistent across scenes
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Destroy duplicate GameManager instances
            Destroy(gameObject);
        }
    }
}
