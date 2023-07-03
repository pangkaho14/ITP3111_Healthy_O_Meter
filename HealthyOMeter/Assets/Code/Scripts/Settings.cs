using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Settings : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        //Get the Exit Button
        Button buttonExitSettings = root.Q<Button>("btnExit");
        buttonExitSettings.clicked += LoadNextScene;
    }

    private void LoadNextScene()
    {
        // Replace "NextSceneName" with the name or index of your desired next scene
        SceneManager.LoadScene("Game");
    }
}
