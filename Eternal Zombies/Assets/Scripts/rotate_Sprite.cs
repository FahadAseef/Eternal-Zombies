using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_Sprite : MonoBehaviour
{
    public float rotationSpeed = 50f; // Adjust the rotation speed as needed

    void Update()
    {
        // Rotate the object around the Z-axis
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

    }
}
