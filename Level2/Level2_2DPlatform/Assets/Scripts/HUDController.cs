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
        SetPowerUpCount(0);
    }

    private void Update()
    {
        UpdateHealth();
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
    /// This method is called by the LevelManager when the count changes.
    /// </summary>
    public void SetPowerUpCount(int count)
    {
        if (powerUpsCountText != null)
            powerUpsCountText.text = count.ToString("00");
    }
}
