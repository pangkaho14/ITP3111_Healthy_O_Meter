using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TutorialItems : MonoBehaviour
{
    //Game objects needed
    public GameObject HawkerFood;
    public GameObject NTUCFood;

    public void HawkerButton()
    {
        HawkerFood.SetActive(true);
        NTUCFood.SetActive(false);
    }

    public void NTUCButton()
    {
        HawkerFood.SetActive(false);
        NTUCFood.SetActive(true);
    }

    public void NextButton()
    {
        SceneManager.LoadScene("TutorialHealthmeter");
    }
}
