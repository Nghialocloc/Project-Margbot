using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //Make sure there can only be one manager
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("MainMenu");
        musicSource.volume = PlayerPrefs.GetFloat("music", 1);
        sfxSource.volume = PlayerPrefs.GetFloat("sfx", 1);
    }

    #region Sound Choose

    [SerializeField] public Sound[] musicSounds, sfxSounds;
    [Header("Sound Source")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;

    public void PlayMusic(string name)
    {
        //Search thought the list the sound name
        Sound chosseSound = Array.Find(musicSounds, x => x.soundName == name);

        //If find it, play the music.Else debug
        if(chosseSound == null)
        {
            Debug.Log("No Sound");
        }
        else
        {
            musicSource.clip = chosseSound.soundClip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        //Same as above
        Sound chosseSound = Array.Find(sfxSounds, x => x.soundName == name);

        if (chosseSound == null)
        {
            Debug.Log("No Sound");
        }
        else
        {
            sfxSource.clip = chosseSound.soundClip;
            sfxSource.Play();
        }
    }

    #endregion

    #region Sound Control

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    #endregion
}
