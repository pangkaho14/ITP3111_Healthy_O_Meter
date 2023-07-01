using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour{
    public TextMeshProUGUI pointsText;

    public void Setup(int score){
        gameObject.SetActive(true);
        //To be added in the future to display the points gained
        pointsText.text = score.ToString() + " Points";
    }

    public void PlayAgainButton() {
        SceneManager.LoadScene("Lanes");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Game");
    }
}
    