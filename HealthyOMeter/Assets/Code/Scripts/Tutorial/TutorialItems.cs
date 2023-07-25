using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class TutorialItems : MonoBehaviour
{
    //Game objects needed
    public GameObject HawkerFood;
    public GameObject NTUCFood;
    public GameObject NutrigradeLabel;
    public GameObject Healthy;
    public GameObject Unhealthy;
    public UnityEngine.UI.Button defaultSelectedButton;

    void Start()
    {
        defaultSelectedButton.Select();
    }

    public void HawkerButton()
    {
        ActivateItemScreen("Hawker");
        ActivateLabel();
    }

    public void NTUCButton()
    {
        ActivateItemScreen("NTUC");
        ActivateLabel();
    }
    
    public void NutrigradeButton()
    {
        ActivateItemScreen("Nutrigrade");
        DeactivateLabel();
    }

    private void ActivateItemScreen(string item)
    {
        switch (item)
        {
            case "Hawker":
                HawkerFood.SetActive(true);
                NTUCFood.SetActive(false);
                NutrigradeLabel.SetActive(false);
                break;
            case "NTUC":
                HawkerFood.SetActive(false);
                NTUCFood.SetActive(true);
                NutrigradeLabel.SetActive(false);
                break;
            case "Nutrigrade":
                HawkerFood.SetActive(false);
                NTUCFood.SetActive(false);
                NutrigradeLabel.SetActive(true);
                break;
        }
    }

    private void ActivateLabel()
    {
        Healthy.SetActive(true);
        Unhealthy.SetActive(true);
    }

    private void DeactivateLabel()
    {
        Healthy.SetActive(false);
        Unhealthy.SetActive(false);
    }

    public void NextButton()
    {
        SceneManager.LoadScene("TutorialHealthmeter");
    }
}
