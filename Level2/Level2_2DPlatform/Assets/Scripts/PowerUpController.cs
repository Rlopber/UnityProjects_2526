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

    private void CollectPowerUp()
    {
        Destroy(gameObject);
    }
}

