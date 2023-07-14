using System;
using TMPro;
using UnityEngine;


/*
 * - Min Height, Max Height is to adjust the height of the sprite object how big do you want it to appear etc. 
 
   - Lane offset and Lane width is the offset between each lane and the width of each lane
    
   - Middle Lane Object determines which lane you want it to be in the middle for the object to spawn

   - Max spawn speed is the speed that the items drop how fast how slow.
    
   - Spawn interval is how many sprites u want to spawn at once.
 

** to change how many times you want to spawn at once - edit spawn interval 
*  to change the spawn speed increase amt(how many sprites should be spawn in one sec) as time increase- edit spawn speed increase amount. 
*  Spawn speed increase interval - is how fast u want ur speed interval to go per second the lower it is it makes the sprite drop even faster per second. 

 */


public class SpawnSprite : MonoBehaviour
{
    // The prefabs of the objects you want to spawn
    public GameObject[] ntucHealthyFoodPrefabs;
    public GameObject[] ntucUnhealthyFoodPrefabs;
    public GameObject[] hawkerHealthyFoodPrefabs;
    public GameObject[] hawkerUnhealthyFoodPrefabs;

    public GameObject[] nutrigrades; // Array for the NutriGrade
    public float constantNutrigradeSpeed;
    // Time interval between spawns (initially 1 second)
    public float spawnInterval = 1f;

    // The speed at which the object moves (initially 5 units per second)
    public float spawnSpeed = 5f;

    // The offset between each lane (initially 3 units)
    public float laneOffset = 3f;

    // The width of each lane (initially 5 units)
    public float laneWidth = 5f;

    // The minimum and maximum height of the spawned objects (initially 1 and 5 units respectively)
    public float minHeight = 1f;
    public float maxHeight = 5f;

    // The middle lane object (unused in the provided code)
    public GameObject middleLaneObject;

    // The maximum speed at which objects will spawn (initially 15 units per second)
    public float maxSpawnSpeed = 15f;

    // The time interval between spawn speed increases (initially 10 seconds)
    public float spawnSpeedIncreaseInterval = 10f;

    // The amount by which spawn speed increases when spawnSpeedIncreaseInterval is reached (initially 1 unit per second)
    public float spawnSpeedIncreaseAmount = 1f;

    // The minimum spawn interval (initially 0.1 seconds)
    public float minSpawnInterval = 0.1f;

    // The amount by which the spawn interval decreases when spawnSpeedIncreaseInterval is reached (initially 0.1 seconds)
    public float spawnIntervalDecreaseAmount = 0.1f;

    // Timer to track when to spawn a new object
    private float spawnTimer = 0f;

    private int currentLaneIndex = 0; // Track the current lane index

    // Timer to track when to increase spawn speed and decrease spawn interval
    private float elapsedSpawnSpeedIncreaseInterval = 0f;

    //Healthbar Object
    public HealthBar HealthBar;
 
    // Method to update the spawn speed-related fields
    // Method to update spawn speed-related fields
    public void UpdateSpawnSpeedFields(float interval, float increaseAmount, float increaseInterval)
    {
        spawnInterval = interval;
        spawnSpeedIncreaseAmount = increaseAmount;
        spawnSpeedIncreaseInterval = increaseInterval;
    }

    // Update is called once per frame
    private void Update()
    {
        // Increment the spawn timer by the time since the last frame
        spawnTimer += Time.deltaTime;

        // Increment the elapsed time for spawn speed increase
        elapsedSpawnSpeedIncreaseInterval += Time.deltaTime;

        //Getting current health
        float currentHealth = HealthBar.GetCurrentHealth();
        // Check if it's time to spawn a new object
        if (spawnTimer >= spawnInterval)
        {
            // Call the method to spawn a new object
            SpawnObject();

            // Reset the spawn timer
            spawnTimer = 0f;
        }

        // Check if it's time to increase spawn speed and decrease spawn interval
        if (elapsedSpawnSpeedIncreaseInterval >= spawnSpeedIncreaseInterval)
        {
            // Call the method to increase spawn speed
            IncreaseSpawnSpeed();

            // Call the method to decrease spawn interval
            DecreaseSpawnInterval();

            // Reset the elapsed time for spawn speed increase
            elapsedSpawnSpeedIncreaseInterval = 0f;
        }
    }

