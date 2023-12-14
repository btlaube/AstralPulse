using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmo : MonoBehaviour
{
    public int availableAttacks;
    public float fragmentsToAmmo;
    public float currentFragments;

    public AttackSegmentController attackBar;
    
    void Start()
    {
        availableAttacks = 3;
        attackBar.ChargeAll();
    }

    // void Update()
    // {
    //     for (int i = availableAttacks; i < attackBar.attackSegments.Length; i++)
    //     {
    //         attackBar.ChargeAttackToLevel(i, 0.0f);
    //     }
    // }

    public void UpdateAvailability()
    {
        // Debug.Log("jkljkj");
        int i = 0;
        for (i = availableAttacks; i < attackBar.attackSegments.Length; i++)
        {
            Debug.Log(i);
            attackBar.ChargeAttackToLevel(i, 0.0f);
        }
        if (availableAttacks < 3)
        {
            attackBar.ChargeAttackToLevel(availableAttacks, currentFragments);
        }
    }

    public void GainAmmo(float amount)
    {
        currentFragments += amount;
        if (currentFragments >= fragmentsToAmmo)
        {
            availableAttacks = Mathf.Clamp(availableAttacks + 1, 0, 3);
            currentFragments = 0.0f;
        }

        float chargeAmount = currentFragments / fragmentsToAmmo;
            
        switch (availableAttacks)
        {
            case 1:
                attackBar.ChargeAttackToLevel(0, 1.0f);
                attackBar.ChargeAttackToLevel(1, chargeAmount);
                break;
            case 2:
                attackBar.ChargeAttackToLevel(2, chargeAmount);
                break;
            case 3:
                // attackBar.ChargeAttackToLevel(2, chargeAmount);
                break;
        }
    }
}
