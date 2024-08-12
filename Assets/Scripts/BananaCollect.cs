using UnityEngine;

public class BananaCollect : MonoBehaviour
{
    public float circularDuration ; // Duration of circular movement
    public float moveUpDuration;   // Duration of moving up to the top center
    public float circularRadius;   // Radius of the circular movement
    private Vector3 startPosition;
    private bool startAnimation = false;
    private float elapsedTime = 0f;
    private Vector3 unitX;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            unitX = new Vector3(1f, 0.3f, 0);
            startPosition = transform.position;
            startAnimation = true;
            elapsedTime = 0f;
        }
    }

    void Update()
    {
        if (startAnimation)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime <= circularDuration)
            {
                // Circular movement
                float angle = (elapsedTime / circularDuration) * Mathf.PI * 2f; // Complete one full circle
                float xOffset = Mathf.Cos(angle) * circularRadius;
                float yOffset = Mathf.Sin(angle) * circularRadius;
                transform.position = startPosition + new Vector3(xOffset, yOffset, 0);
            }
            else if (elapsedTime <= circularDuration + moveUpDuration)
            {
                // Move to top center
                float t = (elapsedTime - circularDuration) / moveUpDuration;
                Vector3 targetPosition = new Vector3(1.5f, Screen.height - 1, 0);
                targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
                targetPosition.z = 0; // Ensure it's at the same z-depth

                transform.position = Vector3.Lerp(startPosition + unitX, targetPosition, t);
            }
            else
            {
                startAnimation = false; // Stop the animation
            }
        }
    }
}
