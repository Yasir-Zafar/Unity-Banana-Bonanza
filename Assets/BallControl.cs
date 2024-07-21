using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{    
    public float power = 7f;
    public float maxDrag = 2f;
    public Rigidbody2D rb;
    public LineRenderer lr;

    Vector3 dragStartPos;
    Touch touch;
    private bool grounded;
    private bool onBranch;
    private Transform currentBranch;

    private void Start() {
        Color startColor = lr.startColor;
        Color endColor = lr.endColor;
        startColor.a = 0.1f; 
        endColor.a = 0.1f;
        lr.startColor = startColor;
        lr.endColor = endColor;
    }

    private void Update(){
        if ((grounded || onBranch) && Input.touchCount > 0) {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                DragStart();
            }

            if (touch.phase == TouchPhase.Moved) {
                Dragging();
            }

            if (touch.phase == TouchPhase.Ended) {
                DragRelease();
            }
        }
    }

    void DragStart() {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }
    void Dragging() {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;

        Vector3 dragVector = draggingPos - dragStartPos;
        if (dragVector.magnitude > maxDrag) {
            draggingPos = dragStartPos + dragVector.normalized * maxDrag;
        }

        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }
    void DragRelease() {
        lr.positionCount = 0;

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        rb.AddForce(clampedForce, ForceMode2D.Impulse);
        if (onBranch) {
            onBranch = false;
            currentBranch = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            grounded = true;
        }
        else if (collision.gameObject.CompareTag("Branch")) {
            onBranch = true;
            currentBranch = collision.transform;
            PositionOnBranch();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            grounded = false;
        }
        else if (collision.gameObject.CompareTag("Branch")) {
            onBranch = false;
            currentBranch = null;
        }
    }

    private void PositionOnBranch() {
        if (currentBranch == null) return;

        Vector3 branchCenter = currentBranch.position;

        Vector3 ballPosition = branchCenter;
        ballPosition.y += (transform.localScale.y / 2);  

        rb.velocity = Vector2.zero;  
        rb.angularVelocity = 0f;
        transform.position = ballPosition;
    }

}
