using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("OceanScene"); // loads the game
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
