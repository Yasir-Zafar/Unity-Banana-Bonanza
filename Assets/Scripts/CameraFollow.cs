using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f; 
    public Vector3 offset;
    public Transform ceiling;
    public Transform floor; 
    public float boundaryPadding = 1f;
    public Vector2 minBounds; 
    public Vector2 maxBounds; 

    private Camera mainCamera; 
    private float cameraHeight;

    private void Start() {
        mainCamera = GetComponent<Camera>();
        cameraHeight = mainCamera.orthographicSize * 2;
        UpdateBounds();
    }

    private void LateUpdate() {
        if (player == null) return;

        float playerY = player.position.y;
        float targetY = Mathf.Clamp(playerY, minBounds.y, maxBounds.y);

        Vector3 targetPosition = new Vector3(0, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }

    void UpdateBounds() {
        if (ceiling == null || floor == null) return;

        float ceilingY = ceiling.position.y;
        float floorY = floor.position.y;

        minBounds = new Vector2(
            0, 
            floorY + cameraHeight / 2
        );
        maxBounds = new Vector2(
            0,
            ceilingY - cameraHeight / 2 - boundaryPadding 
        );
    }
}
