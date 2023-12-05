using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRicochet : MonoBehaviour, IAttractable, IPushable
{
    public float speed = 5.0f;
    public float smoothness = 5.0f;
    public bool isLocked;
    public Transform orbitParent;
    public float orbitRadius;
    public float orbitAngle;
    public float orbitSpeed;
    public float orbitAngularVelocity;
    public Vector3 orbitPos;

    [SerializeField] private float lockSmoothness;

    private Rigidbody2D rb;

    // Lockable for escape orbit
    public float unlockDelay;
    public bool isLockable;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        isLockable = true;
    }

    void Update()
    {
        if (isLocked)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Unlock();
            }
        }
    }

    void FixedUpdate()
    {
        if (isLocked)
        {
            // Smoothing variables for distance towards orbitParent
            Vector3 distanceVelocity = Vector3.zero;
            float distanceSmoothness = 0.1f; // Adjust this for a smooth transition towards orbitParent

            orbitAngle = (orbitAngle + Time.deltaTime * orbitSpeed) % (2f * Mathf.PI);
            // Debug.Log(orbitAngle);

            Vector3 orbitDirection = new Vector3(Mathf.Cos(orbitAngle), Mathf.Sin(orbitAngle), 0.0f);
            Vector3 orbitPos = orbitParent.position + orbitDirection * orbitRadius;

            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(orbitPos.x, orbitPos.y, transform.position.x), ref distanceVelocity, lockSmoothness);
        }
    }

    public void Attract(Vector2 direction, float power)
    {
        Debug.Log("Attracted");
        Vector2 gravity = direction * power;

        rb.AddForce(gravity, ForceMode2D.Force);
    }

    public void Lock(Transform parent, float orbitRadius, float orbitSpeed)
    {
        
        if(isLocked) return;
        Debug.Log("Locked");
        // this.transform.SetParent(parent);
        this.orbitRadius = orbitRadius;
        this.orbitSpeed = orbitSpeed;
        this.orbitParent = parent;

        isLocked = true;
        // Get Orbit Pos
        Vector2 direction = orbitParent.position - transform.position;
        
        direction.Normalize();
        Debug.Log(direction);
        orbitPos = direction * orbitRadius;
        // Calculate the angle in radians using Atan2
        orbitAngle = Mathf.Atan2(-direction.y, -direction.x);
        Debug.Log(orbitAngle);
    }

    public void Unlock()
    {
        if (!isLocked) return;

        // Apply ricochet
        Vector3 direction = transform.position - orbitParent.position;
        direction.Normalize();
        StartCoroutine("DelayedUnlock");
        rb.AddForce(direction * 50.0f, ForceMode2D.Impulse);

        Debug.Log("Unlocked");
        isLocked = false;
        orbitParent = null;
    }

    private IEnumerator DelayedUnlock()
    {
        isLockable = false;

        yield return new WaitForSeconds(unlockDelay);

        isLockable = true;
    }


    public void Push(Vector2 direction, float power)
    {
        Vector2 targetVelocity = direction * power;
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * lockSmoothness);
    }

    // void OnDrawGizmos()
    // {
    //     // Draw a gizmo circle to visualize the gravity radius
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireSphere(orbitParent.position, orbitRadius);
    // }
}
