using UnityEngine;
using UnityEngine.UI;

public class HeartIconController : MonoBehaviour
{
    // COMPONENTS
    private Image heartImage;
    private PlayerHealth playerHealth;

    // SPRITES
    [Header("Heart Sprites")]
    [Tooltip("Sprite when the player has full health.")]
    [SerializeField] private Sprite fullHeart;

    [Tooltip("Sprite when the player has 2-4 health.")]
    [SerializeField] private Sprite halfHeart;

    [Tooltip("Sprite when the player has 1 health.")]
    [SerializeField] private Sprite lowHeart;

    [Tooltip("Sprite when the player has 0 health.")]
    [SerializeField] private Sprite emptyHeart;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        UpdateHeart();
    }

    private void UpdateHeart()
    {
        int health = playerHealth.Health;

        if (health >= 5)
            heartImage.sprite = fullHeart;
        else if (health >= 2)
            heartImage.sprite = halfHeart;
        else if (health == 1)
            heartImage.sprite = lowHeart;
        else
            heartImage.sprite = emptyHeart;
    }
}
