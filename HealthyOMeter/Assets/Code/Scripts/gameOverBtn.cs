using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverBtn : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonStartAgain = root.Q<Button>("btn_playAgain");
        buttonStartAgain.clicked += LoadNextScene;
    }

    private void LoadNextScene()
    {
        // Replace "NextSceneName" with the name or index of your desired next scene
        SceneManager.LoadScene("Lanes");
    }
}
