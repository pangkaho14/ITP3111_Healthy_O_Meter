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

    // Fixed position for the floating text
    private Vector3 fixedPosition;

    // Text size parameter
    [SerializeField] private float pointsTextSize = 30f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("healthy"))
        {
            healthBar.Heal(healAmt);
            scoreKeeper.Add(scoreAmt);
            // Trigger text
            if (FloatingTextAddPointsPrefab)
            {
                ShowFloatingText(); // No need to pass the collision position
            }

        }
        else if (other.CompareTag("unhealthy"))
        {
            healthBar.TakeDamage(damageAmt);
        }

       

        Destroy(other.gameObject);
    }

    void ShowFloatingText()
    {
        GameObject textObject = Instantiate(FloatingTextAddPointsPrefab, fixedPosition, Quaternion.identity);

        // Access the TextMeshPro component on the instantiated object
        TextMeshPro textMesh = textObject.GetComponent<TextMeshPro>();
        if (textMesh != null)
        {
            textMesh.transform.position += new Vector3(pointsCollectedoffsetX, pointsCollectedoffsetY, 0f);
            textMesh.text = addPointsText; // Set the custom text
            textMesh.fontSize = pointsTextSize; // Set the text size
        }
    }
}
