using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    private float currentScore;
    private int currentCombo;
    private int highestCombo = 0; // Variable to track the highest combo
    private float multiplier;
    private float currentMultiplier = 1f;

    [SerializeField] private TextMeshProUGUI scoreTextGame;
    [SerializeField] private TextMeshProUGUI scoreTextLeaderBoard;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI comboGameOverText;
    [SerializeField] private int ComboIncrements = 50;

    private int LocaleKey = 0;
    private void Start()
    {
        // check localization from player prefs
        LocaleKey = PlayerPrefs.GetInt("LocaleKey");
    }

    public float GetCurrentScore()
    {
        return currentScore;
    }

    public void AddScore(float scoreAmt, bool isHealthyFood)
    {
        // Get Combo multiplier when combo is a multiple of ComboIncrements
        if (currentCombo % ComboIncrements == 0 && currentCombo != 0)
        {
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

    // Called by collision detector to update combo count
    public void AddCombo()
    {
        currentCombo++;

        // Update highest combo if the current combo is higher
        if (currentCombo > highestCombo)
        {
            highestCombo = currentCombo;
        }

        UpdateComboText();
    }

    private float CalculateScore(float scoreAmt, float multiplier)
    {
        return scoreAmt * multiplier;
    }

    private float CalculateScoreMultiplier(int combo)
    {
        float quotient = (currentCombo / ComboIncrements) / 10f;
        multiplier = 1f + quotient;
        return multiplier;
    }

    public string GetNewScoreAmt(float scoreAmt)
    {
        float newScore = currentMultiplier * scoreAmt;
        return newScore.ToString();
    }

    private void UpdateScoreText()
    {
        if (scoreTextGame != null)
        {
            scoreTextGame.text = currentScore.ToString();
        }
        if (scoreTextLeaderBoard != null && LocaleKey == 0)
        {
          scoreTextLeaderBoard.text = "Score: " + currentScore.ToString() + "\nHighest Combo: " + highestCombo.ToString();
        }
        else if (scoreTextLeaderBoard != null && LocaleKey == 1)
        {
            scoreTextLeaderBoard.text = "分数: " + currentScore.ToString() + "\n最高组合: " + highestCombo.ToString();
        }
    }


    public void ResetScoreAndCombo()
    {
        currentCombo = 0;
        UpdateComboText();
    }

    private void UpdateComboText()
    {
        if (comboText != null && currentCombo > 0 && LocaleKey == 0)
        {
            comboText.text = "COMBO\n" + currentCombo.ToString();
        }
        else if(comboText != null && currentCombo > 0 && LocaleKey == 1)
        {
            comboText.text = "组合\n" + currentCombo.ToString();
        }
        else
        {
            comboText.text = string.Empty; // Clear the combo text
        }
    }

    public int GetHighestCombo()
    {
        return highestCombo;
    }
}
