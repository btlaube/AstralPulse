using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    public float gravityRadius;
    public float orbitRadius;
    public float orbitSpeed;
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

                if (Vector3.Distance(transform.position, collider.transform.position) > orbitRadius + escapeOrbitBuffer && pRicochet.isLocked)
                {
                    Debug.Log(Vector3.Distance(transform.position, collider.transform.position));
                    pRicochet.Unlock();
                }

                // Move toward the orbit radius
                Vector3 direction = transform.position - collider.transform.position;
                direction.Normalize();

                Vector3 normal = new Vector3(-direction.y, direction.x, 0.0f) * normalForce;

                Vector2 result = direction + normal;

                Vector2 force = result * gravityStrength;
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Force);
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
                    Vector3 distanceVelocity = Vector3.zero;

                    pRicochet.orbitAngle = (pRicochet.orbitAngle + Time.deltaTime * orbitSpeed) % (2f * Mathf.PI);
                    Vector3 orbitDirection = new Vector3(Mathf.Cos(pRicochet.orbitAngle), Mathf.Sin(pRicochet.orbitAngle), 0.0f);
                    Vector3 targetPos = transform.position + (orbitDirection * orbitRadius);
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

    void OnDrawGizmos()
    {
        // Draw a gizmo circle to visualize the gravity radius
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, gravityRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, orbitRadius);
    }
}
