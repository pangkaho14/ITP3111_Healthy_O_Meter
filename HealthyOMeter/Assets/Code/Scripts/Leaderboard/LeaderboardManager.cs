using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class LeaderboardManager : MonoBehaviour
{
    [System.Serializable]
    public class ScoreEntry
    {
        public string playerName;
        public float score;

        public ScoreEntry(string playerName, float score)
        {
            this.playerName = playerName;
            this.score = score;
        }
    }

    [System.Serializable]
    public class ScoreEntryList
    {
        public List<ScoreEntry> scoreEntries;
    }

    public GameObject InputFieldUI;
    public GameObject LeaderboardUI;

    [SerializeField] private LeaderboardUI leaderboardUI;
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private ScoreKeeper scoreKeeper;

    public void SaveScoreAndName()
    {
        string playerName = playerNameInput.text;
        float score = scoreKeeper.GetCurrentScore();

        SaveScore(playerName, score);
    }

    public void SaveScore(string playerName, float score)
    {
        // Create a list to store the score entries
        ScoreEntryList leaderboardData = new ScoreEntryList { scoreEntries = new List<ScoreEntry>() };
        UnityEngine.Debug.Log(leaderboardData);

        // Check if leaderboard data already exists in PlayerPrefs
        if (PlayerPrefs.HasKey("Leaderboard"))
        {
            // Retrieve the existing leaderboard data from PlayerPrefs
            string leaderboardJson = PlayerPrefs.GetString("Leaderboard");
            leaderboardData = JsonUtility.FromJson<ScoreEntryList>(leaderboardJson);
        }
        // Create a new score entry
        ScoreEntry newScoreEntry = new ScoreEntry(playerName, score);

        // Add the new score entry to the leaderboard data
        leaderboardData.scoreEntries.Add(newScoreEntry);

        // Convert the leaderboard data to JSON format
        string updatedLeaderboardJson = JsonUtility.ToJson(leaderboardData);

        // Save the updated leaderboard data to PlayerPrefs
        PlayerPrefs.SetString("Leaderboard", updatedLeaderboardJson);

        // Save the PlayerPrefs to disk
        PlayerPrefs.Save();

        //Set the input field inactive 
        InputFieldUI.SetActive(false);

        //Update the leaderboard UI
        leaderboardUI.UpdateLeaderboard();

        //Display the leaderboard from top 5
        LeaderboardUI.SetActive(true);
    }
}

