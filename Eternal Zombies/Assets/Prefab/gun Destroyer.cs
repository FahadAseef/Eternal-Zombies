using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunDestroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with the gun!");
            // Destroy the current gun
            Destroy(gameObject);
        }
    }
}
