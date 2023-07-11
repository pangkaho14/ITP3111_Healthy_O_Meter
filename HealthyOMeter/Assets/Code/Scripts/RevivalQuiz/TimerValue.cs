using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "TimerValue", menuName = "Timer Value")]
public class TimerValue : ScriptableObject
{
    // 2 minutes 30 seconds
    public float maximumTimeToAnswerQuestionSeconds = 150f;
    public float timeLeftToAnswerQuestionSeconds = 150f;

    public bool isActive = false;

    public void ResetTimerValue()
    {
        timeLeftToAnswerQuestionSeconds = maximumTimeToAnswerQuestionSeconds;
    }
}
