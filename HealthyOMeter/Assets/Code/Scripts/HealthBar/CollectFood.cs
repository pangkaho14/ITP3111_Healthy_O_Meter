using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFood : MonoBehaviour
{
    // assign the health bar instance you want from the UI Inspector
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private float healAmt = 20f;
    [SerializeField] private float damageAmt = 20f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("healthy"))
        {
            healthBar.Heal(healAmt);
        }
        else if (other.CompareTag("unhealthy"))
        {
            healthBar.TakeDamage(damageAmt);
        }
        
        Destroy(other.gameObject);
    }
}
