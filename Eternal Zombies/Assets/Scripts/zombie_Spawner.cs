using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_Spawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs; //Array of different zombie prefabs
    public float initialSpawnDelay = 2f;
    public float timeBetweenWaves = 10f;
    public Transform[] spawnPoints;

    private int waveCount = 0;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("StartWave", initialSpawnDelay, timeBetweenWaves);
    }

    void StartWave()
    {
        Debug.Log("start wave called");
        waveCount++;
        Debug.Log("Wave " + waveCount + " Started!");

        /*
        // Spawn zombies at random spawn points, excluding those near the player
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (Vector3.Distance(spawnPoints[i].position, player.position) > 20f) // Adjust the distance as needed
            {
                SpawnRandomZombie(spawnPoints[i]);
            }
        }
        */

        // Spawn zombies at random spawn points, excluding those near the player
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            float distanceToPlayer = Vector3.Distance(spawnPoints[i].position, player.position);
            Debug.Log("Distance to Player: " + distanceToPlayer);

            if (distanceToPlayer > 50f) // Adjust the distance as needed
            {
                Debug.Log("Spawning Zombie");
                SpawnRandomZombie(spawnPoints[i]);
            }
            else
            {
                Debug.Log("Skipping Zombie Spawn - Too Close to Player");
            }
        }
    }

    void SpawnRandomZombie(Transform spawnPoint)
    {
        if (zombiePrefabs.Length > 0)
        {
            // Choose a random zombie prefab from the array
            GameObject randomZombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
            // Check if the randomZombiePrefab is not null before instantiating
            if (randomZombiePrefab != null)
            {
                Debug.Log("SpawnZombie called");
                Instantiate(randomZombiePrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        else
        {
            Debug.LogWarning("No zombie prefabs assigned to the array!");
        }
    }
}
