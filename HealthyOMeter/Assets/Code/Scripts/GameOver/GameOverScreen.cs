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

    public void ActivateGameOverScreen()
    {
        // Activate your game over screen here
        UnityEngine.Debug.Log("GameOverScreen's ActivateGameOverScreen method called.");
        gameObject.SetActive(true);
        //Pause the game to stop movement and food falling
        Time.timeScale = 0;
    }

    public void DeactivateGameOverScreen()
    {
        UnityEngine.Debug.Log("GameOverScreen's DeactivateGameOverScreen method called.");
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
    