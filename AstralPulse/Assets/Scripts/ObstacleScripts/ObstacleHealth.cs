using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHealth : MonoBehaviour, IDamageable
{

    // [Header("Visuals")]
    // private Animator playerAnimator;
    // private GameObject playerVisual;
    // private GameObject attackVisual;

    public GameObject fragmentPrefab;

    [SerializeField] private float startingHealth;
    public float currentHealth;

    private bool isDead;

    void Awake()
    {
        currentHealth += Random.Range(0, 3);
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

    public IEnumerator Death()
    {
        // playerAnimator.SetTrigger("Die");
        // GetComponentInChildren<PlayerMovement>().enabled = false;

        // Add points to player
        GameObject.Find("Player").GetComponent<PlayerScoreCounter>().AddPointsWithIcon(GetComponentInChildren<SpriteRenderer>().sprite, Random.Range(115, 237));


        int randomDrops = Random.Range(2, 5);
        for (int i = 0; i < randomDrops; i++)
        {
            Instantiate(fragmentPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);

        yield return new WaitForSeconds(1.0f);

        // Hide all player visuals
        // playerVisual.SetActive(false);
        // attackVisual.SetActive(false);
    }
}
