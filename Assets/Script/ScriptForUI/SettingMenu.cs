using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer music;
    [SerializeField] private AudioMixer effect;

    public void SetSoundEffect(float volume)
    {
        music.SetFloat("SoundFV", volume);
    }

    public void SetMusic(float volume)
    {
        effect.SetFloat("MusicV", volume);
    }

    public void SetGraphic(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
