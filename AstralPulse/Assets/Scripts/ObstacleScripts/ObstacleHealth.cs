using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHealth : MonoBehaviour
{

    // [Header("Visuals")]
    // private Animator playerAnimator;
    // private GameObject playerVisual;
    // private GameObject attackVisual;

    [SerializeField] private float startingHealth;
    public float currentHealth;

    private bool isDead;

    void Awake()
    {
        // playerVisual = transform.Find("PlayerVisual").gameObject;
        // attackVisual = transform.Find("AttackVisual/AttackPulse").gameObject;
        // playerAnimator = playerVisual.GetComponentInChildren<Animator>();
    }

    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            // Took damage without dying
        }
        else
        {
            if (!isDead)
            {
                // Died
                isDead = true;
                // playerAnimator.SetTrigger("Die");
                StartCoroutine("Death");
            }
        }
    }

    public void Respawn()
    {
        // GetComponentInChildren<PlayerMovement>().enabled = true;
        // playerVisual.SetActive(true);
        // attackVisual.SetActive(true);
    }

    public IEnumerator Death()
    {
        // playerAnimator.SetTrigger("Die");
        // GetComponentInChildren<PlayerMovement>().enabled = false;
        Destroy(gameObject);
        yield return new WaitForSeconds(1.0f);

        // Hide all player visuals
        // playerVisual.SetActive(false);
        // attackVisual.SetActive(false);
    }
}
