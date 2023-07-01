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


public class ObjectSpawner : MonoBehaviour
{
    // The prefabs of the objects you want to spawn
    public GameObject[] objectPrefab;

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

    // Timer to track when to increase spawn speed and decrease spawn interval
    private float elapsedSpawnSpeedIncreaseInterval = 0f;

    // Update is called once per frame
    private void Update()
    {
        // Increment the spawn timer by the time since the last frame
        spawnTimer += Time.deltaTime;

        // Increment the elapsed time for spawn speed increase
        elapsedSpawnSpeedIncreaseInterval += Time.deltaTime;

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
    private void SpawnObject()
    {
        // Randomly choose a lane (-1, 0, or 1)
        float lanePosition = Random.Range(-1, 2);

        // Calculate the spawn position based on the lane position and lane offset
        float spawnX = transform.position.x + lanePosition * laneOffset;
        float spawnY = transform.position.y;

        // Randomly choose an object from the prefab array
        int randomIndex = Random.Range(0, objectPrefab.Length);

        // Instantiate the selected object at the calculated spawn position
        GameObject newObject = Instantiate(objectPrefab[randomIndex], new Vector3(spawnX, spawnY, 0f), Quaternion.identity);

        // Add a Rigidbody2D component to the object if it doesn't have one
        if (!newObject.GetComponent<Rigidbody2D>())
            newObject.AddComponent<Rigidbody2D>();

        // Set the scale of the object
        float height = Random.Range(minHeight, maxHeight);
        newObject.transform.localScale = new Vector3(laneWidth, height, 1f);

        // Set the velocity of the object to move straight downwards
        newObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * spawnSpeed;
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
