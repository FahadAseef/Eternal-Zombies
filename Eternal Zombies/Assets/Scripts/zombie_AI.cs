using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie_AI : MonoBehaviour
{
    public float detectionRange = 20f;
    public float movementSpeed = 3f;

    private Transform playerPosition;
    private NavMeshAgent navMeshAgent;
    private float maxHealth = 10f;
    private float currentHealth;

    [SerializeField] ParticleSystem deathParticles; // Reference to the particle system
    [SerializeField] GameObject coinPrefab; // Reference to the coin prefab



    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
        currentHealth = maxHealth;
    }

    void Update()
    {

        if (currentHealth > 0f)
        {
            if (playerPosition != null && Vector3.Distance(transform.position, playerPosition.position) <= detectionRange)
            {
                MoveTowardsPlayer();
            }
            else
            {
                CheckDestinationReached();
            }
        }

    }

    void MoveTowardsPlayer()
    {
        navMeshAgent.SetDestination(playerPosition.position);
    }

    void CheckDestinationReached()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();
        }
    }

    void SetRandomDestination()
    {
        Vector3 newPos = RandomNavMeshPosition();
        navMeshAgent.SetDestination(newPos);
    }

    // Generate a random position on the NavMesh
    Vector3 RandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 100f; // Adjust the multiplier based on your map size
        randomDirection += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, 100f, -1);

        return navHit.position;
    }

    // Method to take damage when hit by the player's bullets
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Optional: Add visual/audio effects for taking damage

        Debug.Log("Zombie health: " + currentHealth);

        if (currentHealth <= 0f)
        {
            Die(); // Zombie is defeated
        }
    }

    void Die()
    {
        // Play the death particle effect
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
        // Instantiate the coin when the zombie dies
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        // Optional: Add death-related actions (e.g., play death animation, particle effects)
        Debug.Log("Zombie defeated!");
        Destroy(gameObject); // Destroy the zombie GameObject
    }


}
