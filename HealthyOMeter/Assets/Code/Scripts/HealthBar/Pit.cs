using UnityEngine;

public class Pit : MonoBehaviour
{
    // assign the health bar instance you want from the UI Inspector
    [SerializeField] private PlayerHealthPoints playerHealthPoints;
    
    [SerializeField] private DamageEffect overlayEffect;
    [SerializeField] private ScoreKeeper scoreKeeper;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("healthy"))
        {
            playerHealthPoints.TakeDamage(20);
            overlayEffect.ShowOverlay();
            scoreKeeper.ResetScoreAndCombo();
        }
        
        Destroy(other.gameObject);
    }
}
