using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// nice to have: randomize answer choices button, they are now always fixed

// note to future self:
// making everything events because I don't want hard dependencies between the scripts
// not sure if best practice but we shall see
public class RevivalQuiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] private List<Question> questions = new();
    private Question currentQuestion;
    [SerializeField] private TextMeshProUGUI currentQuestionText;

    // -1 means no answer has been chosen yet
    [SerializeField] private int playerAnswerChoiceIndex = -1;
    [SerializeField] private int currentQuestionCount = 0;
    
    // not making it a constant because I want to be able to change it in the inspector
    [SerializeField] private int maximumQuestions = 3;

    [Header("Answer Buttons")]
    [SerializeField] private Toggle[] answerToggleButtons = new Toggle[4];
    private TextMeshProUGUI[] answerToggleButtonTexts = new TextMeshProUGUI[4];
    
    [Header("Footer Button Group")]
    [SerializeField] private Button submitAnswerButton;
    [SerializeField] private Button nextQuestionButton;
    [SerializeField] private Button resumeGameButton;
    
    [Header("Timer")]
    [SerializeField] private Image timerImage;
    [SerializeField] private TimerValue timerValue;
    [SerializeField] private float fillPercent;
    
    [Header("Quiz State")]
    [SerializeField] private int numberOfCorrectAnswers = 0;
    [SerializeField] private bool loadNextQuestion = true;
    [SerializeField] private bool isAnsweringQuestion = true;
    
    [SerializeField] private PlayerHealthPoints playerHealthPoints;
    [SerializeField] private UnityEvent quizOverEvent;

    private string quizEndText;
    
    private int LocaleKey = 0;

    private void Start()
    {
        // check localization from player prefs
        LocaleKey = PlayerPrefs.GetInt("LocaleKey");
        // Debug.Log("RevivalQuiz.Start()");
        
        // Set up answer toggle button text
        for (var i = 0; i < answerToggleButtons.Length; i++)
        {
            answerToggleButtonTexts[i] = answerToggleButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            answerToggleButtons[i].gameObject.SetActive(false);
        }
        
        // Set up footer button group
        submitAnswerButton.gameObject.SetActive(true);
        nextQuestionButton.gameObject.SetActive(false);
        resumeGameButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (currentQuestionCount == maximumQuestions)
        {
            // Debug.Log("No more questions to display!");
            LocaleKey = PlayerPrefs.GetInt("LocaleKey");
            if (LocaleKey == 1)
            {
                DisplayPlayerScoreCN();
            }
            else
            {
                DisplayPlayerScore();
            }
            
            SetAllAnswerChoiceButtonsIsActiveInHierarchy(false);
            submitAnswerButton.gameObject.SetActive(false);
            resumeGameButton.gameObject.SetActive(true);
            ResetQuizState();
            return;
        }
        
        // Update timer: Decrement timer of 2.5 minutes
        UpdateTimerImage();
        
        if (loadNextQuestion)
        {
            // reset player answer choice
            playerAnswerChoiceIndex = -1;
            
            currentQuestion = GetRandomQuestion();
            DisplayRandomQuestion(currentQuestion);
            
            // reset timer
            timerValue.timeLeftToAnswerQuestionSeconds = timerValue.maximumTimeToAnswerQuestionSeconds;
            
            timerValue.isActive = true;
            loadNextQuestion = false;
        }
    }

    public void ResumeGame()
    {
        // manager to respond to quiz completed event and teardown UI
        quizOverEvent.Invoke();

        // if the player did not heal at all, this means the player is still dead
        if (playerHealthPoints.GetCurrentHealth() <= playerHealthPoints.GetMinHealth())
        {
            // making the player take damage to trigger the DeathEvent
            // the DeathEvent is only triggered when the player takes damage and dies (triggered by PlayerHealthPoints)
            // 2 DeathEvents will trigger the game over screen (this is managed by the RevivalQuizManager)
            playerHealthPoints.TakeDamage(10);
        }
    }

    private void ResetQuizState()
    {
        playerAnswerChoiceIndex = -1;
        currentQuestionCount = 0;
        numberOfCorrectAnswers = 0;
        timerValue.ResetTimerValue();
        timerValue.isActive = false;
        loadNextQuestion = false;
    }

    // English Method
    private void DisplayPlayerScore()
    {
        string quizEndText = "You have completed the quiz!";
        if (numberOfCorrectAnswers == maximumQuestions)
        {
            quizEndText += "\nYou have answered all questions correctly! Congratulations!";
        }
        else
        {
            quizEndText += $"\nYou have {numberOfCorrectAnswers} out of {maximumQuestions} questions correctly! Better luck next time!";
        }
        currentQuestionText.text = quizEndText;
    }

    // Chinese Method
    private void DisplayPlayerScoreCN()
    {
        string quizEndText = "您已经完成了测验!";
        if (numberOfCorrectAnswers == maximumQuestions)
        {
            quizEndText += "\n您已经正确回答了所有问题!恭喜!";
        }
        else
        {
            quizEndText += $"\n您有{maximumQuestions}个问题中有{numberOfCorrectAnswers}个问题！下次好运!";
        }    
        currentQuestionText.text = quizEndText;
    }

    private Question GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        Question question = questions[index];
        questions.RemoveAt(index);
        return question;
    }

    private void DisplayRandomQuestion(Question question)
    {
        if (questions.Count == 0)
        {
            // Debug.Log("No questions to display!");
            return;
        }
        
        LocaleKey = PlayerPrefs.GetInt("LocaleKey");
        // 0 means it is english, else it is chinese
        question.SetEnglishLocalization(LocaleKey == 0);
        
        currentQuestionText.text = question.GetQuestionText();
        for (var i = 0; i < question.GetAnswerCount(); i++)
        {
            answerToggleButtons[i].gameObject.SetActive(true);
            answerToggleButtonTexts[i].text = question.GetAnswerChoiceText(i);
        }

        ResetAllAnswerChoiceButtonsColour();
        
        submitAnswerButton.interactable = false;
    }

    private void SwapSubmitButtonAndNextButton()
    {
        submitAnswerButton.gameObject.SetActive(!submitAnswerButton.gameObject.activeInHierarchy);
        nextQuestionButton.gameObject.SetActive(!nextQuestionButton.gameObject.activeInHierarchy);
    }
    
    // event handler for Answer Selected
    public void OnAnswerSelected(int index)
    {
        playerAnswerChoiceIndex = index;
        // Debug.Log($"OnAnswerSelected() index: {index}");
        
        submitAnswerButton.interactable = true;
        
        // this is so that when the OnDeselect event is triggered, the button is not set to white
        // in addition, the way the ToggleButton is built is by layering the image on top of a background
        // and it is the image that I am changing the colour of
        answerToggleButtons[index].image.color = new Color32(255, 237, 104, 255);
    }
    
    // event handler for Submit Answer
    public void OnSubmitAnswer()
    {
        CheckAndDisplayAnswer(playerAnswerChoiceIndex);
        SetAllAnswerChoiceButtonsIsInteractable(false);
        timerValue.isActive = false;
        isAnsweringQuestion = false;
        SwapSubmitButtonAndNextButton();
    }
    
    // event handler for Next Question
    public void OnNextQuestion()
    {
        SwapSubmitButtonAndNextButton();
        SetAllAnswerChoiceButtonsIsInteractable(true);
        SetAllAnswerChoiceButtonsIsActiveInHierarchy(false);
        ResetAllAnswerChoiceButtonsColour();
        isAnsweringQuestion = true;
        timerValue.ResetTimerValue();
        timerValue.isActive = true;
        
        // this flag will trigger the Update() to load the next question
        loadNextQuestion = true;
        
        currentQuestionCount++;
    }

    private void CheckAndDisplayAnswer(int selectionIndex)
    {
        int correctAnswerIndex = currentQuestion.GetAnswerIndex();
        bool isCorrect = selectionIndex == correctAnswerIndex;
        if (isCorrect)
        {
            numberOfCorrectAnswers++;
            playerHealthPoints.Heal(10);
        }
        
        for (var i = 0; i < answerToggleButtons.Length; i++)
        {
            if (i == correctAnswerIndex) continue;
            answerToggleButtons[i].image.color = Color.red;
        }
        answerToggleButtons[correctAnswerIndex].image.color = Color.green;
    }
    
    private void SetAllAnswerChoiceButtonsIsInteractable(bool state)
    {
        foreach (var toggleButton in answerToggleButtons)
        {
            toggleButton.interactable = state;
        }
    }

    private void ResetAllAnswerChoiceButtonsColour()
    {
        foreach (var toggleButton in answerToggleButtons)
        {
            toggleButton.image.color = Color.white;
        }
    }

    private void SetAllAnswerChoiceButtonsIsActiveInHierarchy(bool state)
    {
        foreach (var toggleButton in answerToggleButtons)
        {
            toggleButton.gameObject.SetActive(state);
        }
    }

    private void UpdateTimerImage()
    {
        if (!isAnsweringQuestion) return;
        
        fillPercent = timerValue.timeLeftToAnswerQuestionSeconds / timerValue.maximumTimeToAnswerQuestionSeconds;
        timerImage.fillAmount = fillPercent;
    }
}
