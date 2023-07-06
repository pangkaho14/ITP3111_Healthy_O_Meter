using UnityEngine;
using UnityEngine.UI;

public class OverlayEffect : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;       // Duration of the fade effect
    [SerializeField] private float fadingSpeed = 1f;        // Speed at which the overlay fades
    private Image overlayImage;                             // Reference to the Image component
    private float fadeTimer;                                // Timer for the fade effect
    private bool isFading;                                  // Flag to control fading state

    // Desired RGBA color values
    [SerializeField] private Color targetColor;

    private void Awake()
    {
        overlayImage = GetComponent<Image>();              // Get the Image component on Awake
        overlayImage.enabled = false;                       // Disable the Image component by default
    }

    public void ShowOverlay()
    {
        overlayImage.enabled = true;                        // Enable the Image component
        overlayImage.color = targetColor;                   // Set the target color
        fadeTimer = fadeDuration;                           // Reset the fade timer
        isFading = true;                                    // Set the fading flag to true
    }

    private void Update()
    {
        if (isFading)
        {
            Color currentColor = overlayImage.color;         // Get the current color of the overlay
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);  // Calculate the alpha value based on the fade timer
            currentColor.a = alpha * targetColor.a;          // Update the alpha component relative to the target color's alpha
            overlayImage.color = currentColor;               // Apply the updated color to the overlay
            fadeTimer -= Time.deltaTime * fadingSpeed;       // Decrement the fade timer based on time and fading speed
            if (fadeTimer <= 0f)
            {
                overlayImage.enabled = false;               // Disable the Image component
                isFading = false;                            // Set the fading flag to false
            }
        }
    }
}
