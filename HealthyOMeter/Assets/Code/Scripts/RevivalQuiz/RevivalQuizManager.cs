using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class RevivalQuizManager : MonoBehaviour
{
    [SerializeField] private int attempts = 1;
    [SerializeField] private UnityEvent gameOverEvent;

    [SerializeField] private RevivalQuiz revivalQuiz;
    [SerializeField] private GameObject selectionDialog;
    [SerializeField] private GameObject timer;
    
    [SerializeField] private TextMeshProUGUI selectionDialogText;
    [SerializeField] private RevivalQuizData revivalQuizData;
    
    private int LocaleKey;
    
    // Start is called before the first frame update
    void Start()
    {
        revivalQuiz.gameObject.SetActive(false);
        selectionDialog.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
    }
    
    public void IncrementAttempts()
    {
        attempts++;
    }

    public int GetAttempts()
    {
        return attempts;
    }
    
    public void CheckAndTriggerOptionDialog()
    {
        // Debug.Log("CheckAndTriggerOptionDialog()");
        attempts--;
        // Debug.Log($"Attempts after decrementing: {attempts}");
        
        // only when the user has used up all of its attempts and has died again, then the game is over
        if (attempts < 0)
        {
            triggerGameOverEvent();
        }
        else
        {
            // TODO: to replace with Ryan's function for pausing a game
            // just for simulating a pause
            Time.timeScale = 0f;
            
            SetupOptionDialog();
            timer.gameObject.SetActive(true);
        }
    }

    public void triggerGameOverEvent()
    {
        // Debug.Log("triggerGameOverEvent()");
        UITearDown();
        gameOverEvent.Invoke();
    }
    
    public void UITearDown()
    {
        // Debug.Log("UITearDown()");
        
        // TODO: replace with Ryan's function to resume the game
        Time.timeScale = 1;
        
        revivalQuiz.gameObject.SetActive(false);
        selectionDialog.gameObject.SetActive(false);
    }
    
    public void SetupOptionDialog()
    {
        // check localization from player prefs
        LocaleKey = PlayerPrefs.GetInt("LocaleKey");
        
        // 0 means it is english
        if (LocaleKey == 0)
        {
            selectionDialogText.text =
                $"You have {revivalQuizData.Attempts} attempt(s) left for the revival quiz. " +
                $"There will be 3 questions. " +
                $"For every correct answer, you heal {revivalQuizData.HealAmount} hp and can continue playing the game for a higher score!";
        }
        else
        {
            selectionDialogText.text = 
                $"您有{revivalQuizData.Attempts}尝试进行复兴测验。" +
                $"将有3个问题。" +
                $"对于每个正确的答案，您都可以治愈{revivalQuizData.HealAmount}hp，并且可以继续玩游戏以更高的分数！";
        }
        
        // Debug.Log("SetupOptionDialog()");
        selectionDialog.gameObject.SetActive(true);
    }

    public void SetupRevivalQuiz()
    {
        // Debug.Log("SetupRevivalQuiz()");
        
        selectionDialog.gameObject.SetActive(false);
        
        // this will trigger the Start() callback function... so be careful when setting attributes there
        revivalQuiz.gameObject.SetActive(true);
    }
}
