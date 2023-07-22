using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialCombos : MonoBehaviour
{
    // CountDown UI
    public GameObject CountDownUI; // Reference to the countdown text UI element
    public TextMeshProUGUI CountDownText;
    [SerializeField] private int currentCountdown = 30; // Initial countdown 
    private bool comboCounted = false; // Flag to track if combo has been counted

    // Tutorial UI
    public TextMeshProUGUI textElement1;
    public TextMeshProUGUI textElement2;
    public TextMeshProUGUI textElement3;
    public float delayInSeconds = 5f;
    public GameObject ButtonCanvas;
    public TextMeshProUGUI combo;
    public GameObject Background;
    private bool isPaused = false;
    
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
        textElement1.gameObject.SetActive(false);
        textElement3.gameObject.SetActive(false);
    }

    private IEnumerator CountdownCoroutine()
    {
        CountDownUI.SetActive(true);
        while (currentCountdown > 0)
        {
            if (!isPaused)
            {
                CountDownUI.SetActive(true);
                CountDownText.text = currentCountdown.ToString(); // Update the countdown text
                yield return new WaitForSeconds(1f); // Wait for 1 second
                currentCountdown--; // Decrement the countdown value
            }
            else
            {
                CountDownUI.SetActive(false);
                yield return null; // Wait for the next frame if the game is paused
            }
        }
        CountDownUI.SetActive(false); // Deactivate the countdown text UI element

        // Get highest combo count
        int comboCount = scoreKeeper.GetHighestCombo();
        
        // Activate text
        // Check if language selected is english
        if (LocaleKey == 0)
        {
            //Activate text
            textElement2.text = "Wow, the highest combo you got is " + comboCount.ToString() + "!\n\n" +
                         "Try to beat that later!\n\n" +
                         "A surprise can be found when u first lose all health points!\n\n"+
                         "Now, let's dive into the real game!";
        }
        // Check if language selected is chinese
        else
        {
            // Activate text
            textElement2.text = "哇，你得到的最高组合是 " + comboCount.ToString() + "!\n\n" +
                         "稍后尝试击败它!\n\n" +
                         "当你第一次失去所有生命值时，可以找到一个惊喜！\n\n"+
                         "现在，让我们深入了解真正的游戏！";
        }
        textElement3.gameObject.SetActive(false);
        textElement2.gameObject.SetActive(true);
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
        // Check LocaleKey once again to apply language change
        LocaleKey = PlayerPrefs.GetInt("LocaleKey");
        if (LocaleKey == 0)
        {
            textElement3.text = "Avoid unhealthy food!\n\n" + "Do not miss healthy food!";
        }
        else
        {
            textElement3.text = "避免食物不健康!\n\n" + "不要错过健康食品!";
        }
        textElement3.gameObject.SetActive(true);
        StartCoroutine(HideTextCoroutine());
    }

    public void HandlePauseEvent()
    {
        // stop the countdown
        isPaused = true;
    }

    public void HandleResumeEvent()
    {
        // continue the countdown
        isPaused = false;
    }
}
