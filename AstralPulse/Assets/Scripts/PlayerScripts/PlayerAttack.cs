using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    // Animator
    private Animator playerAnimator;

    [SerializeField] private int attackLevel;
    [SerializeField] private int maxAttackLevel;
    [SerializeField] private int currentAttackLevel;
    
    [Header("Attack Visual")]
    // public GameObject twinklePrefab;
    private Transform twinkleTransform;
    private Transform pulseTransform;
    private GameObject twinkle;
    private GameObject pulse;

    // Attack charge 
    [SerializeField] private float attackChargeRate;
    [SerializeField] private float attackChargeTimer;

    // Attack Particle
    private ParticleSystem pSystem;
    private bool isEmitting;

    [SerializeField] private Vector3 attackRangeScaleVector;
    [SerializeField] private float attackRange = 0.0f;
    [SerializeField] private float attackPower = 0.0f;

    [SerializeField] private Vector3 timeScaleVector;
    [SerializeField] private float escapeScaler;

    private Camera myCamera;

    void Awake()
    {
        playerAnimator = transform.Find("PlayerVisual").gameObject.GetComponentInChildren<Animator>();
        
        twinkleTransform = transform.Find("AttackVisual/AttackTwinkle");
        twinkle = twinkleTransform.gameObject;

        pulseTransform = transform.Find("AttackVisual/AttackPulse");
        pulse = pulseTransform.gameObject;

        // pSystem = GetComponentInChildren<ParticleSystem>();
        pSystem = GetComponentInChildren<ParticleSystem>();

        myCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        // pSystem.Play();
        currentAttackLevel = 1;
        playerAnimator.SetInteger("AttackLevel", -1);
        twinkle.SetActive(false);
        pulse.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Start charging the attack
            ChargeAttack();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Release the charged attack
            ReleaseAttack();
        }
    }

    private void ChargeAttack()
    {
        if (currentAttackLevel < maxAttackLevel)
        {
            attackChargeTimer += Time.deltaTime;
            if (attackChargeTimer >= attackChargeRate)
            {
                attackChargeTimer = 0;
                currentAttackLevel += 1;
                // playerAnimator.SetInteger("AttackLevel", currentAttackLevel);
            }
            playerAnimator.SetInteger("AttackLevel", currentAttackLevel);
            

            
            switch (currentAttackLevel)
            {
                case 1:
                    SetParticleSystem(0.4f, -1.3f, 0.10f, 30.0f, 0.65f);
                    twinkleTransform.localScale = Vector3.one * attackRangeScaleVector.x / 2.0f;
                    pulseTransform.localScale = Vector3.one * attackRangeScaleVector.x;

                    Time.timeScale = timeScaleVector.x;
                    break;
                case 2:
                    SetParticleSystem(0.4f, -1.45f, 0.25f, 50.0f, 0.7f);
                    twinkleTransform.localScale = Vector3.one * attackRangeScaleVector.y / 2.0f;
                    pulseTransform.localScale = Vector3.one * attackRangeScaleVector.y;

                    Time.timeScale = timeScaleVector.y;
                    break;
                case 3:
                    SetParticleSystem(0.4f, -1.45f, 0.25f, 50.0f, 0.7f);
                    twinkleTransform.localScale = Vector3.one * attackRangeScaleVector.z / 2.0f;
                    pulseTransform.localScale = Vector3.one * attackRangeScaleVector.z;

                    Time.timeScale = timeScaleVector.z;
                    break;
            }
            if (!isEmitting)
            {
                pSystem.Play();
                pSystem.Emit(1);
                isEmitting = true;
            }
        }
    }

    private void ReleaseAttack()
    {

        Time.timeScale = 1.0f;
        StartCoroutine(myCamera.GetComponent<CameraShake>().Shake(0.2f, 0.2f));

        // Attack Push
        switch (currentAttackLevel)
        {
            case 1:
                attackRange = attackRangeScaleVector.x;
                attackPower = attackRangeScaleVector.x * 2.0f;
                break;
            case 2:
                attackRange = attackRangeScaleVector.y;
                attackPower = attackRangeScaleVector.y * 2.0f;
                break;
            case 3:
                attackRange = attackRangeScaleVector.z;
                attackPower = attackRangeScaleVector.z * 2.0f;
                break;
        }

        // Get Collisions
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        // Process the colliders
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.name == "Player") continue;

            if (collider.gameObject.tag == "Unpushable")
            {
                Vector2 direction = transform.position - collider.transform.position;
                direction.Normalize();
                GetComponent<PlayerRicochet>().Push(direction, attackPower * escapeScaler);
            }
            else
            {
                // Check if the collided object implements IPushable interface
                IPushable pushableObject = collider.gameObject.GetComponent<IPushable>();
                if (pushableObject != null)
                {
                    Vector3 direction = collider.transform.position - transform.position;
                    // Normalize the direction vector to get a unit vector
                    direction.Normalize();
                    // The collided object implements the IDamageable interface
                    pushableObject.Push(direction, attackPower * escapeScaler); // Example: Call a method from the interface
                }
            }

            // Check if the collided object implements IDamageable interface
            IDamageable damageableObject = collider.gameObject.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                // The collided object implements the IDamageable interface
                damageableObject.TakeDamage(currentAttackLevel); // Example: Call a method from the interface
            }
        }


        // Debug.Log($"Released attack level {currentAttackLevel}");
        // Attack Visual
        playerAnimator.SetInteger("AttackLevel", 0);
        playerAnimator.SetTrigger("ReleaseAttack");
        
        // StopAllCoroutines();
        StartCoroutine("TwinkleAnimation");

        pSystem.Stop();
        pSystem.Clear();
        isEmitting = false;

        attackChargeTimer = 0;
        currentAttackLevel = 1;

    }

    // Optionally, visualize the detection circle in the Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private IEnumerator TwinkleAnimation()
    {
        twinkle.SetActive(true);
        pulse.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        twinkle.SetActive(false);
        pulse.SetActive(false);
    }

    private void SetParticleSystem(float startLifetime, float startSpeed, float startSize, float rateOverTime, float radius)
    {
        ParticleSystem.MainModule main = pSystem.main;

        main.startLifetime = startLifetime;
        main.startSpeed = startSpeed;
        main.startSize = startSize;

        ParticleSystem.EmissionModule psEmission = pSystem.emission;
        psEmission.rateOverTime = rateOverTime;

        ParticleSystem.ShapeModule psShape = pSystem.shape;
        psShape.radius = radius;
    }

}
