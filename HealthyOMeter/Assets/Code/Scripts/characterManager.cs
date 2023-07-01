using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class characterManager : MonoBehaviour
{
    private void OnEnable()
    {
        //VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        //Get the Aunty and Uncle Icon Buttons
       // Button buttonAunty = root.Q<Button>("btnAunty");
      //  Button buttonUncle = root.Q<Button>("btnUncle");
      //  buttonAunty.clicked += () => LoadNextScene("Lanes");
       // buttonUncle.clicked += () => LoadNextScene("LanesUnc");
    }

    private void LoadNextScene(string sceneName)
    {
        // Replace "NextSceneName" with the name or index of your desired next scene
        SceneManager.LoadScene(sceneName);
    }
}
