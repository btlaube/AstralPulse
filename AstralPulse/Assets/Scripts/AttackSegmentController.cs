using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSegmentController : MonoBehaviour
{
    public AttackSegment[] attackSegments;

    void Awake()
    {
        // Create an array to store the children
        attackSegments = new AttackSegment[transform.childCount];
        // Populate the array with the children
        for (int i = 0; i < attackSegments.Length; i++)
        {
            attackSegments[i] = transform.GetChild(i).gameObject.GetComponent<AttackSegment>();
        }
    }

    void Start()
    {
        // Discharge();
    }

    public void ChargeAttackToLevel(int level, float chargeAmount)
    {
        Debug.Log(attackSegments[level]);
        attackSegments[level].SetAvailableProgress(chargeAmount);
    }

    public void ActivateAttackToLevel(int level, float attackAmount)
    {
        Debug.Log(attackSegments[level]);
        attackSegments[level].SetActiveProgress(attackAmount);
    }

    public void ChargeAll()
    {
        foreach (Transform segment in transform)
        {
            AttackSegment attackSeg = segment.gameObject.GetComponent<AttackSegment>();
            if (attackSeg != null)
            {
                attackSeg.MakeAvailable();
            }
        }
    }

    public void DischargeAll()
    {
        foreach (Transform segment in transform)
        {
            AttackSegment attackSeg = segment.gameObject.GetComponent<AttackSegment>();
            if (attackSeg != null)
            {
                attackSeg.SetActiveProgress(0.0f);
            }
        }
    }

    public void ResetAttackSegments()
    {

    }

    public void ReleaseAttack(int attackLevel)
    {

    }

}
