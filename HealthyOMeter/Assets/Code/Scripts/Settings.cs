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
    
}