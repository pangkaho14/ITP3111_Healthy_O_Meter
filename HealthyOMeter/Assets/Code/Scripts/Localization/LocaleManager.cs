using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LocaleManager : MonoBehaviour
{
    //Create LocaleKey variable for assignment of English or Mandarin Selection
    private int LocaleKey = 0;

    // [SerializeField] private AudioSource ButtonClick;

    void Start()
    {
        //Checks whether there is a LocaleKey stored in PlayerPrefs
        if(!PlayerPrefs.HasKey("LocaleKey"))
        {
            LocaleKey = 0;
        }
        else
        {
            //If there is a LocaleKey, load the selection made by User
            Load();
        }
        ChangeLocale(LocaleKey);;
    }

    //English Option
    public void EnglishOption()
    {
        LocaleKey = 0;
        Debug.Log("English");
        ChangeLocale(LocaleKey);;
        Save();
        // ButtonClick.Play();
    }

    //Mandarin Option
    public void MandarinOption()
    {
        LocaleKey = 1;
        Debug.Log("Mandarin");
        ChangeLocale(LocaleKey);;
        Save();
        // ButtonClick.Play();
    }

    private bool active = false;
    
    public void ChangeLocale(int localeID)
    {
        if(active == true)
        {
            return;
        }
        //Starts the Coroutine to set the Locale
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        //Checks the Localization Settings
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        //Set the Locale ID according the LocaleKey in PlayerPrefs
        PlayerPrefs.SetInt("LocaleKey", _localeID);
        active = false;
    }

    private void Load()
    {
        //Load LocaleKey from PlayerPrefs
        LocaleKey = PlayerPrefs.GetInt("LocaleKey");
    }

    private void Save()
    {
        //Set LocaleKey from PlayerPrefs
        PlayerPrefs.SetInt("LocaleKey", LocaleKey);
    }

    public void ChangeScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
