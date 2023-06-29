using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    public HealthBar healthBar;
    
    void Start()
    {
        healthBar = FindObjectOfType<HealthBar>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("healthy"))
        {
            healthBar.TakeDamage(20);
        }
        
        Destroy(other.gameObject);
    }
}
