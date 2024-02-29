using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Health : MonoBehaviour
{
    public float maxHealth = 50f;
    private float currentHealth;

    private bool canDamage = true; // Flag to check if damage can be dealt
    private float damageCooldown = 3f; // Adjust the cooldown duration as needed

    private health_UI_Manager health_ui_manager;

    [SerializeField] private GameObject childObject; // Reference to the child GameObject you want to toggle

    private GameObject playerObject;
    //private GameObject childObject; // Reference to the child GameObject you want to toggle
    private bool isFlickering = false;

    [SerializeField] private Animator playerAnimator; // Reference to the Animator component

    [SerializeField] ParticleSystem deathParticles; // Reference to the particle system

    private Character_Movement_Manager movementManager; // Reference to the Character_Movement_Manager script

    public GameObject deathScreenUI;

    void Start()
    {
        currentHealth = maxHealth;
        // Find the GameObject with the health_UI_Manager script
        health_ui_manager = FindObjectOfType<health_UI_Manager>();

        playerObject = gameObject;
        //childObject = playerObject.transform.Find("Mesh").gameObject; // Replace "YourChildObjectName" with the actual name of your child object

        // Find the GameObject with the Character_Movement_Manager script
        movementManager = FindObjectOfType<Character_Movement_Manager>();

    }

    private void Update()
    {
        // Update the cooldown timer
        if (!canDamage)
        {
            damageCooldown -= Time.deltaTime;

            // Reset the cooldown flag when the timer reaches zero
            if (damageCooldown <= 0f)
            {
                canDamage = true;
                damageCooldown = 3f; // Reset the cooldown duration
                StopFlickering();
            }
        }
    }

    // Function to reduce player health
    public void TakeDamage(float damage)
    {
        if (currentHealth > 0f)
        {

            currentHealth -= damage;

            // Optional: Add visual/audio effects for taking damage

            Debug.Log("Player health: " + currentHealth);

            if (currentHealth <= 0f)
            {
                currentHealth = 0f; // Ensure health doesn't go below zero
                Die(); // Player is defeated
            }
            else
            {
                // If player can take damage, start flickering
                if (canDamage)
                {
                    canDamage = false;
                    StartFlickering();
                }
            }
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0f;
    }

    public void Die()
    {
        // Optional: Add death-related actions (e.g., play death animation, particle effects)
        Debug.Log("Player defeated!");
        // Handle the player's death, e.g., respawn or game over
        playerAnimator.SetTrigger("die"); // Trigger the "Death" animation in the Animator
        // Play the death particle effect
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
        movementManager.enabledJoystickInput = false;
        movementManager.enabledMovement = false;
        deathScreenUI.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with Zombie");
        // Check if the collision is with the zombie
        if (other.gameObject.CompareTag("enemy") &&canDamage)
        {
            // Call TakeDamage function
            TakeDamage(10f); // Adjust the damage amount as needed
            //Call health decrease function
            health_ui_manager.healthDecrease();
        }
    }

    void StartFlickering()
    {
        if (!isFlickering)
        {
            InvokeRepeating("ToggleVisibility", 0f, 0.1f); // Adjust the interval as needed
            isFlickering = true;
        }
    }

    void ToggleVisibility()
    {
        childObject.SetActive(!childObject.activeSelf); // Toggle visibility of the child object       
    }

    void StopFlickering()
    {
        CancelInvoke("ToggleVisibility");
        childObject.SetActive(true); // Ensure the child object is visible when flickering ends
        isFlickering = false;
        //canDamage = true;
    }


}
