using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFood : MonoBehaviour
{
    public HealthBar healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
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
