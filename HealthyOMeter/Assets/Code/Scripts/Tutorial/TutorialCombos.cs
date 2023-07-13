using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class TutorialCombos : MonoBehaviour
{
    //CountDown UI
    public GameObject CountDownUI; // Reference to the countdown text UI element
    public TextMeshProUGUI CountDownText;
    [SerializeField] private int currentCountdown = 30; // Initial countdown 

    //Tutorial UI
    public TextMeshProUGUI textElement;
    public float delayInSeconds = 5f;
    public GameObject ButtonCanvas;
    public TextMeshProUGUI combo;
    public GameObject Background;

    //Healthbar event subscribe
    public HealthBar healthBar;

    private void Start()
    {
        // Subscribe the HealthBar's OnHealthDepleted event
        healthBar.OnHealthDepleted.AddListener(HandleHealthDepleted);

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

        // Extract numerals from the combo string
        string comboString = combo.text;
        string comboCount = "";

        for (int i = 0; i < comboString.Length; i++)
        {
            if (char.IsDigit(comboString[i]))
            {
                comboCount += comboString[i];
            }
        }
        //Activate text
        textElement.text = "Wow, the highest combo you got is " + comboCount + "!\n\n" +
                         "Try to beat that later!\n\n" +
                         "Remember it is possible to die now!";
        textElement.gameObject.SetActive(true);
        Background.SetActive(true);

        //Activate COMBOS button
        //Set the text of the child TextMeshProUGUI component
        TextMeshProUGUI textComponent = ButtonCanvas.GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = "HOME";
        ButtonCanvas.SetActive(true);

        // Perform pausing logic for the Lanes scene
        Time.timeScale = 0;
    }

    public void HandleHealthDepleted()
    {
        //Pop up instructions to avoid unhealthy food & do not miss healthy food

        textElement.text = "Avoid unhealthy food!\n\n" + "Do not miss healthy food!";
        textElement.gameObject.SetActive(true);
        StartCoroutine(HideTextCoroutine());

        // Unsubscribe from the OnHealthDepleted event
        healthBar.OnHealthDepleted.RemoveListener(HandleHealthDepleted);
    }
}
