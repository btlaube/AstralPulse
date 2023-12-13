using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public float gravityRadius;
    public float orbitRadius;
    public float orbitSpeed;
    public float orbitSmoothing;
    public float gravityStrength;
    public float escapeOrbitBuffer;
    public float normalForce;

    void Update()
    {
        Collider2D[] gravityColliders = Physics2D.OverlapCircleAll(transform.position, gravityRadius);
        foreach (Collider2D collider in gravityColliders)
        {
            PlayerRicochet pRicochet = collider.gameObject.GetComponent<PlayerRicochet>();
            if (pRicochet != null)
            {
                if (Vector3.Distance(transform.position, collider.transform.position) <= orbitRadius)
                {
                    continue;
                }
                if (Vector3.Distance(transform.position, collider.transform.position) > orbitRadius + escapeOrbitBuffer && pRicochet.isLocked)
                {
                    Debug.Log(Vector3.Distance(transform.position, collider.transform.position));
                    pRicochet.Unlock();
                }
                // collider is within gravity radius and outside of orbit Radius
                // attract collider
                Vector3 direction = transform.position - collider.transform.position;
                direction.Normalize();
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * gravityStrength, ForceMode2D.Force);

                // apply normal force
                Vector3 normal = new Vector3(-direction.y, direction.x, 0.0f);
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(normal * normalForce, ForceMode2D.Force);
                Debug.Log("Applied forces");
            }
        }

        Collider2D[] orbitingColliders = Physics2D.OverlapCircleAll(transform.position, orbitRadius);
        foreach (Collider2D collider in orbitingColliders)
        {
            PlayerRicochet pRicochet = collider.gameObject.GetComponent<PlayerRicochet>();
            if (pRicochet != null)
            {
                if (pRicochet.isLocked)
                {
                    pRicochet.orbitAngle += orbitSpeed / 1000.0f;
                    Vector3 targetPos = CalculatePointOnCircle(pRicochet.orbitAngle);
                    collider.transform.position = targetPos;
                }
                else
                {
                    Vector2 direction = transform.position - collider.transform.position;
                    direction.Normalize();
                    pRicochet.orbitAngle = Mathf.Atan2(-direction.y, -direction.x);

                    if (pRicochet.isLockable) pRicochet.Lock(transform);
                }
            }
        }
    }

    // Function to calculate the position on the circle based on the angle
    Vector3 CalculatePointOnCircle(float angle)
    {
        float x = transform.position.x + orbitRadius * Mathf.Cos(angle);
        float y = transform.position.y + orbitRadius * Mathf.Sin(angle);

        // Assuming the circle is in the XY plane
        return new Vector3(x, y, 0f);
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
