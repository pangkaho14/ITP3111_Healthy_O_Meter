using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectFood : MonoBehaviour
{
    // Assign the health bar instance you want from the UI Inspector
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ScoreKeeper scoreKeeper;

    [SerializeField] private float healAmt = 20f;
    [SerializeField] private float damageAmt = 20f;
    public GameObject FloatingTextAddPointsPrefab;
    [SerializeField] private float scoreAmt = 100f;

    // Customizable text to display when colliding
    [SerializeField] private string addPointsText;

    // Offset variables for text position
    [SerializeField] private float pointsCollectedoffsetX;
    [SerializeField] private float pointsCollectedoffsetY;
    [SerializeField] private DamageEffect overlayEffect;
    // Fixed position for the floating text
    private Vector3 fixedPosition;

    // Text size parameter
    [SerializeField] private float pointsTextSize = 30f;

    [SerializeField] private string nutriAText;
    [SerializeField] private string nutriBText;
    [SerializeField] private string nutriCText;
    [SerializeField] private string nutriDText;

    //Declaration of Food Collection SFX
    [SerializeField] private AudioSource RightItem;
    [SerializeField] private AudioSource WrongItem;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("healthy"))
        {
            healthBar.Heal(healAmt);
            scoreKeeper.Add(scoreAmt);
            // Trigger text
            if (FloatingTextAddPointsPrefab)
            {
                ShowFloatingText(addPointsText); // Pass the custom text
            }
            RightItem.Play();
            
        }
        else if (other.CompareTag("unhealthy"))
        {
            // Trigger overlay effect
            overlayEffect.ShowOverlay();
            healthBar.TakeDamage(damageAmt);
            WrongItem.Play();
            
        }
       else if (other.CompareTag("Nutri-A"))
        {
            float scoreAmt = GetScoreFromText(nutriAText);  // Get the score amount from the nutriAText
            scoreKeeper.Add(scoreAmt);
            ShowFloatingText(nutriAText);
            RightItem.Play();
            
        }
        else if (other.CompareTag("Nutri-B"))
        {
            float scoreAmt = GetScoreFromText(nutriBText);  // Get the score amount from the nutriBText
            scoreKeeper.Add(scoreAmt);
            ShowFloatingText(nutriBText);
            RightItem.Play();
        }
        else if (other.CompareTag("Nutri-C"))
        {
            float scoreAmt = GetScoreFromText(nutriCText);  // Get the score amount from the nutriCText
            scoreKeeper.Add(scoreAmt);
            ShowFloatingText(nutriCText);
            RightItem.Play();
        }
        else if (other.CompareTag("Nutri-D"))
        {
            float scoreAmt = GetScoreFromText(nutriDText);  // Get the score amount from the nutriDText
            scoreKeeper.Add(scoreAmt);
            ShowFloatingText(nutriDText);
            RightItem.Play();
        }
        Destroy(other.gameObject);
    }
    private float GetScoreFromText(string text)
    {
        float scoreAmt = 0f;
        if (float.TryParse(text, out scoreAmt))
        {
            return scoreAmt;
        }
        else
        {
            Debug.LogWarning("Invalid score amount: " + text);
            return 0f;
        }
    }

    void ShowFloatingText(string text)
    {
        GameObject textObject = Instantiate(FloatingTextAddPointsPrefab, fixedPosition, Quaternion.identity);

        // Access the TextMeshPro component on the instantiated object
        TextMeshPro textMesh = textObject.GetComponent<TextMeshPro>();
        if (textMesh != null)
        {
            textMesh.transform.position += new Vector3(pointsCollectedoffsetX, pointsCollectedoffsetY, 0f);
            textMesh.text = text; // Set the custom text
            textMesh.fontSize = pointsTextSize; // Set the text size
        }
    }
}
