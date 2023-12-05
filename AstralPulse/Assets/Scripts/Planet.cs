using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public float gravityRadius;
    public float orbitRadius;
    public float orbitSpeed;
    public float gravityStrength;

    // Pull on all pullable objects
        // Start pulling objects when they reach gravity radius
        // Lock objects when they reach lock radius
        // Smooth transition from free to locked
        // Locked objects will orbit until pushed off

    // Implment IOrbital
        // Pull
        // Lock
        // Unlock


    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, gravityRadius);
        Collider2D[] lockedColliders = Physics2D.OverlapCircleAll(transform.position, orbitRadius);
        foreach (Collider2D collider in colliders)
        {
            // if (collider in lockedColliders) {return;}
            if (Vector3.Distance(transform.position, collider.transform.position) < orbitRadius) continue;
            IAttractable attractableObject = collider.gameObject.GetComponent<IAttractable>();
            if (attractableObject != null)
            {
                // Calculate distance and direction
                Vector3 direction = collider.transform.position - transform.position;
                float distance = direction.magnitude;

                // Ensure distance is not zero to avoid division by zero
                if (distance > 0.0f)
                {
                    // Adjust gravityStrength based on distance (inverse square law)
                    float adjustedGravityStrength = gravityStrength / (distance * distance);

                    // Apply the adjusted gravity
                    attractableObject.Attract(-direction, adjustedGravityStrength);
                }
            }
        }

        // colliders = Physics2D.OverlapCircleAll(transform.position, orbitRadius);
        foreach (Collider2D lockedCollider in lockedColliders)
        {
            IAttractable attractableObject = lockedCollider.gameObject.GetComponent<IAttractable>();
            if (attractableObject != null)
            {
                // Lock Attractable in orbit around object
                attractableObject.Lock(transform, orbitRadius, orbitSpeed);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw a gizmo circle to visualize the gravity radius
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, gravityRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, orbitRadius);
    }
}
