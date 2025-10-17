using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health Settings")]
    [Range(0f, 10f)]
    [SerializeField] private int health = 5;

    [Header("Blink Damage")]
    [SerializeField] private float blinkDuration = 0.2f;
    [SerializeField] private Color blinkColor = Color.red;

    // STATES
    private bool canTakeDamage = true;

    // COMPONENTS
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            health--;
            if (health <= 0)
            {
                // Handle player death (e.g., reload scene, show game over screen, etc.)
                Debug.Log("Player Dead");
            }
            else
            {
                StartCoroutine(BlinkSpriteEffect(4));
            }
        }
    }


    private IEnumerator BlinkSpriteEffect(int blinkTimes)
    {
        canTakeDamage = false;
        do
        {
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkDuration);
            blinkTimes--;
        } while (blinkTimes > 0);

        canTakeDamage = true;
    }
}
