using UnityEngine;

public class BananaCollector : MonoBehaviour
{
    private CameraFollow cameraFollow;
    private bool isInvincible = false;

    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Banana"))
        {
            CollectBanana(collision.gameObject);
        }
    }

    private void CollectBanana(GameObject banana)
    {
        // Disable the banana
        banana.SetActive(false);

        // Start the zoom and slow-motion effect
        StartCoroutine(cameraFollow.ZoomInOnPlayer());

        // Make the player invincible
        // StartCoroutine(TemporaryInvincibility());
    }

    private System.Collections.IEnumerator TemporaryInvincibility()
    {
        isInvincible = true;
        
        // Wait for the slow-motion effect to end
        yield return new WaitForSecondsRealtime(cameraFollow.slowMotionDuration);

        isInvincible = false;
    }

    // Add this method to your player's script (e.g., BallControl)
    public bool IsInvincible()
    {
        return isInvincible;
    }
}