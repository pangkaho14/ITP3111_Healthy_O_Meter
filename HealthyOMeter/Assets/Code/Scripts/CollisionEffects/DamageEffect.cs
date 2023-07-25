using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float fadingSpeed = 1f;
    private Image overlayImage;
    private float fadeTimer;
    private bool isFading;

    // Desired RGBA color values
    [SerializeField] private Color targetColor;

    private void Awake()
    {
        overlayImage = GetComponent<Image>();
        overlayImage.enabled = false;
    }

    public void ShowOverlay()
    {
        overlayImage.enabled = true;
        overlayImage.color = targetColor; // Set the target color
        fadeTimer = fadeDuration;
        isFading = true;
    }

    private void Update()
    {
        if (isFading)
        {
            Color currentColor = overlayImage.color;
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);
            currentColor.a = alpha * targetColor.a; // Update the alpha component relative to the target color's alpha
            overlayImage.color = currentColor;
            fadeTimer -= Time.deltaTime * fadingSpeed;
            if (fadeTimer <= 0f)
            {
                overlayImage.enabled = false;
                isFading = false;
            }
        }
    }
}
