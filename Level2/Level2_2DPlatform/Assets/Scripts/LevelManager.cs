using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private HUDController hudController;
    private bool hasWon = false;

    private void Awake()
    {
        hudController = GetComponentInChildren<HUDController>();

        if (hudController == null)
            Debug.LogError("HUDController not found in children!");
    }

    void Update()
    {
        CheckWin();
    }

    private void CheckWin()
    {
        if (!hasWon && hudController != null && hudController.PowerUpsRemaining == 0)
        {
            hasWon = true;
            Debug.Log("YOU WIN THIS LEVEL!");
        }
    }
}
