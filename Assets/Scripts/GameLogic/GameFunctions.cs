using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFunctions : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1.0f;
    }

    public void PlaySavedGame()
    {

    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public void PausedGame()
    {
        Time.timeScale = 0.0f;
    }

    public void ExitGame()
    {
        Debug.Log("Bye");
        Application.Quit();
    }

}
