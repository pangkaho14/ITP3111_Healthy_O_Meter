using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private PlayerHealthPoints playerHP;
    private float lerpSpeed = 3f;

    private void Start()
    {
        playerHP.Heal(playerHP.GetMaxHealth());
    }

    private void Update()
    {
        // this will gradually reduce the health bar after taking damage or healing, instead of instant transformation
        // Mathf.Lerp(startValue, endValue, speed);
        healthBarSlider.value = Mathf.Lerp(healthBarSlider.value, playerHP.GetCurrentHealth() / playerHP.GetMaxHealth(), lerpSpeed * Time.unscaledDeltaTime);
        
        // this will gradually change the colour based on the health and the max health
        Color healthBarColor = Color.Lerp(Color.red, Color.green, healthBarSlider.value);
        healthBarFillImage.color = healthBarColor;
    }
}