using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenManager : MonoBehaviour
{
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private HealthBar healthBar;

    //The gameover screen is set not active at the start, to bypass this we use an active object as the middleman

    private void Start()
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        // Subscribe the HealthBar's OnHealthDepleted event
        healthBar.OnHealthDepleted.AddListener(HandleHealthDepleted);
    }

    private void HandleHealthDepleted()
    {
        //Here can have a flag to see whether revival quiz have occured before?

        // if flag == true -> activate the gameover if not activate the quiz screen
        gameOverScreen.ActivateGameOverScreen();
        // Unsubscribe from the OnHealthDepleted event
        healthBar.OnHealthDepleted.RemoveListener(HandleHealthDepleted);
    }
}
