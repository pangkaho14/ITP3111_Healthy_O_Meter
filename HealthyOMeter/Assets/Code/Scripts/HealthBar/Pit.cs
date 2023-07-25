using UnityEngine;
using UnityEngine.Events;

public class Pit : MonoBehaviour
{
    // assign the health bar instance you want from the UI Inspector
    [SerializeField] private PlayerHealthPoints playerHealthPoints;
    
    [SerializeField] private DamageEffect overlayEffect;
    [SerializeField] private ScoreKeeper scoreKeeper;

    // Declaration of Uncollected Food SFX
    [SerializeField] private AudioSource WrongItem;

    // For tutorial popup message
    [SerializeField] private UnityEvent HealthyMissedEvent;
    private bool isHealthyMissedEvent = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("healthy"))
        {
            playerHealthPoints.TakeDamage(20);
            overlayEffect.ShowOverlay();
            scoreKeeper.ResetScoreAndCombo();
            WrongItem.Play();

            // Invoke event to pop up text
            if (isHealthyMissedEvent)
            {
                HealthyMissedEvent.Invoke();
                isHealthyMissedEvent = false;
            }
        }
        
        Destroy(other.gameObject);
    }
}
