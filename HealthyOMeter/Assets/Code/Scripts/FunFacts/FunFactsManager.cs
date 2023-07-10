using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FunFactsManager : MonoBehaviour
{
    public FunFactDatabase funFactDatabase; // Reference to the FunFactDatabase scriptable object
    public TMPro.TextMeshProUGUI Header;
    public TMPro.TextMeshProUGUI Funfacts;
    public Image FunFactsImage;

    public void ShowRandomFunFact()
    {
        if (funFactDatabase.funFactsList.Count == 0)
        {
            // Handle the case when there are no fun facts available
            UnityEngine.Debug.Log("No fun facts available.");
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, funFactDatabase.funFactsList.Count);
        FunFactData currentFunFact = funFactDatabase.funFactsList[randomIndex];

        string formattedFunFact = currentFunFact.GetFormattedFunFact();
        UnityEngine.Debug.Log(formattedFunFact);

        // Display the header
        Header.text = currentFunFact.header;

        // Display the fun fact sentence
        Funfacts.text = currentFunFact.funFact;

        // Check if the fun fact has an image
        if (currentFunFact.image != null)
        {
            // Assign the sprite to the Image component
            FunFactsImage.sprite = currentFunFact.image;
            FunFactsImage.gameObject.SetActive(true);
        }
        else
        {
            // Hide or disable the fun fact image
            FunFactsImage.gameObject.SetActive(false);
        }
    }
}
