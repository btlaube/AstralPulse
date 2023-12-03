using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityStrength = 5f;
    public float orbitSpeed;
    public float orbitRadius = 10f; // Set your desired orbit radius
    public float gravityRadius = 5f;

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, gravityRadius);
        foreach (Collider2D collider in colliders)
        {
            IAttractable attractableObject = collider.gameObject.GetComponent<IAttractable>();
            if (attractableObject != null)
            {
                Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
                Vector2 targetVelocity = new Vector2(0.0f, 0.0f);

                // Move the object toward planet
                Vector3 direction = collider.transform.position - transform.position;
                direction.Normalize();
                rb.AddForce(-direction * gravityStrength);

                // Move the object in the direction of the orbit
                direction = new Vector3(-direction.y, direction.x, direction.z);
                rb.AddForce(direction * orbitSpeed);



                // rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime);
                

                


            }
        }
    }

    // Function to translate a point on a circle
    Vector2 TranslatePointOnCircle(Vector2 initialPoint, float radius, float arcLength)
    {
        // Convert Cartesian to Polar coordinates
        float angle = Mathf.Atan2(initialPoint.y, initialPoint.x);
        float r = Mathf.Sqrt((initialPoint.x * initialPoint.x) + (initialPoint.y * initialPoint.y));

        // Update angle for arc length
        float newAngle = angle + (arcLength / radius);

        // Convert back to Cartesian coordinates
        float xNew = radius * Mathf.Cos(newAngle);
        float yNew = radius * Mathf.Sin(newAngle);

        return new Vector2(xNew, yNew);
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
