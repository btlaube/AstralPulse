using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Get the viewport position of the current player position
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

            // Check if the player is outside the camera bounds and adjust their position accordingly
            if (transform.position.x > maxX)
            {
                // Teleport to the left side
                transform.position = new Vector3(-mainCamera.orthographicSize * mainCamera.aspect, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < minX)
            {
                // Teleport to the right side
                transform.position = new Vector3(mainCamera.orthographicSize * mainCamera.aspect, transform.position.y, transform.position.z);
            }

            if (transform.position.y > maxY)
            {
                // Teleport to the bottom
                transform.position = new Vector3(transform.position.x, -mainCamera.orthographicSize, transform.position.z);
            }
            else if (transform.position.y < minY)
            {
                // Teleport to the top
                transform.position = new Vector3(transform.position.x, mainCamera.orthographicSize, transform.position.z);
            }
        }
    }
}
