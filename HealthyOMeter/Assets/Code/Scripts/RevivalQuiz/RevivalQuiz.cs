using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// note to future self:
// making everything events because I don't want hard dependencies between the scripts
// not sure if best practice but we shall see

// TODO:
// 1. to handle multi select questions
// 2. to establish game play loop
// 3. respond to the QuestionTimeUp event to trigger and submit the last chosen answer ✅
public class RevivalQuiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] private List<Question> questions = new();
    private Question currentQuestion;
    [SerializeField] private TextMeshProUGUI currentQuestionText;
    
    // -1 means no answer has been chosen yet
    [SerializeField] private int playerAnswerChoiceIndex = -1;
    [SerializeField] private int currentQuestionCount = 0;
    [SerializeField] private int maximumQuestions = 3;

    [Header("Answer Buttons")]
    [SerializeField] private Toggle[] answerToggleButtons = new Toggle[4];
    private TextMeshProUGUI[] answerToggleButtonTexts = new TextMeshProUGUI[4];
    
    [Header("Footer Button Group")]
    [SerializeField] private Button submitAnswerButton;
    [SerializeField] private Button nextQuestionButton;
    
    [Header("Timer")]
    [SerializeField] private Image timerImage;
    [SerializeField] private TimerValue timerValue;
    [SerializeField] private float fillPercent;
    
    [Header("Quiz State")]
    [SerializeField] private bool loadNextQuestion = true;
    [SerializeField] private bool isAnsweringQuestion = true;

    [SerializeField] private PlayerHealthPoints playerHealthPoints;
    
    private void Start()
    {
        Debug.Log("RevivalQuiz.Start()");
        
        // Set up answer toggle button text
        for (var i = 0; i < answerToggleButtons.Length; i++)
        {
            answerToggleButtonTexts[i] = answerToggleButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        }
        
        // Set up footer button group
        submitAnswerButton.gameObject.SetActive(true);
        nextQuestionButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (currentQuestionCount == maximumQuestions)
        {
            // Debug.Log("No more questions to display!");
            currentQuestionCount = 0;
            
            // TODO:
            // 1. calculate the number of correct answers
            // 2. display text and encourage the player
            // 3. change the question text to the score and encouragement
            // DisplayPlayerScore();
            
            // 1. change submit button text to "PRESS TO RESUME GAME"
            // 2. resume the game
            
            // broadcast quiz completed event (?)
            // manager to respond to quiz completed event and teardown UI
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
            currentQuestionCount++;
            loadNextQuestion = false;
        }
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
        
        currentQuestionText.text = question.GetQuestionText();
        for (var i = 0; i < question.GetAnswerCount(); i++)
        {
            answerToggleButtons[i].gameObject.SetActive(true);
            answerToggleButtonTexts[i].text = question.GetAnswerChoiceText(i);
        }
        
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
    }

    private void CheckAndDisplayAnswer(int selectionIndex)
    {
        int correctAnswerIndex = currentQuestion.GetAnswerIndex();
        bool isCorrect = selectionIndex == correctAnswerIndex;
        if (isCorrect)
        {
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
