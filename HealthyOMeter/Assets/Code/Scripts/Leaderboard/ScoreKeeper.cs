using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    private float currentScore;

    [SerializeField] private TextMeshProUGUI scoreTextGame;
    [SerializeField] private TextMeshProUGUI scoreTextLeaderBoard;

    public float GetCurrentScore()
    {
        return currentScore;
    }

    public void Add(float scoreAmt)
    {
        currentScore += scoreAmt;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreTextGame != null)
        {
            scoreTextGame.text = currentScore.ToString();
        }
        if (scoreTextLeaderBoard != null)
        {
            scoreTextLeaderBoard.text = currentScore.ToString();
        }
    }
}
