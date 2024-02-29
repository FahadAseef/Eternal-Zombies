using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Shooting : MonoBehaviour
{
    public float shootingRange = 20f;
    public Transform gunTransform;
    public LayerMask enemyLayer;
    public float timeBetweenShots = 1f; // Adjust the time between shots
    private float timer;

    private Character_Movement_Manager movementManager; // Reference to the movement manager script

    public float detectionRange = 20f; // Add this variable to control the detection range

    private player_Health healthScript;

    public GameObject bulletPrefab; // Prefab of the bullet object
    public Transform bulletSpawnPoint; // Transform representing the spawn point of the bullet
    public float bulletSpeed = 10f; // Adjust the bullet speed
    private Transform enemyTransform; // Add this variable to store the reference to the enemy transform
    //public ParticleSystem shootingEffect; // Particle system for shooting effect

    void Start()
    {
        // Attempt to get the Character_Movement_Manager component from the player GameObject
        movementManager = GetComponent<Character_Movement_Manager>();
        if (movementManager == null)
        {
            Debug.LogError("Character_Movement_Manager component not found.");
        }

        // Attempt to get the player_Health component from the player GameObject
        healthScript = GetComponent<player_Health>();
        if (healthScript == null)
        {
            Debug.LogError("player_Health component not found.");
        }
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if the player is alive before shooting
        if (healthScript != null && healthScript.IsAlive())
        {
            // Shoot at enemies continuously
            ShootAtEnemies();
        }
    }

    bool IsEnemyWithinShootingRange(Collider enemyCollider)
    {
        float distanceToEnemy = Vector3.Distance(transform.position, enemyCollider.transform.position);
        return distanceToEnemy <= shootingRange;
    }

    void ShootAtEnemies()
    {
        // Check if enough time has passed to shoot again
        if (timer >= timeBetweenShots)
        {
            // Find all enemies within detection range
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);

            // Sort enemies by distance (closest to farthest)
            System.Array.Sort(hitColliders, (x, y) => Vector3.Distance(transform.position, x.transform.position)
                                              .CompareTo(Vector3.Distance(transform.position, y.transform.position)));

            // Shoot at the first enemy in range (even if it's at the tip of the detection range)
            if (hitColliders.Length > 0)
            {
                enemyTransform = hitColliders[0].transform; // Store the reference to the enemy transform
                //ShootAtEnemy(hitColliders[0].transform);
                ShootAtEnemy();
            }

            // Reset the timer
            timer = 0f;
        }
    }

    //void ShootAtEnemy(Transform enemyTransform)
    void ShootAtEnemy() 
    { 
    
        // Calculate the direction to the enemy
        Vector3 direction = (enemyTransform.position - gunTransform.position).normalized;

        // Draw a debug ray to visualize the shooting direction
        Debug.DrawRay(gunTransform.position, direction * shootingRange, Color.red, 0.1f);

        // Play the shooting effect
        //PlayShootingEffect();

        // Instantiate a bullet object at the spawn point
        InstantiateBullet();

        // Check if the object hit has the ZombieAI script
        zombie_AI zombieAI = enemyTransform.GetComponent<zombie_AI>();

        if (zombieAI != null)
        {
            // Damage the zombie
            zombieAI.TakeDamage(10f); // Adjust the damage value

            // Update player's rotation based on shooting direction
            if (movementManager != null)
            {
                movementManager.RotatePlayerTowards(direction);
            }
        }
    }

    /*
    void PlayShootingEffect()
    {
        // Play the shooting particle system effect
        //shootingEffect.Play();
    }
    */

    
    void InstantiateBullet()
    {
        // Instantiate the bullet prefab at the bullet spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);


        // Get the bullet's rigidbody (assuming the bullet has a Rigidbody component)
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Check if the bullet has a Rigidbody component
        if (bulletRigidbody != null)
        {
            // Apply a force to the bullet to make it move in the calculated direction
            bulletRigidbody.AddForce((enemyTransform.position - bullet.transform.position).normalized * bulletSpeed, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Bullet prefab is missing Rigidbody component.");
        }

    }
    

    private void OnDrawGizmos()
    {
        // Draw a wire sphere in the editor to represent the detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
