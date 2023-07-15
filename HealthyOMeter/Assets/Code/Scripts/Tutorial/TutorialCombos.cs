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
    private int LocaleKey = 0;

    private void Start()
    {
        StartCoroutine(HideTextCoroutine());
        // Start the countdown and pause the game
        StartCoroutine(CountdownCoroutine());
        // check localization from player prefs
        LocaleKey = PlayerPrefs.GetInt("LocaleKey");
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

        //Get highest combo count
        int comboCount = scoreKeeper.GetHighestCombo();

        if (LocaleKey == 0)
        {
            //Activate text
            textElement.text = "Wow, the highest combo you got is " + comboCount.ToString() + "!\n\n" +
                         "Try to beat that later!\n\n" +
                         "Remember it is possible to die now!";
        }
        else
        {
            //Activate text
            textElement.text = "哇，你得到的最高组合是 " + comboCount.ToString() + "!\n\n" +
                         "稍后尝试击败它!\n\n" +
                         "请记住，现在有可能死了!";
        }
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
        //Pop up instructions to avoid unhealthy food & do not miss healthy food
        PopUpMessage();
    }

    private void PopUpMessage()
    {
        if (LocaleKey == 0)
        {
            textElement.text = "Avoid unhealthy food!\n\n" + "Do not miss healthy food!";
        }
        else
        {
            textElement.text = "避免食物不健康!\n\n" + "不要错过健康食品!";
        }
        textElement.gameObject.SetActive(true);
        StartCoroutine(HideTextCoroutine());
    }
}
