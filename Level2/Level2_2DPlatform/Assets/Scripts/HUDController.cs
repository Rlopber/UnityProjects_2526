using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    // COMPONENTS
    private PlayerHealth playerHealth;

    [SerializeField] private TextMeshProUGUI healthText;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        healthText.text = playerHealth.Health.ToString("00");
    }

    private void Update()
    {
        // TODO Hacer más eficiente
        healthText.text = playerHealth.Health.ToString("00");
    }
}
