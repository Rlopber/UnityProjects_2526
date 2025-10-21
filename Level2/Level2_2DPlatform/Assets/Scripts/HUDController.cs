using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    // COMPONENTS
    private PlayerHealth playerHealth;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI powerUpsCountText;

    private void Awake()
    {

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        UpdateHealth();
        UpdatePowerUps();
    }

    private void Update()
    {
        UpdateHealth();
        UpdatePowerUps();
    }


    // METHODS

    /// <summary>
    /// Updates the HUD text and heart icon with the player's current health.
    /// </summary>
    private void UpdateHealth()
    {
        healthText.text = playerHealth.Health.ToString("00");
    }

    /// <summary>
    /// Updates the power-up counter on the HUD using LevelManager directly.
    /// </summary>
    private void UpdatePowerUps()
    {
        if (powerUpsCountText != null && LevelManager.Instance != null)
            powerUpsCountText.text = LevelManager.Instance.PowerUpsRemaining.ToString("00");
    }
}
