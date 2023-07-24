using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private void Start()
    {
        //Loads LocaleKey stored in PlayerPrefs
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID);
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

}
