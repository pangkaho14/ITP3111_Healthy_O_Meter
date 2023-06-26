using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TutorialMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        //Get the buttons from UI document
        Button buttonItems = root.Q<Button>("btn_items");


        //On click listeners for the buttons
        buttonItems.clicked += () => LoadNextScene("TutorialItems");
    }

    private void LoadNextScene(string sceneName)
    {
        // Replace "NextSceneName" with the name or index of your desired next scene
        SceneManager.LoadScene(sceneName);
    }
}
