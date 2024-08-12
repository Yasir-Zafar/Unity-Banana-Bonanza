using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PowerMove : MonoBehaviour
{
    public float powerIncrease = 5f;
    public float heightIncrease = 0.2f;
    public float widthIncrease = 0.1f;

    private void Start()
    {
        Debug.Log("PowerMove script started on " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BallControl ballControl = collision.GetComponent<BallControl>();
            if (ballControl != null)
            {
                ballControl.maxForce += powerIncrease;
                ballControl.maxLineHeight += heightIncrease;
                ballControl.maxLineWidth += widthIncrease;
            }
            else
            {
                Debug.Log("BallControl script not found on Player.");
            }

            // Play sound effect or animation if needed
            // AudioSource.PlayClipAtPoint(powerUpSound, transform.position);

            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("PowerMove collided with non-Player object.");
        }
    }
}
