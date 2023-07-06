using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Diagnostics;

public class HealthBar : MonoBehaviour
{
    private const float MIN_HEALTH = 0;
    private const float MAX_HEALTH = 100;
    
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Slider healthBarSlider;
    
    private float lerpSpeed = 3f;
    private float currentHealth = MAX_HEALTH;

    public UnityEvent OnHealthDepleted;
    private bool isHealthDepleted = false; // Flag to track if the health has been depleted

    private void Awake()
    {
        currentHealth = MAX_HEALTH;
        if (OnHealthDepleted == null)
        {
            OnHealthDepleted = new UnityEvent();
        }
    }

    private void Update()
    {
        // never below min health, and never above max health
        currentHealth = Mathf.Clamp(currentHealth, MIN_HEALTH, MAX_HEALTH);

        // when player is dead
        if (!isHealthDepleted && currentHealth <= MIN_HEALTH)
        {
            OnHealthDepleted.Invoke();
            isHealthDepleted=true; // Set the flag to true after invoking the event
        }

        healthText.text = $"Health: {currentHealth}%";

        // this will gradually reduce the health bar after taking damage or healing, instead of instant transformation
        // Mathf.Lerp(startValue, endValue, speed);
        healthBarSlider.value = Mathf.Lerp(healthBarSlider.value, currentHealth / MAX_HEALTH, lerpSpeed * Time.unscaledDeltaTime);

        // this will gradually change the colour based on the health and the max health
        Color healthBarColor = Color.Lerp(Color.red, Color.green, healthBarSlider.value);
        healthBarFillImage.color = healthBarColor;

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
    
    public void Heal(float heal)
    {
        currentHealth += heal;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
