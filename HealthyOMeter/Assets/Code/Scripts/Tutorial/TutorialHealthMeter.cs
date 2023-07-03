using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TutorialHealthMeter : MonoBehaviour
{
    public void HomeButton()
    {
        SceneManager.LoadScene("Game");
    }
}
