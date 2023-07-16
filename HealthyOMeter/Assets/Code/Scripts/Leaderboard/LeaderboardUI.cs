using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leaderboardText;

    public void UpdateLeaderboard()
    {
        // Retrieve the leaderboard data from PlayerPrefs
        string leaderboardJson = PlayerPrefs.GetString("Leaderboard");
        LeaderboardManager.ScoreEntryList leaderboardData = JsonUtility.FromJson<LeaderboardManager.ScoreEntryList>(leaderboardJson);

        if (leaderboardData != null)
        {
            // Sort the leaderboard data by score (descending order)
            leaderboardData.scoreEntries = leaderboardData.scoreEntries.OrderByDescending(entry => entry.score).ToList();

            // Create a string to hold the leaderboard text
            string leaderboardTextString = "";

            // Display the top 5 entries in the leaderboard, or as many as available
            int maxEntries = Mathf.Min(5, leaderboardData.scoreEntries.Count);
            if (maxEntries > 0)
            {
                for (int i = 0; i < maxEntries; i++)
                {
                    leaderboardTextString += $"{i + 1}: {leaderboardData.scoreEntries[i].playerName} {leaderboardData.scoreEntries[i].score}\n";
                }
            }
            else
            {
                // there is no entries in leaderboard
                leaderboardTextString = "No leaderboard entries";
            }
            // Set the leaderboard text in the TextMeshProUGUI component
            leaderboardText.text = leaderboardTextString;
        }
        else
        {
            // there is no score entry list
            string leaderboardTextString = "No leaderboard entries";
            // Set the leaderboard text in the TextMeshProUGUI component
            leaderboardText.text = leaderboardTextString;
        }
    }
}