using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class MusicDoNotDestroy : MonoBehaviour
{
    [SerializeField] public AudioMixer SoundMixer;

    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("gamemusic");
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            float volume = PlayerPrefs.GetFloat("SoundVolume");
            SoundMixer.SetFloat("Sound", Mathf.Log10(volume)*20);
        }
        else
        {
            // float volume = PlayerPrefs.GetFloat("SoundVolume");
            // SoundMixer.SetFloat("Sound", Mathf.Log10(volume)*20);
            // PlayerPrefs.SetFloat("SoundVolume", volume);
        }
    }
}
