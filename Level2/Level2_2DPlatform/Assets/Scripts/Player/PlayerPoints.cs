using System;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    // CONFIGURATION
    [Header("Points Configuration")]
    [Tooltip("Points awarded to the player for collecting a power-up.")]
    [Range(100, 500)]
    [SerializeField] private int pointsPerPowerUp = 250;


    // VARIABLES
    private string powerUpTag = "PowerUp";

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(powerUpTag))
        {
            RegisterPowerUpPoints();
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// Registers points for collecting a power-up.
    /// </summary>
    private void RegisterPowerUpPoints()
    {
        LevelManager.Instance.CurrentPlayerPowerUps++;
        LevelManager.Instance.PowerUpsRemaining--;
    }
}
