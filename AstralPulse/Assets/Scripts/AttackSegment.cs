using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSegment : MonoBehaviour
{
    public GameObject activeFill;
    public GameObject availableFill;
    private Animator animator;

    void Awake()
    {
        activeFill = transform.Find("ActiveFill").gameObject;
        availableFill = transform.Find("AvailableFill").gameObject;
        animator = GetComponent<Animator>();
    }
    
    public void Activate()
    {
        animator.SetTrigger("Activate");
    }

    public void MakeAvailable()
    {
        // activeFill.GetComponent<Image>().fillAmount = 0.0f;
        availableFill.GetComponent<Image>().fillAmount = 1.0f;
    }

    public void MakeUnavailable()
    {
        // activeFill.GetComponent<Image>().fillAmount = 0.0f;
        availableFill.GetComponent<Image>().fillAmount = 0.0f;
    }

    public void SetActiveProgress(float activeAmount)
    {
        Debug.Log(this);
        activeFill.GetComponent<Image>().fillAmount = activeAmount;
    }

    public void SetAvailableProgress(float availableAmount)
    {
        Debug.Log(this);
        availableFill.GetComponent<Image>().fillAmount = availableAmount;
    }

}
