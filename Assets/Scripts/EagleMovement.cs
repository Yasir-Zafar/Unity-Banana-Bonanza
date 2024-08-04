using UnityEngine;

public class EagleMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform startingWall;
    public Transform leftWall;
    public Transform rightWall;
    public Vector3 leftWallOffset;  // Offset from the left wall to start position
    public Vector3 rightWallOffset; // Offset from the right wall to start position

    private Vector3 startPosition;
    private bool movingRight = true;
    private float leftEdge = -2.05f;
    private float rightEdge = 2.05f;

    private void Start()
    {
        if (startingWall == leftWall)
        {
            movingRight = true;
            startPosition = leftWall.position + leftWallOffset;
        }
        else
        {
            movingRight = false;
            startPosition = rightWall.position + rightWallOffset;
        }

        transform.position = new Vector3(startPosition.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (movingRight && transform.position.x >= rightEdge)
        {
            movingRight = false;
            FlipEagle();
        }
        else if (!movingRight && transform.position.x <= leftEdge)
        {
            movingRight = true;
            FlipEagle();
        }
    }

    private void FlipEagle()
    {
        // Flip the eagle horizontally
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
