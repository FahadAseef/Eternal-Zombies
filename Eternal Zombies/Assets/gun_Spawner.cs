using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_Spawner : MonoBehaviour
{
    public GameObject[] gunPrefabs; // Array of gun prefabs to spawn
    public float spawnInterval = 30f; // Interval for spawning a new gun
    public float spawnRadius = 50f; // Radius for randomizing spawn positions
    private float spawnTimer = 0f; // Timer for gun spawning
    private int currentGunIndex = 0; // Index of the current gun to spawn

    void Update()
    {
        // Update the spawn timer
        spawnTimer += Time.deltaTime;

        // Check if enough time has passed to spawn a new gun
        if (spawnTimer >= spawnInterval)
        {
            // Spawn the next gun in the order
            SpawnNextGun();
            // Reset the spawn timer
            spawnTimer = 0f;
        }
    }

    void SpawnNextGun()
    {
        // Instantiate the next gun prefab in the array
        GameObject nextGunPrefab = gunPrefabs[currentGunIndex];
        // Randomize spawn position within the specified radius
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        // Ensure the spawn position stays at ground level
        spawnPosition.y = 0f;
        Instantiate(nextGunPrefab, spawnPosition, Quaternion.identity);

        // Move to the next gun in the array
        currentGunIndex++;

        // If the index exceeds the length of the array, reset it to 0
        if (currentGunIndex >= gunPrefabs.Length)
        {
            currentGunIndex = 0;
        }
    }

}
