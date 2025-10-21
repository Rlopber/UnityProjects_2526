using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource backgroundMusicAudio;
    [SerializeField] private AudioSource powerUpAudio;
    [SerializeField] private AudioSource damageAudio;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Starts playing the background music.
    /// </summary>
    public void PlayBackgroundMusic()
    {
        if (!backgroundMusicAudio.isPlaying)
        {
            backgroundMusicAudio.Play();
        }
    }

    /// <summary>
    /// Stops playing the background music.
    /// </summary>
    public void StopBackgroundMusic()
    {
        if (backgroundMusicAudio.isPlaying)
        {
            backgroundMusicAudio.Stop();
        }
    }

    /// <summary>
    /// Starts playing the power-up sound effect.
    /// </summary>
    public void PlayPowerUpSound()
    {
        powerUpAudio.Play();
    }

    /// <summary>
    /// Starts playing the damage sound effect.
    /// </summary>
    public void PlayDamageSound()
    {
        damageAudio.Play();
    }
}
