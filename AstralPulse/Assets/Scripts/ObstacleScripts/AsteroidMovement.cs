using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour, IPushable
{

    private Transform playerTransform;
    public Vector3 targetOffset = new Vector3(0f, 2f, 0f);
    public Vector2 offsetVariability;
    public float speed = 5f;
    public Vector2 speedOffset;

    private Rigidbody2D rb;

    void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Calculate the adjusted target position with the offset
        Vector3 adjustedTarget = playerTransform.position + GetTargetOffset();

        // Move the object towards the adjusted target position
        MoveTowards(adjustedTarget);
    }

    private void MoveTowards(Vector3 target)
    {
        // Calculate the direction towards the target
        Vector3 direction = (target - transform.position).normalized;

        // Move the object in the calculated direction
        rb.velocity = direction * GetRandomSpeed();
    }

    private float GetRandomSpeed()
    {
        return speed + Random.Range(speedOffset.x, speedOffset.y);
    }

    private Vector3 GetTargetOffset()
    {
        return new Vector3(targetOffset.x + Random.Range(offsetVariability.x, offsetVariability.y), targetOffset.y + Random.Range(offsetVariability.x, offsetVariability.y), targetOffset.z);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1.0f);
            GetComponent<ObstacleHealth>().TakeDamage(1.0f);
        }
        // Check if the collided object implements IDamageable interface
        IDamageable damageableObject = collision.gameObject.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            // The collided object implements the IDamageable interface
            damageableObject.TakeDamage(1.0f); // Example: Call a method from the interface
        }
    }

    public void Push(Vector2 direction, float power)
    {
        rb.velocity = direction.normalized * power;
        // Debug.Log($"Applied: power {power}, direction {direction}");
    }

}
