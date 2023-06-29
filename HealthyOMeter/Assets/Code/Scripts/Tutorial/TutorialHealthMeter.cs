using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TutorialHealthMeter : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        //Get the buttons from UI document
        Button buttonHome = root.Q<Button>("btn_home");


        //On click listeners for the buttons
        buttonHome.clicked += () => LoadNextScene("Game");
    }

    private void LoadNextScene(string sceneName)
    {
        // Replace "NextSceneName" with the name or index of your desired next scene
        SceneManager.LoadScene(sceneName);
    }
}
