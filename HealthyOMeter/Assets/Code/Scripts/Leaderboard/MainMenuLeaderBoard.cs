using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLeaderBoard : MonoBehaviour
{
    [SerializeField] private LeaderboardUI leaderboardUI;
    [SerializeField] private FunFactsManager funFactsUI;
    // To add the fun fact script;
    // Start is called before the first frame update
    void Start()
    {
        // Display the leaderboard and fun facts
        leaderboardUI.UpdateLeaderboard();
        funFactsUI.DisplayFunFact();

    }
}
