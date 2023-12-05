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
            IAttractable attractableObject = collider.gameObject.GetComponent<IAttractable>();
            if (attractableObject != null)
            {
                Vector3 direction = transform.position - collider.transform.position;
                direction.Normalize();

                attractableObject.Attract(direction , gravityStrength);
            }
        }

        foreach (Collider2D lockedCollider in lockedColliders)
        {
            IAttractable attractableObject = lockedCollider.gameObject.GetComponent<IAttractable>();
            if (attractableObject != null)
            {
                if (lockedCollider.gameObject.GetComponent<PlayerRicochet>().isLockable)
                {
                    attractableObject.Lock(transform, orbitRadius, orbitSpeed);
                }
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
