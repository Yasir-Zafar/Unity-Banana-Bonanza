using UnityEngine;

public class PowerMove : MonoBehaviour
{
    public float powerIncrease = 5f;

    private void Start()
    {
        Debug.Log("PowerMove script started on " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PowerMove collided with something.");

        if (collision.CompareTag("Player"))
        {
            Debug.Log("PowerMove collided with Player.");

            BallControl ballControl = collision.GetComponent<BallControl>();
            if (ballControl != null)
            {
                Debug.Log("BallControl script found on Player.");
                ballControl.maxForce += powerIncrease;
                Debug.Log("Player's maxPower increased by " + powerIncrease + ". New maxPower: " + ballControl.maxForce);
            }
            else
            {
                Debug.Log("BallControl script not found on Player.");
            }

            // Play sound effect or animation if needed
            // AudioSource.PlayClipAtPoint(powerUpSound, transform.position);

            Destroy(gameObject); // Remove the power-up item after collection
        }
        else
        {
            Debug.Log("PowerMove collided with non-Player object.");
        }
    }
}
