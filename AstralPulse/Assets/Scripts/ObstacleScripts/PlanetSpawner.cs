using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject planetPrefab;
    public Vector3 lastPos;
    public float playerSpawnDist;
    public float minPlanetDist;

    private Transform player;
    [SerializeField] private List<GameObject> planets = new List<GameObject>();

    void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, lastPos) >= playerSpawnDist)
        {
            SpawnPlanet();
            lastPos = player.position;
        }

        
    }

    void SpawnPlanet()
    {
        // Generate a random position for the new planet
        Vector3 randomPosition = GetRandomPosition();

        // Check if the new planet is at least minPlanetDist away from existing planets
        while (!IsFarEnoughFromPlanets(randomPosition))
        {
            randomPosition = GetRandomPosition();
        }

        // Instantiate the planetPrefab at the chosen position
        GameObject newPlanet = Instantiate(planetPrefab, randomPosition, Quaternion.identity);

        // Add the new planet to the planets list
        planets.Add(newPlanet);
    }

    Vector3 GetRandomPosition()
    {
        // Generate a random position based on your game's world boundaries
        // You may need to customize this based on your game's design
        float randomX = Random.Range(-50f, 50f);
        float randomY = Random.Range(-50f, 50f);
        return new Vector3(player.position.x + randomX, player.position.y + randomY, 0.0f);
    }

    bool IsFarEnoughFromPlanets(Vector3 position)
    {
        // Check if the new position is at least minPlanetDist away from all existing planets
        foreach (GameObject existingPlanet in planets)
        {
            if (Vector3.Distance(position, existingPlanet.transform.position) < minPlanetDist)
            {
                return false;
            }
        }
        return true;
    }

}
