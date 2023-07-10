using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class PauseButton : MonoBehaviour
{
    public CountDown countDown;

    public void ActivatePauseScreen()
    {
        // Activate your game over screen here
        gameObject.SetActive(true);

        // Pause the game to stop movement and food falling
        Pause();
    }

    public void DeactivatePauseScreen()
    {
        // Deactivate your game over screen here
        gameObject.SetActive(false);

        // Unpause the game
        Unpause();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        // Start the countdown an unpause the game
        countDown.StartCountdown();
    }
}
