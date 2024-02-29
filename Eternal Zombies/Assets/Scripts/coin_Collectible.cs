using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class coin_Collectible : MonoBehaviour
{
    public int coinValue = 1; // You can adjust the coin value as needed
    public float magnetSpeed = 5f; // Adjust the speed of the coin towards the player
    public float destroyDistance = 1f; // Adjust the distance at which the coin gets destroyed
    public float collectionRange = 3f; // Adjust the range for collecting the coin

    private Transform player;
    private Slider progressBar;

    //public int progressLevelNo=1;
    private health_UI_Manager health_manager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (progressBar == null)
        {
            progressBar = FindObjectOfType<Slider>(); // Make sure you have only one Slider in the scene
        }

        health_manager = FindObjectOfType<health_UI_Manager>();

    }


    private void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Check if the player is within the collection range before moving towards the player
            if (Vector3.Distance(transform.position, player.position) <= collectionRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, magnetSpeed * Time.deltaTime);

                // Check if the coin is close enough to the player to destroy it
                if (Vector3.Distance(transform.position, player.position) < destroyDistance)
                {
                    Collect();
                }
            }
        }
    }

    void Collect()
    {
        // You can implement any additional effects or logic here when the coin is collected
        Debug.Log("Coin Collected!");
        Destroy(gameObject); // Destroy the coin GameObject when collected
        
        // Update the progress bar
        progressBar.value += coinValue;


        if (progressBar.value >= progressBar.maxValue)
        {
            progressBar.value = progressBar.minValue;
            //progressLevelNo++;
            health_manager.levelNoShow();
        }

    }


}
