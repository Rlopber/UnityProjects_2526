using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // METHODS
    public void SelectLevel()
    {
        // Load the next scene in the build index (Index Scene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OpenSettings()
    {
        // TODO implement open settings logic
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
