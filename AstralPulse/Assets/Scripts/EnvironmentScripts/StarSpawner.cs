using System.Collections;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    public Transform playerTransform;
    public float gridWidth = 1.0f;
    public float gridHeight = 1.0f;

    private Transform[,] starGrid = new Transform[3, 3];
    private Vector2Int playerGridPosition;

    void Start()
    {
        InitializeGrid();
    }

    void Update()
    {
        UpdatePlayerGridPosition();
        CheckSpawnDespawnStars();
    }

    void InitializeGrid()
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                Vector3 spawnPosition = new Vector3((col-1) * gridWidth, (row-1) * gridHeight, 0);
                Transform star = Instantiate(starPrefab, spawnPosition, Quaternion.identity).transform;
                star.SetParent(transform);
                starGrid[row, col] = star;
            }
        }
    }

    void UpdatePlayerGridPosition()
    {
        // Calculate the player's grid position based on the world position
        int row = Mathf.FloorToInt(playerTransform.position.y / gridWidth) + 1;
        int col = Mathf.FloorToInt(playerTransform.position.x / gridHeight) + 1;

        playerGridPosition = new Vector2Int(Mathf.Clamp(row, 0, 2), Mathf.Clamp(col, 0, 2));
        Debug.Log(playerGridPosition);
    }

    void CheckSpawnDespawnStars()
    {
        // Check if player moved beyond the center sprite
        if (playerGridPosition.x != 1 || playerGridPosition.y != 1)
        {
            // Player Moved Up
            if (playerGridPosition.y == 0)
            {
                // Move bottom row to top row
                for (int i = 0; i < 3; i++)
                {
                    // starGrid[]
                }
            }
        }
    }

}

