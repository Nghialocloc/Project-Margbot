using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public void Music1()
    {
        AudioManager.Instance.PlayMusic("MainMenu");
    }

    public void Music2()
    {
        AudioManager.Instance.PlayMusic("Theme");
    }

    public void Music3()
    {
        AudioManager.Instance.PlayMusic("Secret");
    }
}
