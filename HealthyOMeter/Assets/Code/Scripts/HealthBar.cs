using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private const float MIN_HEALTH = 0;
    private const float MAX_HEALTH = 100;
    
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Slider healthBarSlider;
    private float lerpSpeed = 3f;
    private float currentHealth = 100f;
    
    private void Start()
    {
        currentHealth = MAX_HEALTH;
    }

    private void Update()
    {
        // never below min health, and never above max health
        currentHealth = Mathf.Clamp(currentHealth, MIN_HEALTH, MAX_HEALTH);
        
        healthText.text = $"Health: {currentHealth}%";
        
        // this will gradually reduce the health bar after taking damage or healing, instead of instant transformation
        // Mathf.Lerp(startValue, endValue, speed);
        healthBarSlider.value = Mathf.Lerp(healthBarSlider.value, currentHealth / MAX_HEALTH, lerpSpeed * Time.deltaTime);
        
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
}
