using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    // assign the health bar instance you want from the UI Inspector
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private DamageEffect overlayEffect;
    [SerializeField] private ScoreKeeper scoreKeeper;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("healthy"))
        {
            healthBar.TakeDamage(20);
            overlayEffect.ShowOverlay();
            scoreKeeper.ResetScoreAndCombo();
        }
        
        Destroy(other.gameObject);
    }
}
