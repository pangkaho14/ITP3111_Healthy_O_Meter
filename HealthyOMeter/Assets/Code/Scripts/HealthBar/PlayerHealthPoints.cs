using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerHealthPoints", menuName = "Player Health Points")]
public class PlayerHealthPoints : ScriptableObject
{
    [SerializeField] private float minHealth = 0;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 100;
    [SerializeField] private UnityEvent DeathEvent;

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            Debug.LogError("Damage amount must be positive");
            return;
        }
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
        
        if (currentHealth <= minHealth)
        {
            DeathEvent.Invoke();
        }
    }
    
    public void Heal(float heal)
    {
        if (heal < 0)
        {
            Debug.LogError("Heal amount must be positive");
            return;
        }
        currentHealth += heal;
        currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
    }
    
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    
    public float GetMinHealth()
    {
        return minHealth;
    }
}
