using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrbit : MonoBehaviour
{
    public float rotationSpeed = 5f; // Adjust the rotation speed as needed

    void Update()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");

        // Rotate the player object around the central point
        RotatePlayer(horizontalInput);
    }

    void RotatePlayer(float input)
    {
        // Calculate the rotation angle based on input
        float rotationAngle = input * rotationSpeed * Time.deltaTime;

        // Apply rotation around the central point (0, 0, 0)
        transform.RotateAround(Vector3.zero, Vector3.up, rotationAngle);
    }
}
