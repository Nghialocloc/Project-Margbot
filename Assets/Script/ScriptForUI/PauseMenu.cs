using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScene;
    [SerializeField] public static bool isPause = false;

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ControlMenu();
        }
        // Make sure as long isPause true, Pausemenu still active
        if (isPause)
        {
            pauseScene.SetActive(true);
        }
    }

    // Press esc to control pause menu
    public void ControlMenu()
    {
        if (!isPause)
        {
            Time.timeScale = 0;
            pauseScene.SetActive(true);
            isPause = true;
        }
        else if (isPause)
        {
            Time.timeScale = 1;
            pauseScene.SetActive(false);
            isPause = false;
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        isPause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Go to next level
    public void SkipLevel()
    {
        Time.timeScale = 1;
        isPause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.Instance.PlayMusic("Theme");
    }

    public void ToTheTilte()
    {
        Time.timeScale = 1;
        isPause = false;
        SceneManager.LoadScene("Title Scene");
        AudioManager.Instance.PlayMusic("MainMenu");
    }
}
