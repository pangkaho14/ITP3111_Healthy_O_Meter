using UnityEngine;
using UnityEngine.Events;

public class RevivalQuizManager : MonoBehaviour
{
    [SerializeField] private int attempts = 1;
    [SerializeField] private UnityEvent gameOverEvent;

    [SerializeField] private RevivalQuiz revivalQuiz;
    [SerializeField] private GameObject selectionDialog;
    [SerializeField] private GameObject timer;
    
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
            UITearDown();
            gameOverEvent.Invoke();
        }
        else
        {
            // TODO: to replace with Ryan's function for pausing a game
            // just for simulating a pause
            Time.timeScale = 0f;
            
            SetupOptionDialog();
        }
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
