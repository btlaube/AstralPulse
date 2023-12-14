using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject playerDeathCanvas;

    [Header("Visuals")]
    private Animator playerAnimator;
    private GameObject playerVisual;
    private GameObject attackVisual;

    [SerializeField] private float startingHealth;
    public float currentHealth;

    private bool isDead;
    public MonoBehaviour[] disableScripts;

    // UI
    public Image healthFill;

    void Awake()
    {
        playerVisual = transform.Find("PlayerVisual").gameObject;
        attackVisual = transform.Find("AttackVisual/AttackPulse").gameObject;
        playerAnimator = playerVisual.GetComponentInChildren<Animator>();
    }

    void Start()
    {
        playerDeathCanvas.SetActive(false);
        
        currentHealth = startingHealth;
        float healthRatio = currentHealth / startingHealth;
        healthFill.fillAmount = healthRatio;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        float healthRatio = currentHealth / startingHealth;
        healthFill.fillAmount = healthRatio;

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
        GetComponentInChildren<PlayerMovement>().enabled = true;
        playerVisual.SetActive(true);
        attackVisual.SetActive(true);
    }

    public IEnumerator Death()
    {
        playerAnimator.SetTrigger("Die");
        GetComponentInChildren<PlayerMovement>().enabled = false;
        playerDeathCanvas.SetActive(true);

        foreach (MonoBehaviour script in disableScripts)
        {
            script.enabled = false;
        }

        yield return new WaitForSeconds(1.0f);

        // Destroy(gameObject);
        // Hide all player visuals
        playerVisual.SetActive(false);
        attackVisual.SetActive(false);
    }
}