    // Method to spawn a new object
    // Method to spawn a new object
    private void SpawnObject()
    {
        // Array of lane positions (-1, 0, 1)
        int[] lanePositions = new int[] { -1, 0, 1 };

        // Get the lane position at the current index
        int lanePosition = lanePositions[currentLaneIndex];

        // Calculate the spawn position based on the lane position and lane offset
        float spawnX = transform.position.x + lanePosition * laneOffset;
        float spawnY = transform.position.y;

        // Increment the current lane index
        currentLaneIndex = (currentLaneIndex + 1) % lanePositions.Length;

        // Randomly choose an object from the prefab array based on the selected scene
        GameObject[] selectedPrefabs = null;
        int selectedScene = PlayerPrefs.GetInt("selectedScenarioName");

        if (selectedScene == 0)
        {
            // NTUC scene
            GameObject[] unhealthyFoodPrefabs = ntucUnhealthyFoodPrefabs;
            GameObject[] healthyFoodPrefabs = ntucHealthyFoodPrefabs;
            selectedPrefabs = new GameObject[unhealthyFoodPrefabs.Length + healthyFoodPrefabs.Length + nutrigrades.Length];
            unhealthyFoodPrefabs.CopyTo(selectedPrefabs, 0);
            healthyFoodPrefabs.CopyTo(selectedPrefabs, unhealthyFoodPrefabs.Length);
            nutrigrades.CopyTo(selectedPrefabs, unhealthyFoodPrefabs.Length + healthyFoodPrefabs.Length); // Add nutrigrades to the selected prefabs
        }
        else if (selectedScene == 1)
        {
            // Hawker scene
            // Hawker scene: Concatenate both healthy and unhealthy arrays
            GameObject[] unhealthyFoodPrefabs = hawkerUnhealthyFoodPrefabs;
            GameObject[] healthyFoodPrefabs = hawkerHealthyFoodPrefabs;
            selectedPrefabs = new GameObject[unhealthyFoodPrefabs.Length + healthyFoodPrefabs.Length + nutrigrades.Length];
            unhealthyFoodPrefabs.CopyTo(selectedPrefabs, 0);
            healthyFoodPrefabs.CopyTo(selectedPrefabs, unhealthyFoodPrefabs.Length);
            nutrigrades.CopyTo(selectedPrefabs, unhealthyFoodPrefabs.Length + healthyFoodPrefabs.Length);
        }

        // Randomly choose an object from the selected prefab array
        int randomIndex = UnityEngine.Random.Range(0, selectedPrefabs.Length);

        // Instantiate the selected object at the calculated spawn position
        GameObject newObject = Instantiate(selectedPrefabs[randomIndex], new Vector3(spawnX, spawnY, 0f), Quaternion.identity);

        // Add a Rigidbody2D component to the object if it doesn't have one
        if (!newObject.GetComponent<Rigidbody2D>())
            newObject.AddComponent<Rigidbody2D>();

        // Set the scale of the object
        float height = UnityEngine.Random.Range(minHeight, maxHeight);
        newObject.transform.localScale = new Vector3(laneWidth, height, 1f);

        // Check if the spawned object is a nutrigrade prefab
        if (Array.IndexOf(nutrigrades, selectedPrefabs[randomIndex]) != -1)
        {
            // Apply different behavior to nutrigrades
            // For example, you can set their velocity to move upwards instead of downwards
            newObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * constantNutrigradeSpeed;
        }
    
        else
        {
            // Set the velocity of non-nutrigrade objects to move straight downwards
            newObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * spawnSpeed;
        }
    }


    // Method to increase the spawn speed
    private void IncreaseSpawnSpeed()
    {
        // Increase the spawn speed by the specified amount
        spawnSpeed += spawnSpeedIncreaseAmount;

        // Cap the spawn speed at the maximum value
        if (spawnSpeed > maxSpawnSpeed)
        {
            spawnSpeed = maxSpawnSpeed;
        }
    }

    // Method to decrease the spawn interval
    private void DecreaseSpawnInterval()
    {
        // Decrease the spawn interval by the specified amount
        spawnInterval -= spawnIntervalDecreaseAmount;

        // Cap the spawn interval at the minimum value
        if (spawnInterval < minSpawnInterval)
        {
            spawnInterval = minSpawnInterval;
        }
    }
}
