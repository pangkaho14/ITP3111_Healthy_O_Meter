using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour

{
 
    // Start is called before the first frame update
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        //Get the 4 buttons
        Button buttonPlay = root.Q<Button>("btn_play");
        Button buttonLeaderboard = root.Q<Button>("btn_leaderboard");
        Button buttonSettings = root.Q<Button>("btn_settings");
        Button buttonTutorial = root.Q<Button>("btn_tutorial");
        
        //On click listeners for the buttons
        buttonPlay.clicked += () => LoadNextScene("Lanes");
        buttonLeaderboard.clicked += () => LoadNextScene("LeaderboardHome");
        buttonSettings.clicked += () => LoadNextScene("Settings");
        buttonTutorial.clicked += () => LoadNextScene("TutorialMovement");
    }

    private void LoadNextScene(string sceneName)
    {
        // Replace "NextSceneName" with the name or index of your desired next scene
        SceneManager.LoadScene(sceneName);
    }
}
