using System.Collections;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starsPrefab;
    public Transform player;
    public Transform nearestStars;

    public Vector2 spawnOffset;
    public float despawnOffset = 20.0f;
    public float starsWidth;
    public float starsHeight;

    void Start()
    {
        nearestStars = Instantiate(starsPrefab, player.position, Quaternion.identity, transform).transform;
    }

    void Update()
    {
        // minDist = Mathf.inf;
        foreach (Transform stars in transform)
        {
            if (Vector3.Distance(player.position, stars.position) < Vector3.Distance(player.position, nearestStars.position))
            {
                nearestStars = stars;
            }
        }

        Vector3 direction = player.position - nearestStars.position;
        Debug.Log(direction);

        // if direction.x < spawnOffset
            // spawn to the left
        Vector3 spawnPos = Vector3.zero;

        if (direction.x < -spawnOffset.x)
        {
            spawnPos = new Vector3(nearestStars.position.x - starsWidth, nearestStars.position.y, 0.0f);
        }
        else if (direction.x > spawnOffset.x)
        {
            spawnPos = new Vector3(nearestStars.position.x + starsWidth, nearestStars.position.y, 0.0f);
        }

        if (direction.y < -spawnOffset.y)
        {
            spawnPos = new Vector3(nearestStars.position.x, nearestStars.position.y - starsHeight, 0.0f);
        }
        else if (direction.y > spawnOffset.y)
        {
            spawnPos = new Vector3(nearestStars.position.x, nearestStars.position.y + starsHeight, 0.0f);
        }

        if (spawnPos != Vector3.zero)
        {
            Instantiate(starsPrefab, spawnPos, Quaternion.identity, transform);
        }

        // Remove obstacles that have moved far enough behind the player
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Background");
        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.transform.position.x < player.position.x - despawnOffset)
            {
                Destroy(obstacle);
            }
        }
    }

}

