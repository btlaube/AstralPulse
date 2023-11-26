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
    private ParticleSystem particleSystem;
    private bool isEmitting;

    void Awake()
    {
        playerAnimator = transform.Find("PlayerVisual").gameObject.GetComponentInChildren<Animator>();
        
        twinkleTransform = transform.Find("AttackVisual/AttackTwinkle");
        twinkle = twinkleTransform.gameObject;

        pulseTransform = transform.Find("AttackVisual/AttackPulse");
        pulse = pulseTransform.gameObject;

        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // particleSystem.Play();
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
                    twinkleTransform.localScale = Vector3.one * 0.5f;
                    pulseTransform.localScale = Vector3.one * 0.7f;
                    break;
                case 2:
                    SetParticleSystem(0.4f, -1.45f, 0.25f, 50.0f, 0.7f);
                    twinkleTransform.localScale = Vector3.one * 0.8f;
                    pulseTransform.localScale = Vector3.one * 1.0f;
                    break;
                case 3:
                    SetParticleSystem(0.4f, -1.45f, 0.25f, 50.0f, 0.7f);
                    twinkleTransform.localScale = Vector3.one * 1.0f;
                    pulseTransform.localScale = Vector3.one * 1.5f;
                    break;
            }
            if (!isEmitting)
            {
                particleSystem.Play();
                particleSystem.Emit(1);
                isEmitting = true;
            }
        }
    }

    private void ReleaseAttack()
    {
        // Debug.Log($"Released attack level {currentAttackLevel}");
        attackChargeTimer = 0;
        currentAttackLevel = 1;
        playerAnimator.SetInteger("AttackLevel", 0);
        playerAnimator.SetTrigger("ReleaseAttack");
        
        StopAllCoroutines();
        StartCoroutine("TwinkleAnimation");

        particleSystem.Stop();
        particleSystem.Clear();
        isEmitting = false;
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
        particleSystem.startLifetime = startLifetime;
        particleSystem.startSpeed = startSpeed;
        particleSystem.startSize = startSize;

        ParticleSystem.EmissionModule psEmission = particleSystem.emission;
        psEmission.rateOverTime = rateOverTime;

        ParticleSystem.ShapeModule psShape = particleSystem.shape;
        psShape.radius = radius;
    }

}
