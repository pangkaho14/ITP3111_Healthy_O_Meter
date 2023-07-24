using System;
using System.Collections.Generic;
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
    private List<GameObject> spawnedFoodObjects = new List<GameObject>();
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
    public float spawnSpeedIncreaseAmount = 1.3f;

    // The minimum spawn interval (initially 0.1 seconds)
    public float minSpawnInterval = 0.1f;

    // The amount by which the spawn interval decreases when spawnSpeedIncreaseInterval is reached (initially 0.1 seconds)
    public float spawnIntervalDecreaseAmount = 0.1f;

    // Timer to track when to spawn a new object
    private float spawnTimer = 0f;

    private int currentLaneIndex = 0; // Track the current lane index

    // Timer to track when to increase spawn speed and decrease spawn interval
    private float elapsedSpawnSpeedIncreaseInterval = 0f;

    // PlayerHealthPoints scriptable object
    public PlayerHealthPoints playerHP;
    private float constantSpeed = 6f;


    private void Update()
    {
        // Increment the spawn timer by the time since the last frame
        spawnTimer += Time.deltaTime;

        // Increment the elapsed time for spawn speed increase
        elapsedSpawnSpeedIncreaseInterval += Time.deltaTime;

        // Getting current health
        float currentHealth = playerHP.GetCurrentHealth();

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
            // Call the method to increase spawn speed and decrease spawn interval
            IncreaseSpawnSpeedAndDecreaseSpawnInterval();

            // Reset the elapsed time for spawn speed increase
            elapsedSpawnSpeedIncreaseInterval = 0f;
        }

        // Check if the spawned objects are inside the camera's view and adjust their speed accordingly
        foreach (GameObject foodObject in spawnedFoodObjects)
        {
            if (foodObject != null)
            {
                // Check if the object is visible to the camera
                Vector3 viewportPosition = Camera.main.WorldToViewportPoint(foodObject.transform.position);
                bool isVisible = (viewportPosition.x >= 0f && viewportPosition.x <= 1f && viewportPosition.y >= 0f && viewportPosition.y <= 1f);

                // Adjust the speed based on visibility
                Rigidbody2D rb2D = foodObject.GetComponent<Rigidbody2D>();
                if (rb2D != null)
                {
                    // If the object is visible, set its velocity based on the current spawn speed
                    if (isVisible)
                    {
                        rb2D.velocity = Vector2.down * spawnSpeed;
                    }
                    // If the object is not visible, set its velocity to the updated constant speed
                    else
                    {
                        rb2D.velocity = Vector2.down * constantSpeed;
                    }
                }
            }
        }
    }

    // Method to increase the spawn speed and decrease the spawn interval
    private void IncreaseSpawnSpeedAndDecreaseSpawnInterval()
    {
        // Increase the spawn speed by the specified amount
        spawnSpeed += spawnSpeedIncreaseAmount;

        // Decrease the spawn interval by the specified amount
        spawnInterval -= spawnIntervalDecreaseAmount;

        // Cap the spawn speed at the maximum value
        if (spawnSpeed > maxSpawnSpeed)
        {
            spawnSpeed = maxSpawnSpeed;
        }

        // Cap the spawn interval at the minimum value
        if (spawnInterval < minSpawnInterval)
        {
            spawnInterval = minSpawnInterval;
        }
    }
    // Method to update the spawn speed-related fields
    // Method to update spawn speed-related fields
    public void UpdateSpawnSpeedFields(float interval, float increaseAmount, float increaseInterval)
    {
        spawnInterval = interval;
        spawnSpeedIncreaseAmount = increaseAmount;
        spawnSpeedIncreaseInterval = increaseInterval;
    }

    // Method to spawn a new object
    public void SpawnObject()
    {
        // Array of lane positions (-1, 0, 1)
        int[] lanePositions = new int[] { -1, 0, 1 };
        // Adjust the initial height based on the current spawn speed
        float initialHeight = (maxHeight - minHeight) * (spawnSpeed / maxSpawnSpeed) + minHeight;
        // Get the number of objects to spawn (1, 2, or 3)
        int objectsToSpawn = UnityEngine.Random.Range(1, 4);
        // Declare and initialize the selectedPrefabs array at the beginning of the function
        GameObject[] selectedPrefabs = new GameObject[0];
        // Check the selectedScene to initialize the selectedPrefabs array
        int selectedScene = PlayerPrefs.GetInt("selectedScenarioName");
        if (selectedScene == 0)
        {
            // NTUC scene
            GameObject[] unhealthyFoodPrefabs = ntucUnhealthyFoodPrefabs;
            GameObject[] healthyFoodPrefabs = ntucHealthyFoodPrefabs;
            GameObject[] nutrigradesPrefabs = nutrigrades;

            // Combine arrays for NTUC scene
            selectedPrefabs = new GameObject[unhealthyFoodPrefabs.Length + healthyFoodPrefabs.Length + nutrigradesPrefabs.Length];
            unhealthyFoodPrefabs.CopyTo(selectedPrefabs, 0);
            healthyFoodPrefabs.CopyTo(selectedPrefabs, unhealthyFoodPrefabs.Length);
            nutrigradesPrefabs.CopyTo(selectedPrefabs, unhealthyFoodPrefabs.Length + healthyFoodPrefabs.Length);
        }
        else if (selectedScene == 1)
        {
            // Hawker scene
            GameObject[] unhealthyFoodPrefabs = hawkerUnhealthyFoodPrefabs;
            GameObject[] healthyFoodPrefabs = hawkerHealthyFoodPrefabs;
            GameObject[] nutrigradesPrefabs = nutrigrades;

            // Combine arrays for Hawker scene
            selectedPrefabs = new GameObject[unhealthyFoodPrefabs.Length + healthyFoodPrefabs.Length + nutrigradesPrefabs.Length];
            unhealthyFoodPrefabs.CopyTo(selectedPrefabs, 0);
            healthyFoodPrefabs.CopyTo(selectedPrefabs, unhealthyFoodPrefabs.Length);
            nutrigradesPrefabs.CopyTo(selectedPrefabs, unhealthyFoodPrefabs.Length + healthyFoodPrefabs.Length);
        }
        // Check if it's 2 objects and ensure they are in adjacent lanes (1 & 2 or 2 & 3)
        if (objectsToSpawn == 2)
        {
            // Spawn two objects in adjacent lanes (1 & 2 or 2 & 3)
            int randomIndex = UnityEngine.Random.Range(0, 2); // 0 or 1
            int lanePosition1 = lanePositions[(currentLaneIndex + randomIndex) % lanePositions.Length];
            int lanePosition2 = lanePositions[(currentLaneIndex + randomIndex + 1) % lanePositions.Length];

            // Calculate the spawn positions for the two objects
            float spawnX1 = transform.position.x + lanePosition1 * laneOffset;
            float spawnX2 = transform.position.x + lanePosition2 * laneOffset;
            float spawnY = transform.position.y + initialHeight;

            // Select one healthy and one unhealthy food prefab
            GameObject healthyPrefab = null;
            GameObject unhealthyPrefab = null;


            if (selectedScene == 0)
            {
                // NTUC scene

                healthyPrefab = ntucHealthyFoodPrefabs[UnityEngine.Random.Range(0, ntucHealthyFoodPrefabs.Length)];
                unhealthyPrefab = ntucUnhealthyFoodPrefabs[UnityEngine.Random.Range(0, ntucUnhealthyFoodPrefabs.Length)];

            }
            else if (selectedScene == 1)
            {
                // Hawker scene

                healthyPrefab = hawkerHealthyFoodPrefabs[UnityEngine.Random.Range(0, hawkerHealthyFoodPrefabs.Length)];
                unhealthyPrefab = hawkerUnhealthyFoodPrefabs[UnityEngine.Random.Range(0, hawkerUnhealthyFoodPrefabs.Length)];
            }

            // Instantiate the selected healthy and unhealthy objects at the calculated spawn positions
            GameObject healthyObject = Instantiate(healthyPrefab, new Vector3(spawnX1, spawnY, 0f), Quaternion.identity);
            GameObject unhealthyObject = Instantiate(unhealthyPrefab, new Vector3(spawnX2, spawnY, 0f), Quaternion.identity);

            // Set the scale of the objects
            float height1 = UnityEngine.Random.Range(minHeight, maxHeight);
            float height2 = UnityEngine.Random.Range(minHeight, maxHeight);
            healthyObject.transform.localScale = new Vector3(laneWidth, height1, 1f);
            unhealthyObject.transform.localScale = new Vector3(laneWidth, height2, 1f);

            // Set the velocity of non-nutrigrade objects to move straight downwards
            healthyObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * spawnSpeed;
            unhealthyObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * spawnSpeed;

            // Add the spawned objects to the list
            spawnedFoodObjects.Add(healthyObject);
            spawnedFoodObjects.Add(unhealthyObject);

            // Increment the current lane index
            currentLaneIndex = (currentLaneIndex + 1) % lanePositions.Length;
        }
        else if (objectsToSpawn == 3)
        {
            // Spawn three objects
            int lanePosition1 = lanePositions[currentLaneIndex];
            int lanePosition2 = lanePositions[(currentLaneIndex + 1) % lanePositions.Length];
            int lanePosition3 = lanePositions[(currentLaneIndex + 2) % lanePositions.Length];

            // Calculate the spawn positions for the three objects
            float spawnX1 = transform.position.x + lanePosition1 * laneOffset;
            float spawnX2 = transform.position.x + lanePosition2 * laneOffset;
            float spawnX3 = transform.position.x + lanePosition3 * laneOffset;
            float spawnY = transform.position.y + initialHeight;
            // Select two unhealthy food prefabs and one healthy food prefab
            GameObject healthyPrefab = null;
            GameObject[] unhealthyPrefabs = null;


            if (selectedScene == 0)
            {
                // NTUC scene
                healthyPrefab = ntucHealthyFoodPrefabs[UnityEngine.Random.Range(0, ntucHealthyFoodPrefabs.Length)];
                unhealthyPrefabs = new GameObject[2];
                unhealthyPrefabs[0] = ntucUnhealthyFoodPrefabs[UnityEngine.Random.Range(0, ntucUnhealthyFoodPrefabs.Length)];
                unhealthyPrefabs[1] = ntucUnhealthyFoodPrefabs[UnityEngine.Random.Range(0, ntucUnhealthyFoodPrefabs.Length)];
            }
            else if (selectedScene == 1)
            {
                // Hawker scene
                healthyPrefab = hawkerHealthyFoodPrefabs[UnityEngine.Random.Range(0, hawkerHealthyFoodPrefabs.Length)];
                unhealthyPrefabs = new GameObject[2];
                unhealthyPrefabs[0] = hawkerUnhealthyFoodPrefabs[UnityEngine.Random.Range(0, hawkerUnhealthyFoodPrefabs.Length)];
                unhealthyPrefabs[1] = hawkerUnhealthyFoodPrefabs[UnityEngine.Random.Range(0, hawkerUnhealthyFoodPrefabs.Length)];
            }

            // Instantiate the selected healthy and unhealthy objects at the calculated spawn positions
            GameObject healthyObject = Instantiate(healthyPrefab, new Vector3(spawnX1, spawnY, 0f), Quaternion.identity);
            GameObject unhealthyObject1 = Instantiate(unhealthyPrefabs[0], new Vector3(spawnX2, spawnY, 0f), Quaternion.identity);
            GameObject unhealthyObject2 = Instantiate(unhealthyPrefabs[1], new Vector3(spawnX3, spawnY, 0f), Quaternion.identity);

            // Set the scale of the objects
            float height1 = UnityEngine.Random.Range(minHeight, maxHeight);
            float height2 = UnityEngine.Random.Range(minHeight, maxHeight);
            float height3 = UnityEngine.Random.Range(minHeight, maxHeight);
            healthyObject.transform.localScale = new Vector3(laneWidth, height1, 1f);
            unhealthyObject1.transform.localScale = new Vector3(laneWidth, height2, 1f);
            unhealthyObject2.transform.localScale = new Vector3(laneWidth, height3, 1f);

            // Set the velocity of non-nutrigrade objects to move straight downwards
            healthyObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * spawnSpeed;
            unhealthyObject1.GetComponent<Rigidbody2D>().velocity = Vector2.down * spawnSpeed;
            unhealthyObject2.GetComponent<Rigidbody2D>().velocity = Vector2.down * spawnSpeed;

            // Add the spawned objects to the list
            spawnedFoodObjects.Add(healthyObject);
            spawnedFoodObjects.Add(unhealthyObject1);
            spawnedFoodObjects.Add(unhealthyObject2);

            // Increment the current lane index
            currentLaneIndex = (currentLaneIndex + 1) % lanePositions.Length;
        }
        else
        {
            // Spawn a single object (same as the original code)
            // Randomly choose an object from the selected prefab array
            int randomIndex = UnityEngine.Random.Range(0, selectedPrefabs.Length);

            // Calculate the spawn position based on the lane position and lane offset
            float spawnX = transform.position.x + lanePositions[currentLaneIndex] * laneOffset;
            float spawnY = transform.position.y;

            // Instantiate the selected object at the calculated spawn position
            GameObject newObject = Instantiate(selectedPrefabs[randomIndex], new Vector3(spawnX, spawnY, 0f), Quaternion.identity);

            // Add the spawned object to the list
            spawnedFoodObjects.Add(newObject);

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

            // Increment the current lane index
            currentLaneIndex = (currentLaneIndex + 1) % lanePositions.Length;
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

    public void IncreaseSpawnInterval()
    {
        spawnInterval += 0.2f;
    }

    // Method to decrease the spawn interval
    public void DecreaseSpawnInterval()
    {
        // Decrease the spawn interval by the specified amount
        spawnInterval -= spawnIntervalDecreaseAmount;

        // Cap the spawn interval at the minimum value
        if (spawnInterval < minSpawnInterval)
        {
            spawnInterval = minSpawnInterval;
        }
    }

    public void DestroyCurrentFoodSpawns()
    {
        foreach (GameObject foodObject in spawnedFoodObjects)
        {
            Destroy(foodObject);
        }
        spawnedFoodObjects.Clear();
    }
}

