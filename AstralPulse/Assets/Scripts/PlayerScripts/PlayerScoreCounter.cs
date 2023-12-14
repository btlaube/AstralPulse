using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScoreCounter : MonoBehaviour
{
    public int playerScore;
    public GameObject scoreCanvas;
    public GameObject gainPointsPrefab;
    public int minValue = 57;
    public int maxValue = 101;
    public float lowProbability = 0.8f;

    private TMP_Text scoreText;
    private Transform gainedPointsParent;

    // private AudioManager audioManager;

    // Shake parameters
    private float shakeDuration = 0.2f;
    private float shakeMagnitude = 3f;

    void Awake()
    {
        scoreText = scoreCanvas.transform.Find("Score").gameObject.GetComponent<TMP_Text>();
        gainedPointsParent = scoreCanvas.transform.Find("GainPointsParent");
        // audioManager = GetComponent<AudioManager>();
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddPointsWithIcon(Sprite icon, int points)
    {
        AddPoints(points);
        StartCoroutine(DisplayGainedPoints(points, icon));
        UpdateScoreText();
        StartCoroutine(ShakeScoreText());
    }

    public void AddPoints(int pointsAmount=10)
    {
        playerScore+=pointsAmount;
        // audioManager.Play("Points");
    }

    public IEnumerator DisplayGainedPoints(int points, Sprite icon)
    {
        GameObject newPointsObject = Instantiate(gainPointsPrefab, gainedPointsParent.position, Quaternion.identity);
        newPointsObject.transform.SetParent(gainedPointsParent);
        newPointsObject.transform.localScale = Vector3.one;
        newPointsObject.transform.Find("Text").gameObject.GetComponent<TMP_Text>().text = $"+{points}";
        newPointsObject.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = icon;

        yield return new WaitForSeconds(1.3f);

        Destroy(newPointsObject);
    }

    private IEnumerator ShakeScoreText()
    {
        Transform scoreTextTransform = scoreText.transform;
        Vector3 originalPosition = scoreTextTransform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * shakeMagnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * shakeMagnitude;

            scoreTextTransform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        scoreTextTransform.localPosition = originalPosition; // Reset to the original position
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"{playerScore}pts";
    }

}
