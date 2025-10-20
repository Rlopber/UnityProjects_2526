using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // PROPERTIES
    public int Health { get => health; set => health = value; }

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
            Health--;
            if (Health <= 0)
            {
                LevelManager.Instance.GameOver();
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
