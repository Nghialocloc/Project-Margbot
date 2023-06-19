using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour
{
    [SerializeField] private Button[] levelButton;

    // Start is called before the first frame update
    void Start()
    {
        // Kiem soat level da choi toi
        int levelReached = PlayerPrefs.GetInt("levelReached",1);
        for (int i = 0; i < levelButton.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButton[i].interactable = false;
        }
    }

    // Nut bam de chuyen toi level do
    public void LevelSelect(string level)
    {
        SceneManager.LoadScene(level);
        AudioManager.Instance.PlayMusic("Theme");
    }
}
