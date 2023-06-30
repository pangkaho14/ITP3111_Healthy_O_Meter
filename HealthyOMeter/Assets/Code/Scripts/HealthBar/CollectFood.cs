using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFood : MonoBehaviour
{
    // assign the health bar instance you want from the UI Inspector
    [SerializeField] private HealthBar healthBar;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("healthy"))
        {
            healthBar.Heal(20);
        }
        else if (other.CompareTag("unhealthy"))
        {
            healthBar.TakeDamage(20);
        }
        
        Destroy(other.gameObject);
    }
}
