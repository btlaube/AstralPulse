using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Asteroid")]
    public GameObject asteroidPrefab;
    [SerializeField] private float asteroidSpawnRate;
    private float asteroidSpawnTimer;
    public float circleRadius = 5f;


    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        asteroidSpawnTimer += Time.deltaTime;
        if (asteroidSpawnTimer >= asteroidSpawnRate)
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        asteroidSpawnTimer = 0;
        Vector3 spawnPos = GenerateRandomPosition();
        Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
    }

    private Vector3 GenerateRandomPosition()
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);

        // Use polar to Cartesian coordinate conversion
        float randomX = 0.5f + circleRadius * Mathf.Cos(randomAngle);
        float randomY = 0.5f + circleRadius * Mathf.Sin(randomAngle);

        // Convert viewport coordinates to world coordinates
        Vector3 randomViewportPoint = new Vector3(randomX, randomY, 0.0f);
        Vector3 randomWorldPoint = mainCamera.ViewportToWorldPoint(randomViewportPoint);
        randomWorldPoint.z = 0.0f;

        return randomWorldPoint;
    }
}
