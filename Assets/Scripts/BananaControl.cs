using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class BananaControl : MonoBehaviour
{
    public Animator StarAnimator;
    public Animator LevelSelectAnimator;
    public Animator RetryAnimator;
    public Animator NextAnimator;
    public Animator MonkeDance;
    public Animator GokuPose;
    public Button LevelSelect;
    public Button Retry;
    public Button Next;
    private AudioSource audioSource;
    public AudioClip yayyy;
    public int levelIndex;

    public float slowMotionFactor = 0.05f;
    public float slowMotionDuration = 3f;
    public float zoomInFactor = 2f;
    public float zoomDuration = 1.5f;
    public float cameraMovementSpeed = 0.5f;

    private Camera mainCamera;
    private float originalTimeScale;
    private float originalOrthographicSize;

    private Rigidbody2D playerRigidbody;

    void Start()
    {
        MonkeDance.gameObject.SetActive(false);
        GokuPose.gameObject.SetActive(false);
        LevelSelect.gameObject.SetActive(false);
        Retry.gameObject.SetActive(false);
        Next.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.SaveStars(levelIndex, 0);

        mainCamera = Camera.main;
        originalTimeScale = Time.timeScale;
        originalOrthographicSize = mainCamera.orthographicSize;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerRigidbody = player.GetComponent<Rigidbody2D>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BananaCollectedSequence());
        }
    }

    private IEnumerator BananaCollectedSequence()
    {
        audioSource.clip = yayyy;
        if (AudioManager.Instance.play == true)
        {
            audioSource.Play();
        }

        yield return StartCoroutine(SlowMotionZoomEffect());

        StarAnimator.Play("StarsSpawn");
        LevelSelect.gameObject.SetActive(true);
        LevelSelectAnimator.Play("LevelSelectSpawn");
        Retry.gameObject.SetActive(true);
        RetryAnimator.Play("RetrySpawn");
        Next.gameObject.SetActive(true);
        NextAnimator.Play("NextSpawn");
        StartCoroutine(MonkeyDanceInator());
    }

    private IEnumerator SlowMotionZoomEffect()
    {
        Time.timeScale = slowMotionFactor;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = player.transform.position;
        Vector3 startPosition = mainCamera.transform.position;
        Vector3 targetPosition = new Vector3(playerPosition.x, playerPosition.y, mainCamera.transform.position.z);

        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            float t = EaseOutCubic(elapsedTime / zoomDuration);
            
            mainCamera.orthographicSize = Mathf.Lerp(originalOrthographicSize, originalOrthographicSize / zoomInFactor, t);
            
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, t * cameraMovementSpeed);
            mainCamera.transform.position = newPosition;

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // Ensure the camera is exactly at the target position
        mainCamera.transform.position = targetPosition;
        mainCamera.orthographicSize = originalOrthographicSize / zoomInFactor;

        // Wait for the remaining slow-motion duration
        yield return new WaitForSecondsRealtime(slowMotionDuration - zoomDuration);

        if (playerRigidbody != null)
        {
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        // Restore normal time scale
        Time.timeScale = originalTimeScale;
    }

    private float EaseOutCubic(float t)
    {
        return 1 - Mathf.Pow(1 - t, 3);
    }

    private IEnumerator MonkeyDanceInator()
    {
        yield return new WaitForSeconds(0.5f);
        if (GameManager.Instance.GetStars(levelIndex) <= 2)
        {
            MonkeDance.gameObject.SetActive(true);
        }
        else if(GameManager.Instance.GetStars(levelIndex) == 3)
        {
            GokuPose.gameObject.SetActive(true);
        }
    }
}