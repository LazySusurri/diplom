using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Main");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitGame()
    {
        if(SceneManager.GetActiveScene().name == "Main")
            GameManager.instance.SaveState();
        Application.Quit();
    }

    public void MainMenu()
    {
        GameManager.instance.SaveState();
        SceneManager.LoadScene("MainMenu");
    }
}
