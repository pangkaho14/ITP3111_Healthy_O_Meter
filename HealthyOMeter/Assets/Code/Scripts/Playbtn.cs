using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Playbtn : MonoBehaviour

{
 
    // Start is called before the first frame update
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonplay = root.Q<Button>("btn_play");
        buttonplay.clicked += LoadNextScene; 
    }

    private void LoadNextScene()
    {
        // Replace "NextSceneName" with the name or index of your desired next scene
        SceneManager.LoadScene("Lanes");
    }
}
