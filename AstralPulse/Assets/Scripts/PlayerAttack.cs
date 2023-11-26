using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    // Animator
    private Animator animator;

    [SerializeField] private int attackLevel;
    [SerializeField] private int maxAttackLevel;
    [SerializeField] private int currentAttackLevel;
    
    //Attack charge 
    [SerializeField] private float attackChargeRate;
    [SerializeField] private float attackChargeTimer;

    // Attack Particle
    private ParticleSystem particleSystem;
    private bool isEmitting;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // particleSystem.Play();
        currentAttackLevel = 1;
        animator.SetInteger("AttackLevel", -1);
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
                // animator.SetInteger("AttackLevel", currentAttackLevel);
            }
            animator.SetInteger("AttackLevel", currentAttackLevel);
            
            switch (currentAttackLevel)
            {
                case 1:
                    // SetParticleSystem(0.44f, -0.9f, 0.07f, 5.0f, 0.5f);
                    SetParticleSystem(0.4f, -1.3f, 0.10f, 30.0f, 0.65f);
                    break;
                case 2:
                    // SetParticleSystem(0.4f, -1.3f, 0.10f, 30.0f, 0.65f);
                    SetParticleSystem(0.4f, -1.45f, 0.25f, 50.0f, 0.7f);
                    break;
                case 3:
                    SetParticleSystem(0.4f, -1.45f, 0.25f, 50.0f, 0.7f);
                    break;
                // default:
                //     particleSystem.Stop();
                //     break;
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
        Debug.Log($"Released attack level {currentAttackLevel}");
        attackChargeTimer = 0;
        currentAttackLevel = 1;
        animator.SetInteger("AttackLevel", 0);
        animator.SetTrigger("ReleaseAttack");
        particleSystem.Stop();
        particleSystem.Clear();
        isEmitting = false;
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
