using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSceneChanger : MonoBehaviour
{
    public Sprite hawkerSceneSprite; // Assign the "hawkerscene" sprite in the inspector
    public Sprite ntucSceneSprite; // Assign the "ntucscene" sprite in the inspector
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int selectedScene = PlayerPrefs.GetInt("selectedScenarioName");

        if (selectedScene == 1)
        {
            spriteRenderer.sprite = hawkerSceneSprite;
        }
        else if (selectedScene == 0)
        {
            spriteRenderer.sprite = ntucSceneSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Any additional logic you want to perform during Update
    }
}
