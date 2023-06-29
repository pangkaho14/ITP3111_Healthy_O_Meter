using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> foodPrefabs = new();

    // Start is called before the first frame update
    void Start()
    {
        // Wait for 3 seconds then trigger My Function every 0.5 seconds.
        InvokeRepeating(nameof(SpawnFood), 3, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void SpawnFood()
    {
        for (var i = 0; i < foodPrefabs.Count; i++)
        {
            // 50% chance of spawning food every 5 seconds
            if (Random.Range(0, 100) < 50)
            {
                float offset = Random.Range(-2f, 2f);
                Instantiate(foodPrefabs[i], new Vector2(transform.position.x + offset, transform.position.y), Quaternion.identity);
            }
        }
    }
}
