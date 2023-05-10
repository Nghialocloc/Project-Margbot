using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static int currentScene = 1;

    public void PlayGame()
    {
        SceneManager.LoadScene("Select level");
    }

    public void SettingsChange()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Setting Menu");
    }

    public void ReturnFromSettings()
    {
        SceneManager.LoadScene(currentScene);  
    }

    public void Quitgame()
    {
        PlayerPrefs.SetInt("levelReached", 1);
        Application.Quit();
        Debug.Log("Out of the game");
    }
}
