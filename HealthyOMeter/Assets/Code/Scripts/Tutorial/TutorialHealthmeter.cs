using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class TutorialHealthmeter : MonoBehaviour
{
    //CountDown UI
    public GameObject CountDownUI; // Reference to the countdown text UI element
    public TextMeshProUGUI CountDownText;
    [SerializeField] private int currentCountdown = 30; // Initial countdown 

    //Tutorial UI
    public TextMeshProUGUI textElement;
    public float delayInSeconds = 5f;
    public GameObject ButtonCanvas;
    public TextMeshProUGUI score;
    public GameObject Background;

    //Healthbar event subscribe
    public HealthBar healthBar;

    private void Start()
    {
        StartCoroutine(HideTextCoroutine());
        // Start the countdown and pause the game
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator HideTextCoroutine()
    {
        yield return new WaitForSeconds(delayInSeconds);
        textElement.gameObject.SetActive(false);
    }

    private IEnumerator CountdownCoroutine()
    {
        CountDownUI.SetActive(true);
        while (currentCountdown > 0)
        {
            CountDownText.text = currentCountdown.ToString(); // Update the countdown text
            yield return new WaitForSecondsRealtime(1f); // Wait for 1 second
            currentCountdown--; // Decrement the countdown value
        }
        CountDownUI.SetActive(false); // Deactivate the countdown text UI element

        // Activate text
        textElement.text = "Wow, you scored " + score.text + " points!\n\n" +
                         "Try to beat that later!\n\n" +
                         "Now we will teach you about combos!";
        textElement.gameObject.SetActive(true);
        Background.SetActive(true);

        // Activate COMBOS button
        // Set the text of the child TextMeshProUGUI component
        TextMeshProUGUI textComponent = ButtonCanvas.GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = "COMBOS";
        ButtonCanvas.SetActive(true);

        // Perform pausing logic for the Lanes scene
        Time.timeScale = 0;
    }

    public void HandleHealthDepleted()
    {
        // Pop up instructions to avoid unhealthy food & do not miss healthy food

        textElement.text = "Avoid unhealthy food!\n\n" + "Do not miss healthy food!";
        textElement.gameObject.SetActive(true);
        StartCoroutine(HideTextCoroutine());
    }
}
