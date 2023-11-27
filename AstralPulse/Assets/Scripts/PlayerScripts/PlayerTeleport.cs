using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{

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
            if (viewportPos.x > 1.0f)
            {
                // Teleport to the left side
                transform.position = new Vector3(-mainCamera.orthographicSize * mainCamera.aspect, transform.position.y, transform.position.z);
            }
            else if (viewportPos.x < 0.0f)
            {
                // Teleport to the right side
                transform.position = new Vector3(mainCamera.orthographicSize * mainCamera.aspect, transform.position.y, transform.position.z);
            }

            if (viewportPos.y > 1.0f)
            {
                // Teleport to the bottom
                transform.position = new Vector3(transform.position.x, -mainCamera.orthographicSize, transform.position.z);
            }
            else if (viewportPos.y < 0.0f)
            {
                // Teleport to the top
                transform.position = new Vector3(transform.position.x, mainCamera.orthographicSize, transform.position.z);
            }
        }
    }
}
