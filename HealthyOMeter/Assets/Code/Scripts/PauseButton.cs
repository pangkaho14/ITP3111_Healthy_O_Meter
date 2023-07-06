using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void ActivatePauseScreen()
    {
        // Activate your game over screen here
        gameObject.SetActive(true);
        //Pause the game to stop movement and food falling
        Time.timeScale = 0;
    }

    public void DeactivatePauseScreen()
    {
        // Deactivate your game over screen here
        gameObject.SetActive(false);
        //Restart the game to stop movement and food falling
        Time.timeScale = 1;
    }
}
