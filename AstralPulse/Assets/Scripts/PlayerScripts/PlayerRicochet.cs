using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRicochet : MonoBehaviour, IPushable, IAttractable
{
    public float speed = 5.0f;
    public float smoothness = 5.0f;
    
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 direction, float power)
    {
        Debug.Log("Pushed");
        rb.velocity = direction.normalized * power;
    }

    public void Attract(Vector2 direction, float power)
    {
        // Apply the gravitational force to move the player
        // Vector2 targetVelocity = direction * power;
        // rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * smoothness);

        // rb.velocity = direction.normalized * power;
    }
}
