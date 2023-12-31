using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CountDown : MonoBehaviour
{
    public GameObject CountDownUI; // Reference to the countdown text UI element
    public TextMeshProUGUI CountDownText;
    [SerializeField] private int currentCountdown = 3; // Initial countdown 
    [SerializeField] private UnityEvent ResumeEvent;

    public void StartCountdown()
    {
        CountDownUI.SetActive(true); // Activate the countdown text UI element
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        int countDown = currentCountdown;
        while (countDown > 0)
        {
            CountDownText.text = countDown.ToString(); // Update the countdown text
            yield return new WaitForSecondsRealtime(1f); // Wait for 1 second
            countDown--; // Decrement the countdown value
        }
        CountDownUI.SetActive(false); // Deactivate the countdown text UI element
        // Reset currentCountdown
        // Perform unpausing logic for the Lanes scene
        Time.timeScale = 1;
        ResumeEvent.Invoke();
    }
}
