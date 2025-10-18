using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    // COMPONENTS
    private PlayerHealth playerHealth;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI powerUpsCountText;

    // PROPERTIES

    /// <summary>
    /// Returns the number of active power-ups in the scene.
    /// </summary>
    public int PowerUpsRemaining => GameObject.FindGameObjectsWithTag("PowerUp").Length;

    private void Awake()
    {

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        UpdateHealth();
        PowerUpCount();
    }

    private void Update()
    {
        UpdateHealth();
        PowerUpCount();
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
    /// Updates the power-up counter on the HUD.
    /// </summary>
    private void PowerUpCount()
    {
        powerUpsCountText.text = PowerUpsRemaining.ToString("00");
    }
}
