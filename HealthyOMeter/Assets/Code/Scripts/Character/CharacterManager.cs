using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;

    private int selectedOption = 0;

    void Start()
    {
        if(!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOption);
    }

    //Aunty Option
    public void BackOption()
    {
        selectedOption = 0;
        Debug.Log("Option 0");
        UpdateCharacter(selectedOption);
        Save();
    }

    //Uncle Option
    public void NextOption()
    {
        selectedOption = 1;
        Debug.Log("Option 1");
        UpdateCharacter(selectedOption);
        Save();
    }

    public void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

}
