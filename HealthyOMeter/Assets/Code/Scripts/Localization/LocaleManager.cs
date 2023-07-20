using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocaleManager : MonoBehaviour
{
    //Create LocaleKey variable for assignment of English or Mandarin Selection
    private int LocaleKey = 0;
    public Color SelectedColor;
    public Color NonSelectedColor;
    public Button EnglishLanguageButton;
    public Button MandarinLanguageButton;

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
        ChangeLocale(LocaleKey);
        //Check if it is in mandarin
        if (LocaleKey == 1)
        {
            //Mandarin Button becomes grey, English Button becomes orange
            SelectedMandarinButtonColor();
            NonSelectedEnglishButtonColor();
        }
        else
        {
            //English Button becomes grey, Mandarin Button becomes orange
            SelectedEnglishButtonColor();
            NonSelectedMandarinButtonColor();
        }
    }

    //English Option
    public void EnglishOption()
    {
        LocaleKey = 0;
        Debug.Log("English");
        ChangeLocale(LocaleKey);;
        Save();
        if (LocaleKey == 1)
        {
            SelectedMandarinButtonColor();
            NonSelectedEnglishButtonColor();
        }
        else
        {
            SelectedEnglishButtonColor();
            NonSelectedMandarinButtonColor();
        }
    }

    //Mandarin Option
    public void MandarinOption()
    {
        LocaleKey = 1;
        Debug.Log("Mandarin");
        ChangeLocale(LocaleKey);;
        Save();
        if (LocaleKey == 1)
        {
            SelectedMandarinButtonColor();
            NonSelectedEnglishButtonColor();
        }
        else
        {
            SelectedEnglishButtonColor();
            NonSelectedMandarinButtonColor();
        }
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

    //Grey English Color
    public void SelectedEnglishButtonColor()
    {
        ColorBlock cb = EnglishLanguageButton.colors;
        cb.normalColor = SelectedColor;
        cb.highlightedColor = SelectedColor;
        cb.pressedColor = SelectedColor;
        EnglishLanguageButton.colors = cb;
    }

    //Grey Mandarin Color
    public void SelectedMandarinButtonColor()
    {
        ColorBlock cb = MandarinLanguageButton.colors;
        cb.normalColor = SelectedColor;
        cb.highlightedColor = SelectedColor;
        cb.pressedColor = SelectedColor;
        MandarinLanguageButton.colors = cb;
    }

    //Orange English Color
    public void NonSelectedEnglishButtonColor()
    {
        ColorBlock cb = EnglishLanguageButton.colors;
        cb.normalColor = NonSelectedColor;
        cb.highlightedColor = NonSelectedColor;
        cb.pressedColor = NonSelectedColor;
        EnglishLanguageButton.colors = cb;
    }

    //Orange Mandarin Color
    public void NonSelectedMandarinButtonColor()
    {
        ColorBlock cb = MandarinLanguageButton.colors;
        cb.normalColor = NonSelectedColor;
        cb.highlightedColor = NonSelectedColor;
        cb.pressedColor = NonSelectedColor;
        MandarinLanguageButton.colors = cb;
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
