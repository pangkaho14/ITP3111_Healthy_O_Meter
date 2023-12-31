using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FunFactsManager : MonoBehaviour
{
    // Reference to the FunFactDatabase scriptable object
    public FunFactDatabase hawkerFunFactDatabase;
    public FunFactDatabase groceryFunFactDatabase;
    private FunFactDatabase currentFunFactDatabase; // Hold the reference to the selected database

    public TMPro.TextMeshProUGUI Header;
    public TMPro.TextMeshProUGUI FunFactWithImage;
    public TMPro.TextMeshProUGUI FunFactWithoutImage;
    public Image FunFactsImage;

    private int localeKey = 0; // Localization
    private int scenarioKey = 0; // Scenario picked

    private void ShowRandomFunFact()
    {
        GetDatabase();
        if (currentFunFactDatabase.funFactsList.Count == 0)
        {
            // Handle the case when there are no fun facts available
            UnityEngine.Debug.Log("No fun facts available.");
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, currentFunFactDatabase.funFactsList.Count);
        FunFactData currentFunFact = currentFunFactDatabase.funFactsList[randomIndex];

        // Check localisation
        localeKey = PlayerPrefs.GetInt("LocaleKey");
        // 0 means it is english, else it is chinese
        currentFunFact.SetEnglishLocalization(localeKey == 0);

        // Funfact and header after changing language
        string funFactLocalized= currentFunFact.GetFunFactsText();
        string formattedFunFact = currentFunFact.GetFormattedFunFact(funFactLocalized);
        string headerLocalized = currentFunFact.GetHeaderText();
        /*UnityEngine.Debug.Log("funfact: " + formattedFunFact);
        UnityEngine.Debug.Log("header: " + headerLocalized);*/


        // Display the header
        Header.text = headerLocalized;

        

        // Check if the fun fact has an image
        if (currentFunFact.image != null)
        {
            // Assign the sprite & text to the components
            FunFactsImage.sprite = currentFunFact.image;
            FunFactWithImage.text = formattedFunFact;

            // Activate the relevant gameobjects
            FunFactWithImage.gameObject.SetActive(true);
            FunFactsImage.gameObject.SetActive(true);

            // Hide gameobjects not used
            FunFactWithoutImage.gameObject.SetActive(false);
        }
        else
        {
            // Assign the sprite & text to the components
            FunFactWithoutImage.text = formattedFunFact;

            // Activate the relevant gameobjects
            FunFactWithoutImage.gameObject.SetActive(true);

            // Hide or disable the fun fact image
            FunFactsImage.gameObject.SetActive(false);
            FunFactWithImage.gameObject.SetActive(false);
        }
    }

    
    public void DisplayFunFact()
    {
        ShowRandomFunFact();
    }


    private void GetDatabase()
    {
        scenarioKey = PlayerPrefs.GetInt("selectedScenarioName");
        if (scenarioKey == 0) 
        {
            // Grocery Scenario
            currentFunFactDatabase = groceryFunFactDatabase;
            /*UnityEngine.Debug.Log("Using Grocery DB");*/
        }
        else
        {
            // Hawker Scenario
            currentFunFactDatabase = hawkerFunFactDatabase;
           /* UnityEngine.Debug.Log("Using Hawker DB");*/
        }
    }
}
