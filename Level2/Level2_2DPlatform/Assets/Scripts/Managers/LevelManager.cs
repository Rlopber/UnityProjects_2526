using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // SINGLETON INSTANCE
    public static LevelManager Instance { get; private set; }

    // UI PANELS
    [Header("Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winLevelPanel;


    // VARIABLES

    private string MainMenuString = "MainMenu";

    private int powerUpsRemaining = 0;
    private string powerUpTag = "PowerUp";

    private int totalLevelPowerUps = 0;
    private int currentPlayerPowerUps = 0;


    // PROPERTIES

    /// <summary>
    /// Number of active power-ups in the scene.
    /// </summary>
    public int PowerUpsRemaining
    {
        get => powerUpsRemaining;
        set => powerUpsRemaining = value;
    }

    /// <summary>
    /// Player's total collected power-ups in the level.
    /// </summary>
    public int CurrentPlayerPowerUps
    {
        get => currentPlayerPowerUps;
        set => currentPlayerPowerUps = value;
    }

    private void Awake()
    {
        Instance = this;

        // Disable panels at start and set time scale to normal
        gameOverPanel.SetActive(false);
        winLevelPanel.SetActive(false);

        Time.timeScale = 1f;

    }

    private void Start()
    {
        totalLevelPowerUps = GameObject.FindGameObjectsWithTag(powerUpTag).Length;
    }

    void Update()
    {
        UpdatePowerUpCount();
        CheckWin();
    }


    /// <summary>
    /// Counts power-ups in the scene and notifies the HUD if the value changed.
    /// </summary>
    private void UpdatePowerUpCount()
    {
        powerUpsRemaining = totalLevelPowerUps - currentPlayerPowerUps;
    }

    /// <summary>
    /// Checks if the player has won the level, based on the number of remaining power-ups in the HUD.
    /// </summary>
    private void CheckWin()
    {
        if (currentPlayerPowerUps >= totalLevelPowerUps)
        {
            ShowPanelAndPause(winLevelPanel);
        }
    }

    /// <summary>
    /// Game Over logic: shows Game Over panel and pauses the game.
    /// </summary>
    public void GameOver()
    {
        ShowPanelAndPause(gameOverPanel);
    }

    /// <summary>
    /// Shows a panel and pauses the game.
    /// </summary>
    private void ShowPanelAndPause(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Changes the currently active scene to the specified scene.
    /// </summary>
    /// <param name="nameScene">The name of the scene to load. This must match the name of a scene included in the build settings.</param>
    public void MainMenuScene()
    {
        SceneManager.LoadScene(MainMenuString);
    }

    /// <summary>
    /// Restarts the current level by reloading the active scene.
    /// </summary>
    public void RestartLevel()
    {
        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    /// <summary>
    /// Sends the player to the next level.
    /// </summary>
    public void NextLevel()
    {
        int levelIndex = SceneManager.sceneCountInBuildSettings;

        if (SceneManager.GetActiveScene().buildIndex < levelIndex - 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
