using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // COMPONENT
    private HUDController hudController;

    // VARIABLES
    private bool hasWon = false;

    private void Awake()
    {
        hudController = GetComponentInChildren<HUDController>();
    }

    void Update()
    {
        CheckWin();
    }

    /// <summary>
    /// Checks if the player has won the level, based on the number of remaining power-ups in the HUD.
    /// </summary>
    private void CheckWin()
    {
        if (!hasWon && hudController != null && hudController.PowerUpsRemaining == 0)
        {
            hasWon = true;
            Debug.Log("YOU WIN THIS LEVEL!");
        }
    }
}
