using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 50;
    private int currentHP;
    private UnityEngine.UIElements.Label hpText;

    private void Start()
    {
        // Load the UI Document
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // Find the hpText Label within the UI Document
        hpText = root.Q<UnityEngine.UIElements.Label>("hpText");
        currentHP = maxHP;
        if (hpText == null)
        {
            UnityEngine.Debug.Log("hpText label object is not assigned in the Inspector.");
        }
    }

    public void ReduceHP(int amount)
    {
        currentHP -= amount;
        UpdateHPText(currentHP);
    }

    public void GainHP(int amount)
    {
        currentHP += amount;
        UpdateHPText(currentHP);
    }

    public void CheckHP()
    {
        if (currentHP <= 0)
        {
            //Move to gameover screen
            SceneManager.LoadScene("Game Over");
        }
    }

    private void UpdateHPText(int currentHP)
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentHP.ToString();
        }
    }

}
