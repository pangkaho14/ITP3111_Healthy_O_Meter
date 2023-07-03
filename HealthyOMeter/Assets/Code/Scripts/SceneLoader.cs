using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public string sceneNameTutorial;
    public string sceneNamePlay;
    public string sceneNameSettings;
    public string sceneNameLeaderBoard;

    void Start()
    {


    }

    void Update()
    {





    }

    public void LoadSceneTutorial()
    {
        SceneManager.LoadScene(sceneNameTutorial);
    }
    public void LoadScenePlay()
    {
        SceneManager.LoadScene(sceneNamePlay);
    }
    public void LoadSceneSettings()
    {
        SceneManager.LoadScene(sceneNameSettings);
    }
    public void LoadSceneLeaderBoard()
    {
        SceneManager.LoadScene(sceneNameLeaderBoard);
    }

}

