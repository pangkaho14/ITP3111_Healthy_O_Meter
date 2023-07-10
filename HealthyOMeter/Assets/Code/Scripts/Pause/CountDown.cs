using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class CountDown : MonoBehaviour
{
    public GameObject CountDownUI; // Reference to the countdown text UI element
    public TextMeshProUGUI CountDownText;
    [SerializeField] private int currentCountdown = 3; // Initial countdown value
    private bool isCountingDown = false; // Flag to indicate if countdown is in progress

    public void StartCountdown()
    {
        CountDownUI.SetActive(true); // Activate the countdown text UI element
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        isCountingDown = true;
        
        if (isCountingDown == true)
        {
            while (currentCountdown > 0)
            {
                CountDownText.text = currentCountdown.ToString(); // Update the countdown text
                yield return new WaitForSecondsRealtime(1f); // Wait for 1 second

                currentCountdown--; // Decrement the countdown value
            }
        }
        CountDownUI.SetActive(false); // Deactivate the countdown text UI element
        Time.timeScale = 1; // Game unpaused
        isCountingDown = false;
        currentCountdown = 3; // Reset the countdown value for future use
    }
}
