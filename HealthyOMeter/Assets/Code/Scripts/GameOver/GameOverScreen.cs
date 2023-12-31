using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics;

public class GameOverScreen : MonoBehaviour{

    public static GameOverScreen Instance { get; private set; }

    [SerializeField] private AudioSource GameOver;

    public void ActivateGameOverScreen()
    {
        // Activate your game over screen here
        gameObject.SetActive(true);
        //Pause the game to stop movement and food falling
        Time.timeScale = 0;
        GameOver.Play();
    }

    public void DeactivateGameOverScreen()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
    