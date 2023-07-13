using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    private float currentScore;
    private int currentCombo;
    private float multiplier;
    private float currentMultiplier = 1f;

    [SerializeField] private TextMeshProUGUI scoreTextGame;
    [SerializeField] private TextMeshProUGUI scoreTextLeaderBoard;

    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private int ComboIncrements = 50;

    public float GetCurrentScore()
    {
        return currentScore;
    }

    public void AddScore(float scoreAmt, bool isHealthyFood)
    {
        //Get Combo multiplier when combo is a multiple of ComboIncrements
        if (currentCombo % ComboIncrements == 0 && currentCombo != 0)
        {
            UnityEngine.Debug.Log("In cals");
            currentMultiplier = CalculateScoreMultiplier(currentCombo);
        }
        if (isHealthyFood)
        {
            currentScore += CalculateScore(scoreAmt, currentMultiplier);
        }
        else
        {
            currentScore += scoreAmt;
        }
        
        UpdateScoreText();
    }
    
    //Called by collision detector to update combo count
    public void AddCombo()
    {
        currentCombo++;

        UpdateComboText();
    }

    private float CalculateScore(float scoreAmt, float multiplier) 
    {
        UnityEngine.Debug.Log("mutlipliedScore:" + scoreAmt * currentMultiplier);
        return (scoreAmt * multiplier);
    }
    private float CalculateScoreMultiplier(int combo)
    {
        //When combo hits 50, find the quotient e.g 50 = 1 so multiplier is 1.1
        float quotient = (currentCombo / ComboIncrements) / 10f;
        multiplier = 1f + quotient;
        UnityEngine.Debug.Log("Quotient:" + quotient);
        UnityEngine.Debug.Log("Multiplier:" + multiplier);
        return multiplier;

    }
    public string GetNewScoreAmt(float scoreAmt)
    {
        //Only for healthy objects and Nutrigrade A to update the floating text
        float newScore = currentMultiplier * scoreAmt;
        return newScore.ToString();
    }

    private void UpdateScoreText()
    {
        if (scoreTextGame != null)
        {
            scoreTextGame.text = currentScore.ToString();
        }
        if (scoreTextLeaderBoard != null)
        {
            scoreTextLeaderBoard.text = "Score: " + currentScore.ToString();
        }
    }

    private void UpdateComboText()
    {
        if (comboText != null)
        {
            comboText.text = "COMBO\n\n" + currentCombo.ToString();
        }
    }
}
