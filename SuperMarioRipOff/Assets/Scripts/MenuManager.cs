using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void ExitGame()
    {
        // quit the game here
        Application.Quit();
    }

    public void OpenSettingsGui()
    {
        // Here the setting GUI will be opened and the user can chance settings
        SceneManager.LoadScene("SettingsScene");
    }

    public void StartGame()
    {
        // Start the game here
        SceneManager.LoadScene("Level 1");
    }
}
