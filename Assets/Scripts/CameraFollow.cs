using UnityEngine;
using System.Collections;

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

    public float initialWaitTime = 2f;
    public float cinematicSpeed = 2f;

    private Camera mainCamera;
    private float cameraHeight;
    private bool cinematicDone = false;
    private Vector3 startingPosition;

    private static bool hasPlayedCinematic = false;

    public float zoomInFactor = 2f;
    public float zoomDuration = 1f;
    public float slowMotionDuration = 3f;
    
    private float originalTimeScale;
    private float originalOrthographicSize;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        cameraHeight = mainCamera.orthographicSize * 2;
        UpdateBounds();

        startingPosition = transform.position;

        if (!hasPlayedCinematic || !PersistentManager.Instance.IsRestarting)
        {
            hasPlayedCinematic = true;
            StartCoroutine(CinematicSequence());
        }
        else
        {
            cinematicDone = true;
        }

        PersistentManager.Instance.IsRestarting = false;
    }

    private void LateUpdate()
    {
        if (player == null || !cinematicDone) return;

        float playerY = player.position.y;
        float targetY = Mathf.Clamp(playerY, minBounds.y, maxBounds.y);

        Vector3 targetPosition = new Vector3(0, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }

    void UpdateBounds()
    {
        if (ceiling == null || floor == null) return;

        float ceilingY = ceiling.position.y;
        float floorY = floor.position.y;

        minBounds = new Vector2(
            0,
            floorY + (cameraHeight / 2) -0.4f
        );
        maxBounds = new Vector2(
            0,
            ceilingY - cameraHeight / 2 - boundaryPadding
        );
    }

    private IEnumerator CinematicSequence()
    {
        BallControl.touchControlsEnabled = false;

        yield return new WaitForSeconds(initialWaitTime);

        Vector3 ceilingPosition = new Vector3(startingPosition.x, maxBounds.y, startingPosition.z);
        while (Vector3.Distance(transform.position, ceilingPosition) > 0.1f)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, ceilingPosition, Time.deltaTime * cinematicSpeed);
            transform.position = newPosition;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (Vector3.Distance(transform.position, startingPosition) > 0.1f)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, startingPosition, Time.deltaTime * cinematicSpeed);
            transform.position = newPosition;
            yield return null;
        }

        cinematicDone = true;

        BallControl.touchControlsEnabled = true;
    }

    public static void ResetCinematic()
    {
        hasPlayedCinematic = false;
    }

    public IEnumerator ZoomInOnPlayer()
    {
        originalTimeScale = Time.timeScale;
        originalOrthographicSize = mainCamera.orthographicSize;

        // Slow down time
        Time.timeScale = 0.1f;

        // Zoom in
        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(originalOrthographicSize, originalOrthographicSize / zoomInFactor, elapsedTime / zoomDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // Hold the zoom for a moment
        yield return new WaitForSecondsRealtime(slowMotionDuration - zoomDuration);

        // Zoom out and restore time scale
        elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(originalOrthographicSize / zoomInFactor, originalOrthographicSize, elapsedTime / zoomDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = originalTimeScale;
    }
}
