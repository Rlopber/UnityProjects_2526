using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // COMPONENT
    private HUDController hudController;

    // VARIABLES
    private bool hasWon = false;
    private int powerUpsRemaining = -1;
    private string powerUpTag = "PowerUp";

    // PROPERTIES

    /// <summary>
    /// Returns the number of active power-ups in the scene.
    /// </summary>
    public int PowerUpsRemaining => powerUpsRemaining;

    private void Awake()
    {
        hudController = GetComponentInChildren<HUDController>();
        UpdatePowerUpCount();
    }

    void Update()
    {
        UpdatePowerUpCount();
        CheckWin();
    }


    /// <summary>
    /// Counts power-ups in the scene and notifies the HUD if the value changed.
    /// </summary>
    private void UpdatePowerUpCount()
    {
        // Count all active GameObjects with the PowerUp tag
        int currentCount = GameObject.FindGameObjectsWithTag(powerUpTag).Length;

        // Only update if the value changed (avoids unnecessary HUD calls)
        if (currentCount != powerUpsRemaining)
        {
            powerUpsRemaining = currentCount;
            hudController?.SetPowerUpCount(powerUpsRemaining);
        }
    }

    /// <summary>
    /// Checks if the player has won the level, based on the number of remaining power-ups in the HUD.
    /// </summary>
    private void CheckWin()
    {
        if (!hasWon && hudController != null && powerUpsRemaining == 0)
        {
            hasWon = true;
            Debug.Log("YOU WIN THIS LEVEL!");
        }
    }
}
