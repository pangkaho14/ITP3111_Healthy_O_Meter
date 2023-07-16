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
    public FunFactsManager FunFactsUI;
    public GameObject InputError;

    [SerializeField] private LeaderboardUI leaderboardUI;
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private ScoreKeeper scoreKeeper;


    public void SaveScoreAndName()
    {
        string playerName = playerNameInput.text;
        float score = scoreKeeper.GetCurrentScore();

         if (string.IsNullOrEmpty(playerName) || playerName == "Enter Player Name")
        {
            InputError.SetActive(true);
        }
        else
        {
            SaveScore(playerName, score);
        }
        
    }

    public void SaveScore(string playerName, float score)
    {
        // Create a list to store the score entries
        ScoreEntryList leaderboardData = new ScoreEntryList { scoreEntries = new List<ScoreEntry>() };

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

        //Display Leaderboard and FunFacts after saving
        FunFactsUI.DisplayFunFact();
        DisplayTop5LeaderBoard();
    }

    public void DisplayTop5LeaderBoard()
    {
        //Set the input field inactive 
        InputFieldUI.SetActive(false);

        //Update the leaderboard UI
        leaderboardUI.UpdateLeaderboard();

        //Display the leaderboard Top 5
        LeaderboardUI.SetActive(true);
    }
}

