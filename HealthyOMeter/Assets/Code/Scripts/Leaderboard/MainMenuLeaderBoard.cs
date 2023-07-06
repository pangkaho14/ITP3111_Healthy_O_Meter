using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLeaderBoard : MonoBehaviour
{
    [SerializeField] private LeaderboardUI leaderboardUI;
    //To add the fun fact script;
    // Start is called before the first frame update
    void Start()
    {
        leaderboardUI.UpdateLeaderboard();
        //Update funfacts();
    }
}
