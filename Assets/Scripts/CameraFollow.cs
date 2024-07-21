using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Smooth speed of the camera movement
    public Vector3 offset; // Offset from the player's position
    public Transform ceiling; // Reference to the ceiling transform
    public Transform floor; // Reference to the floor transform
    public float boundaryPadding = 1f; // Padding below the ceiling for camera bounds
    public Vector2 minBounds; // Minimum bounds for the camera
    public Vector2 maxBounds; // Maximum bounds for the camera

    private Camera camera;
    private float cameraHeight;

    private void Start() {
        camera = GetComponent<Camera>();
        cameraHeight = camera.orthographicSize * 2;
        UpdateBounds();
    }

    private void LateUpdate() {
        if (player == null) return;

        // Desired vertical position of the camera
        float playerY = player.position.y;
        float targetY = Mathf.Clamp(playerY, minBounds.y, maxBounds.y);

        // Smoothly move the camera's vertical position
        Vector3 targetPosition = new Vector3(0, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }

    // Update the camera bounds based on the level boundaries
    void UpdateBounds() {
        if (ceiling == null || floor == null) return;

        // Get the level boundaries
        float ceilingY = ceiling.position.y;
        float floorY = floor.position.y;

        // Set vertical bounds with padding below the ceiling
        minBounds = new Vector2(
            0, // Fixed x position
            floorY + cameraHeight / 2 // Bottom boundary
        );
        maxBounds = new Vector2(
            0, // Fixed x position
            ceilingY - cameraHeight / 2 - boundaryPadding // Top boundary with padding
        );
    }
}
