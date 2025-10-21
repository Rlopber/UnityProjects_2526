using UnityEngine;

public class PowerUpController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            CollectPowerUp();
        }
    }

    /// <summary>
    /// Increases the player's power-up count and removes the collected power-up from the game.
    /// </summary>
    private void CollectPowerUp()
    {
        LevelManager.Instance.CurrentPlayerPowerUps++;
        Destroy(gameObject);
    }
}

