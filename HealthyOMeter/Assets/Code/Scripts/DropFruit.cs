using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // public GameObject objectPrefab;    // The prefab of the object you want to spawn

    public GameObject[] objectPrefab;  // Create an array of prefab of objects you want to spawn
    public float spawnInterval = 1f;   // The time interval between spawns
    public float spawnSpeed = 5f;      // The speed at which the object moves
    public float laneOffset = 3f;      // The offset between each lane
    public float laneWidth = 5f;       // The width of each lane
    public float minHeight = 1f;       // The minimum height of the spawned objects
    public float maxHeight = 5f;       // The maximum height of the spawned objects

    public GameObject middleLaneObject;  // The middle lane object

    private float spawnTimer = 0f;

    private void Update()
    {
        // Increment the spawn timer
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn a new object
        if (spawnTimer >= spawnInterval)
        {
            SpawnObject();
            spawnTimer = 0f; // Reset the spawn timer
        }
    }
    private void SpawnObject()
    {
        // Calculate the spawn position
        float lanePosition = Random.Range(-1, 2); // Randomly choose a lane (-1, 0, or 1)
        float spawnX = transform.position.x + lanePosition * laneOffset;
        float spawnY = transform.position.y;

        // // Instantiate a new object from the prefab
        // GameObject newObject = Instantiate(objectPrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);

        // Create random index fo the different objects
        int randomIndex = Random.Range(0, objectPrefab.Length);

        // Instatiate different objects from the prefab
        GameObject newObject = Instantiate(objectPrefab[randomIndex], new Vector3(spawnX, spawnY, 0f), Quaternion.identity);

        // Add a rigidbody component to the object if it doesn't have one
        if (!newObject.GetComponent<Rigidbody2D>())
            newObject.AddComponent<Rigidbody2D>();

        // Set the scale of the object
        float height = Random.Range(minHeight, maxHeight);
        newObject.transform.localScale = new Vector3(laneWidth, height, 1f);

        // Set the velocity of the object to move straight downwards
        newObject.GetComponent<Rigidbody2D>().velocity = Vector2.down * spawnSpeed;
    }

}
