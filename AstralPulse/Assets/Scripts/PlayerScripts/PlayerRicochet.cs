using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRicochet : MonoBehaviour, IPushable, IAttractable
{
    public float speed = 5.0f;
    public float smoothness = 5.0f;

    public bool isLocked;
    private Transform orbitParent;
    private float orbitRadius;
    
    private Rigidbody2D rb;
    public float orbitAngle;
    public float orbitSpeed;
    private Camera mainCamera;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    public void Push(Vector2 direction, float power)
    {
        Debug.Log("Pushed");
        rb.velocity = direction.normalized * power;
    }

    void FixedUpdate()
    {
        if (isLocked)
        {
            // Debug.Log(orbitAngle);

            orbitAngle += Time.deltaTime * orbitSpeed;

            Vector2 orbitPos = new Vector2(Mathf.Cos(orbitAngle), Mathf.Sin(orbitAngle)) * orbitRadius;
            Debug.Log($"Angle: {orbitAngle}, Pos: {orbitPos}");
            transform.position = orbitPos;//Vector2.Lerp(rb.velocity, orbitPos, Time.fixedDeltaTime * smoothness);
        }
    }

    public void Attract(Vector2 direction, float power)
    {
        Debug.Log("Attracted");
        Vector2 targetVelocity = direction * power;
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * smoothness);
    }

    public void Lock(Transform parent, float orbitRadius, float orbitSpeed)
    {
        if (isLocked) return;
        Debug.Log("Locked");
        this.orbitParent = parent;
        transform.parent = parent;
        this.orbitRadius = orbitRadius;
        this.orbitSpeed = orbitSpeed;
        isLocked = true;
    }
    public void Unlock()
    {
        if (!isLocked) return;
        Debug.Log("Unlocked");
        orbitParent = null;
        isLocked = false;
    }
}
