using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] fruitPrefabs;     // Array to hold different fruit prefabs
    public float spawnInterval = 1.5f;    // Time interval between spawns
    public float spawnY = -5f;            // Y-position for fruit spawn (bottom of the screen)
    public float spawnXMin = -8f;         // Minimum x-coordinate for fruit spawn
    public float spawnXMax = 8f;          // Maximum x-coordinate for fruit spawn
    public float launchForceMin = 8f;     // Minimum launch force
    public float launchForceMax = 12f;    // Maximum launch force

    private void Start()
    {
        StartCoroutine(SpawnFruits());
    }

    private IEnumerator SpawnFruits()
    {
        while (true)
        {
            // Wait for the specified interval before spawning the next fruit
            yield return new WaitForSeconds(spawnInterval);

            // Choose a random fruit prefab
            int randomIndex = Random.Range(0, fruitPrefabs.Length);
            GameObject fruitPrefab = fruitPrefabs[randomIndex];

            // Choose a random x position within the specified range
            float spawnX = Random.Range(spawnXMin, spawnXMax);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

            // Instantiate the fruit at the chosen position
            GameObject spawnedFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);

            // Get the Rigidbody2D component to apply physics
            Rigidbody2D rb = spawnedFruit.GetComponent<Rigidbody2D>();

            // Apply an upward force with a randomized strength
            if (rb != null)
            {
                float launchForce = Random.Range(launchForceMin, launchForceMax);
                rb.AddForce(new Vector2(0, launchForce), ForceMode2D.Impulse);
            }
        }
    }
}