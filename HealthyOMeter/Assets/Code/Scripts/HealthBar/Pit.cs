using UnityEngine;

public class Pit : MonoBehaviour
{
    // assign the health bar instance you want from the UI Inspector
    [SerializeField] private PlayerHealthPoints healthBar;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("healthy"))
        {
            healthBar.TakeDamage(20);
        }
        
        Destroy(other.gameObject);
    }
}
