using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRicochet : MonoBehaviour, IAttractable, IPushable
{
    // public float speed = 5.0f;
    // public float smoothness = 5.0f;
    public bool isLocked;
    public bool isLockable;
    // public Transform orbitParent;
    // // public float orbitRadius;
    public float orbitAngle;
    public float ricochetScale;
    // public float orbitSpeed;
    // public float orbitAngularVelocity;
    public Vector3 orbitPos;

    // [SerializeField] private float lockSmoothness;

    private Rigidbody2D rb;

    // Lockable for escape orbit
    // public float unlockDelay;
    // public bool isLockable;

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
                StartCoroutine("EscapeOrbit");
            }
        }
    }

    private IEnumerator EscapeOrbit()
    {
        Unlock();
        isLockable = false;

        yield return new WaitForSeconds(0.3f);

        isLockable = true;
    }

    public void Attract(Vector2 direction, float power)
    {
        // Debug.Log("Attracted");
        // Vector2 gravity = direction * power;

        // rb.AddForce(gravity, ForceMode2D.Force);
    }

    public void Lock(Transform lockedParent)
    {
        Debug.Log("Locked");
        // rb.velocity = Vector3.zero;
        isLocked = true;
    }

    public void Unlock()
    {
        Debug.Log("Unlocked");
        isLocked = false;
    }


    public void Push(Vector2 direction, float power)
    {
        rb.AddForce(direction * power * ricochetScale, ForceMode2D.Impulse);
    }

    // void OnDrawGizmos()
    // {
    //     // Draw a gizmo circle to visualize the gravity radius
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireSphere(orbitParent.position, orbitRadius);
    // }
}
