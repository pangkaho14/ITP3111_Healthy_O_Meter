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
    private int LocaleKey = 0;

    private void Start()
    {
        StartCoroutine(HideTextCoroutine());
        // Start the countdown and pause the game
        StartCoroutine(CountdownCoroutine());
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

        LocaleKey = PlayerPrefs.GetInt("LocaleKey");
        if (LocaleKey == 0)
        {
            // Activate text
        textElement.text = "Wow, you scored " + score.text + " points!\n\n" +
                         "Try to beat that later!\n\n" +
                         "Now we will teach you about combos!";
        }
        else
        {
            // Activate text
        textElement.text = "哇，你得到的最高组合是 " + score.text + "!\n\n" +
                         "稍后尝试击败它!\n\n" +
                         "现在，我们将教您有关连击的信息!";
        }
        
        textElement.gameObject.SetActive(true);
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
        // Pop up instructions to avoid unhealthy food & do not miss healthy food

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
