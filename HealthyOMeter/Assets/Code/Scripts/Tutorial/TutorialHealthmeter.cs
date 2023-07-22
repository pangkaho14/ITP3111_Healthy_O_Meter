using System.Collections;
using UnityEngine;
using TMPro;
using System.Threading;

public class TutorialHealthmeter : MonoBehaviour
{
    // CountDown UI
    public GameObject CountDownUI; // Reference to the countdown text UI element
    public TextMeshProUGUI CountDownText;
    [SerializeField] private int currentCountdown = 30; // Initial countdown 

    // Tutorial UI
    public TextMeshProUGUI textElement1;
    public TextMeshProUGUI textElement2;
    public TextMeshProUGUI textElement3;
    public TextMeshProUGUI healthyContact;
    public TextMeshProUGUI healthyMissed;
    public TextMeshProUGUI unhealthyContact;
    public GameObject textBackground;
    public float delayInSeconds = 5f;
    public GameObject ButtonCanvas;
    public TextMeshProUGUI score;
    public GameObject Background;
    private int LocaleKey = 0;
    private bool isPaused = false;
    private bool isHealthyContacted = false;
    private bool isUnhealthyContacted = false;
    public GameObject continueButton;

    // Pausebutton script
    public PauseButton pauseButton;

    private void Start()
    {
        StartCoroutine(HideTextCoroutine());
        // Start the countdown and pause the game
        StartCoroutine(CountdownCoroutine());
        LocaleKey = PlayerPrefs.GetInt("LocaleKey");
    }

    private IEnumerator HideTextCoroutine()
    {
        yield return new WaitForSecondsRealtime(delayInSeconds);
        textElement1.gameObject.SetActive(false);
        textElement3.gameObject.SetActive(false);
        healthyContact.gameObject.SetActive(false);
        unhealthyContact.gameObject.SetActive(false);
        healthyMissed.gameObject.SetActive(false);
        textBackground.SetActive(false);
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

        LocaleKey = PlayerPrefs.GetInt("LocaleKey");
        
        if (LocaleKey == 0)
        {
            // Activate text
            textElement2.text = "Wow, you scored " + score.text + " points!\n\n" +
                         "Try to beat that later!\n\n" +
                         "Now we will teach you about combos!";
        }
        else
        {
            // Activate text
            textElement2.text = "哇，你得到的最高组合是 " + score.text + "!\n\n" +
                         "稍后尝试击败它!\n\n" +
                         "现在，我们将教您有关连击的信息!";
        }
        textElement3.gameObject.SetActive(false);
        textElement2.gameObject.SetActive(true);
        Background.SetActive(true);

        // Activate COMBOS button
        // Set the text of the child TextMeshProUGUI component
        TextMeshProUGUI textComponent = ButtonCanvas.GetComponentInChildren<TextMeshProUGUI>();
        if (LocaleKey == 0)
        {
            textComponent.text = "COMBOS";
        }
        else
        {
            textComponent.text = "组合";
        }
        ButtonCanvas.SetActive(true);

        // Perform pausing logic for the Lanes scene
        Time.timeScale = 0;
    }

    public void HandleHealthDepleted()
    {
        //Check LocaleKey once again to ensure language changes are made
        LocaleKey = PlayerPrefs.GetInt("LocaleKey");
        // Pop up instructions to avoid unhealthy food & do not miss healthy food

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

    public void HandleHealthyContactEvent()
    {
        // Set text to active and pause the game
        pauseButton.Pause();
        healthyContact.gameObject.SetActive(true);
        textBackground.SetActive(true);
        continueButton.SetActive(true);
    }

    public void HandleUnhealthyContactEvent()
    {
        // Set text to active and pause the game
        pauseButton.Pause();
        unhealthyContact.gameObject.SetActive(true);
        textBackground.SetActive(true);
        continueButton.SetActive(true);
    }

    public void HandleHealthyMissedEvent()
    {
        // Set text to active and pause the game
        pauseButton.Pause();
        healthyMissed.gameObject.SetActive(true);
        textBackground.SetActive(true);
        continueButton.SetActive(true);        
    }

    public void HideEventText()
    {
        textBackground.SetActive(false);
        unhealthyContact.gameObject.SetActive(false);
        healthyMissed.gameObject.SetActive(false);
        healthyContact.gameObject.SetActive(false);
        continueButton.SetActive(false);
    }

    // To unpause the game and hide all game objects related
    public void ContinueButton()
    {
        Time.timeScale = 1;
        HideEventText();
    }
}
