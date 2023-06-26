using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TutorialItems : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        //Get the buttons from UI document
        Button buttonHealthMeter = root.Q<Button>("btn_healthmeter");


        //On click listeners for the buttons
        buttonHealthMeter.clicked += () => LoadNextScene("TutorialHealthMeter");
    }

    private void LoadNextScene(string sceneName)
    {
        // Replace "NextSceneName" with the name or index of your desired next scene
        SceneManager.LoadScene(sceneName);
    }
}
