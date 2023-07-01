using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TutorialMovement : MonoBehaviour
{
    public void TutorialItemButton()
    {
        SceneManager.LoadScene("TutorialItems");
    }
}
