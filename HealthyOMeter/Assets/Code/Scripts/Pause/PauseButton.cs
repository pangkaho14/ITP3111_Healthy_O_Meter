using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.Events;

public class PauseButton : MonoBehaviour
{
    public CountDown countDown;
    [SerializeField] private UnityEvent PauseEvent;

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
        PauseEvent.Invoke();
    }

    public void Unpause()
    {
        // Start the countdown an unpause the game
        countDown.StartCountdown();
    }

    public IEnumerator UnpauseWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        // Unpause the game
        Unpause();
    }
}
