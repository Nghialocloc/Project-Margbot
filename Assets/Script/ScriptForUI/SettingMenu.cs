using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    public TMPro.TMP_Dropdown imageQuality;

    public void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("music",10);
        sfxSlider.value = PlayerPrefs.GetFloat("sfx", 10);
        imageQuality.value = PlayerPrefs.GetInt("image", 0);
    }

    public void MusicVolume()
    {
        PlayerPrefs.SetFloat("music",musicSlider.value);
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    public void SfxVolume()
    {
        PlayerPrefs.SetFloat("sfx", sfxSlider.value);
        AudioManager.Instance.SfxVolume(sfxSlider.value);
    }

    public void SetGraphic(int qualityIndex)
    {
        PlayerPrefs.SetInt("image", qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
