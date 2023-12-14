using UnityEngine;

public class Fragment : MonoBehaviour
{

    public Sprite[] sprites;
    private SpriteRenderer sr;

    public float minSpeed = 3f;
    public float maxSpeed = 6f;

    private Transform player;
    private Rigidbody2D rb;

    private float value;
    private PlayerAmmo playerAmmo;

    void Awake()
    {
        playerAmmo = GameObject.Find("Player").GetComponent<PlayerAmmo>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {


        sr.sprite = sprites[Random.Range(0, sprites.Length)];

        value = Random.Range(7, 17);

        // Find the player by tag (you can adjust this based on your setup)
        player = GameObject.Find("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure to set the correct tag or reference.");
            return;
        }

        // Set random speed and direction
        float speed = Random.Range(minSpeed, maxSpeed);
        float angle = Random.Range(0f, 360f);

        // Apply random rotation
        transform.Rotate(Vector3.forward, angle);

        // Convert angle to vector
        Vector2 initialVelocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Apply initial velocity
        rb.velocity = initialVelocity;
    }

    void Update()
    {
        // Move towards the player
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        rb.velocity = directionToPlayer * rb.velocity.magnitude;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle collection (you can modify this part based on your game's logic)
            Debug.Log("Collected!");
            playerAmmo.GainAmmo(value);
            Destroy(gameObject);
        }
    }
}
