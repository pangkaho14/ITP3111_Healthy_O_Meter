using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.UIElements;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] public Slider SliderVolume;
    public GameObject ObjectMusic;
    //Value from the slider, and it converts to volume level
    private float MusicVolume = 0f;
    public AudioSource BackgroundMusic;

    private void Start()
    {
        // //Get Object tag
        // BackgroundMusic.play();
        ObjectMusic = GameObject.FindWithTag("gamemusic");
        BackgroundMusic = ObjectMusic.GetComponent<AudioSource>();
        //Set Volume
        MusicVolume = PlayerPrefs.GetFloat("volume");
        BackgroundMusic.volume = MusicVolume;
        SliderVolume.value = MusicVolume;
    }

    private void Update() 
    {
        BackgroundMusic.volume = MusicVolume;
        PlayerPrefs.SetFloat("volume", MusicVolume);
    }

    public void VolumeUpdater(float volume)
    {
        MusicVolume = volume;
    }

    public void MusicReset()
    {
        PlayerPrefs.DeleteKey("volume");
        BackgroundMusic.volume = 1;
        SliderVolume.value = 1;
    }
    // private void OnEnable()
    // {
    //     VisualElement root = GetComponent<UIDocument>().rootVisualElement;

    //     //Get the Exit Button
    //     Button buttonExitSettings = root.Q<Button>("btnExit");
    //     buttonExitSettings.clicked += LoadNextScene;
    // }

    // private void LoadNextScene()
    // {
    //     // Replace "NextSceneName" with the name or index of your desired next scene
    //     SceneManager.LoadScene("Game");
    // }
}