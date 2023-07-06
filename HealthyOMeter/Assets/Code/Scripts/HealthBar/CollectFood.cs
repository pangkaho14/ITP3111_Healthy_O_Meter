using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFood : MonoBehaviour
{
    // assign the health bar instance you want from the UI Inspector
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ScoreKeeper scoreKeeper;

    [SerializeField] private float healAmt = 20f;
    [SerializeField] private float damageAmt = 20f;
    [SerializeField] private float scoreAmt = 100f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("healthy"))
        {
            healthBar.Heal(healAmt);
            scoreKeeper.Add(scoreAmt);
        }
        else if (other.CompareTag("unhealthy"))
        {
            healthBar.TakeDamage(damageAmt);
        }
        
        Destroy(other.gameObject);
    }
}
