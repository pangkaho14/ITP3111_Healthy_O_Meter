using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Diagnostics;

public class TutorialCombos : MonoBehaviour
{
    //CountDown UI
    public GameObject CountDownUI; // Reference to the countdown text UI element
    public TextMeshProUGUI CountDownText;
    [SerializeField] private int currentCountdown = 30; // Initial countdown 
    private bool comboCounted = false; // Flag to track if combo has been counted

    // Tutorial UI
    public TextMeshProUGUI textElement;
    public float delayInSeconds = 5f;
    public GameObject ButtonCanvas;
    public TextMeshProUGUI combo;
    public GameObject Background;

    // ScoreKeeper script to get highest combo
    public ScoreKeeper scoreKeeper;

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

        // Get highest combo count
        int comboCount = scoreKeeper.GetHighestCombo();
        UnityEngine.Debug.Log(comboCount);
        // Activate text
        textElement.text = "Wow, the highest combo you got is " + comboCount.ToString() + "!\n\n" +
                         "Try to beat that later!\n\n" +
                         "Now, let's dive into the real game!";
        textElement.gameObject.SetActive(true);
        Background.SetActive(true);

        // Activate COMBOS button
        // Set the text of the child TextMeshProUGUI component
        TextMeshProUGUI textComponent = ButtonCanvas.GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = "HOME";
        ButtonCanvas.SetActive(true);

        // Perform pausing logic for the Lanes scene
        Time.timeScale = 0;
    }

    public void HandleHealthDepleted()
    {
        // Pop up instructions to avoid unhealthy food & do not miss healthy food
        PopUpMessage();
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(combo.text))
        {
                comboCounted = true;
        }
        // Check if combo.text is null
        if (string.IsNullOrEmpty(combo.text) && comboCounted)
        {
            ComboBroken();
            comboCounted = false;
        }
    }

    private void ComboBroken()
    {
        // Pop up instructions to avoid unhealthy food & do not miss healthy food
        PopUpMessage();
    }

    private void PopUpMessage()
    {
        textElement.text = "Avoid unhealthy food!\n\n" + "Do not miss healthy food!";
        textElement.gameObject.SetActive(true);
        StartCoroutine(HideTextCoroutine());
    }
}
