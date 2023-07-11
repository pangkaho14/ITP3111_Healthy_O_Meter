using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    // Using observer pattern and have the timer trigger an event
    // rather than to for the RevivalQuiz to poll the timer
    [SerializeField] private UnityEvent questionTimeUpEvent;
    [SerializeField] private TimerValue timerValue;

    // Update is called once per frame
    void Update()
    {
        if (!timerValue.isActive) return;
        
        timerValue.timeLeftToAnswerQuestionSeconds -= Time.unscaledDeltaTime;
        if (timerValue.timeLeftToAnswerQuestionSeconds <= 0f)
        {
            questionTimeUpEvent.Invoke();
            Debug.Log("Time's up!");
        }
    }
}
