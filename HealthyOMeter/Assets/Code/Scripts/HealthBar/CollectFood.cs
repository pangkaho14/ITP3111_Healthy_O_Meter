using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CollectFood : MonoBehaviour
{
    // assign the PlayerHealthPoints Scriptable Object you want from the UI Inspector
    [SerializeField] private PlayerHealthPoints playerHp;
    [SerializeField] private float healAmt = 20f;
    [SerializeField] private float damageAmt = 20f;

    [SerializeField] private ScoreKeeper scoreKeeper;
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
    // Calculate the percentage amount to decrease from the current spawn speed
    [SerializeField] private float percentageDecrease = 0.10f; // 10% decrease, you can adjust this value as needed
    [SerializeField] private string nutriAText;
    [SerializeField] private string nutriBText;
    [SerializeField] private string nutriCText;
    [SerializeField] private string nutriDText;
    // Speed decrease amount when an object with "unhealthy" tag is touched
    [SerializeField] private float speedDecreaseAmount;

    // Declaration of Food Collection SFX
    [SerializeField] private AudioSource RightItem;
    [SerializeField] private AudioSource WrongItem;

    // Reference to the SpawnSprite script
    public SpawnSprite spawnSprite; // Assign this reference in the Inspector

    // Events for tutorial popup messages
    [SerializeField] private UnityEvent HealthyContactEvent;
    [SerializeField] private UnityEvent UnhealthyContactEvent;
    private bool isHealthyContactEvent = true;
    private bool isUnhealthyContactEvent = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("healthy"))
        {
            playerHp.Heal(healAmt);
            scoreKeeper.AddScore(scoreAmt, true);
            scoreKeeper.AddCombo();

            // Update new score with current multiplier
            string newScore = scoreKeeper.GetNewScoreAmt(scoreAmt);

            // Trigger text
            if (FloatingTextAddPointsPrefab)
            {
                ShowFloatingText(newScore); // Pass the custom text
            }
            RightItem.Play();

            // Invoke event to pop up text
            if (isHealthyContactEvent)
            {
                HealthyContactEvent.Invoke();
                isHealthyContactEvent = false;
            }
            
        }
        else if (other.CompareTag("unhealthy"))
        {
            // Trigger overlay effect
            overlayEffect.ShowOverlay();
            playerHp.TakeDamage(damageAmt);
            scoreKeeper.ResetScoreAndCombo();
            WrongItem.Play();

            float spawnSpeedDecreaseAmount = spawnSprite.spawnSpeed * percentageDecrease;
            spawnSprite.spawnSpeed -= spawnSpeedDecreaseAmount;


            // Cap the spawn speed at the minimum value
            if (spawnSprite.spawnSpeed < 1.3f)
            {
                spawnSprite.spawnSpeed = 1.3f;
            }

            // Update the spawn speed-related fields in SpawnSprite
            spawnSprite.UpdateSpawnSpeedFields(spawnSprite.spawnInterval, spawnSprite.spawnSpeedIncreaseAmount, spawnSprite.spawnSpeedIncreaseInterval);

            // Invoke event to pop up text
            if (isUnhealthyContactEvent)
            {
                UnhealthyContactEvent.Invoke();
                isUnhealthyContactEvent = false;
            }
        }
        else if (other.CompareTag("Nutri-A"))
        {
            float scoreAmt = GetScoreFromText(nutriAText);  // Get the score amount from the nutriAText
            scoreKeeper.AddScore(scoreAmt, true);
            RightItem.Play();

            //Update new score with current multiplier
            string newScore = scoreKeeper.GetNewScoreAmt(scoreAmt);
            ShowFloatingText(newScore);
        }
        else if (other.CompareTag("Nutri-B"))
        {
            float scoreAmt = GetScoreFromText(nutriBText);  // Get the score amount from the nutriBText
            scoreKeeper.AddScore(scoreAmt, false);
            ShowFloatingText(nutriBText);
            RightItem.Play();

        }
        else if (other.CompareTag("Nutri-C"))
        {
            float scoreAmt = GetScoreFromText(nutriCText);  // Get the score amount from the nutriCText
            scoreKeeper.AddScore(scoreAmt, false);
            ShowFloatingText(nutriCText);
            WrongItem.Play();
        }
        else if (other.CompareTag("Nutri-D"))
        {
            float scoreAmt = GetScoreFromText(nutriDText);  // Get the score amount from the nutriDText
            scoreKeeper.AddScore(scoreAmt, false);
            ShowFloatingText(nutriDText);
            WrongItem.Play();
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

            // Change text color to red if the amount is negative
            float amount = float.Parse(text);
            if (amount < 0)
            {
                textMesh.color = Color.red;
            }
        }
    }

    private void EventTriggered()
    {

    }
}
