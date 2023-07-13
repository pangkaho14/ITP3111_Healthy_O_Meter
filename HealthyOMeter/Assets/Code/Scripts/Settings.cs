using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] public Slider SliderVolume;
    [SerializeField] public AudioMixer SoundMixer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetSoundVolume();
        }
    }

    public void SetSoundVolume()
    {
        float volume = SliderVolume.value;
        SoundMixer.SetFloat("Sound", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    private void LoadVolume()
    {
        SliderVolume.value = PlayerPrefs.GetFloat("SoundVolume");
        SetSoundVolume();
    }
    // public GameObject ObjectMusic;
    // // //Value from the slider, and it converts to volume level
    // private float MusicVolume = 0f;
    // public AudioSource BackgroundMusic;

    // private void Start()
    // {
    //     //Get Object tag
    //     // BackgroundMusic.Play();
    //     ObjectMusic = GameObject.FindWithTag("gamemusic");
    //     BackgroundMusic = ObjectMusic.GetComponent<AudioSource>();
    //     //Set Volume
    //     MusicVolume = PlayerPrefs.GetFloat("volume");
    //     BackgroundMusic.volume = MusicVolume;
    //     SliderVolume.value = MusicVolume;
    // }

    // private void Update() 
    // {
    //     BackgroundMusic.volume = MusicVolume;
    //     PlayerPrefs.SetFloat("volume", MusicVolume);
    // }

    // public void VolumeUpdater(float volume)
    // {
    //     MusicVolume = volume;
    // }

    // public void MusicReset()
    // {
    //     PlayerPrefs.DeleteKey("volume");
    //     BackgroundMusic.volume = 1;
    //     SliderVolume.value = 1;
    // }
    
}