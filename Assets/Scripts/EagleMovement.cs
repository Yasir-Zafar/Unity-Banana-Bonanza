using UnityEngine;

public class EagleMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform startingWall;

    private Vector3 startPosition;
    private bool movingRight = true;
    private float moveDistance;
    private float leftEdge = -1.8f; 
    private float rightEdge = 1.8f; 

    private void Start() {
        startPosition = startingWall.position;

        transform.position = new Vector3(startPosition.x, transform.position.y, transform.position.z);
    }

    private void Update() {
        Move();
    }

    private void Move() {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (movingRight && transform.position.x >= rightEdge) {
            movingRight = false;
            startPosition = transform.position; 
        } 
        else if (!movingRight && transform.position.x <= leftEdge) {
            movingRight = true;
            startPosition = transform.position; 
        }
    }
}
