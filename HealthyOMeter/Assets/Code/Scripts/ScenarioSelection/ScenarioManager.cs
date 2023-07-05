using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class ScenarioManager : MonoBehaviour
{
    public ScenarioDatabase ScenarioDB;

    private int selectedScene = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedScenarioName"))
        {
            selectedScene = 0;
        }
        else
        {
            Load();
        }
        UpdateScenario(selectedScene);
    }

    //Hawker Option
    public void BackOption()
    {
        selectedScene = 0;
        Debug.Log("Option 0");
        UpdateScenario(selectedScene);
        Save();
    }

    //NTUC Option
    public void NextOption()
    {
        selectedScene = 1;
        Debug.Log("Option 1");
        UpdateScenario(selectedScene);
        Save();
    }

    public void UpdateScenario(int selectedScene)
    {
      
        // Get the selected scenario from the ScenarioDatabase
        Scenario selectedScenario = ScenarioDB.GetScene(selectedScene);


    }   

    private void Load()
    {
        selectedScene = PlayerPrefs.GetInt("selectedScenarioName");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedScenarioName", selectedScene);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

}
