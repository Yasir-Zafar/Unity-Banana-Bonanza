using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float power = 7f;
    public float sensitivity = 2f;
    public float maxForce = 15f;
    public float maxLineHeight = 5f;
    public float maxLineWidth = 3f;
    public float glideSpeed = 5f;
    public Rigidbody2D rb;
    public LineRenderer lr;

    public static bool touchControlsEnabled = true; 

    Vector3 dragStartPos;
    Touch touch;
    private bool grounded;
    private bool onBranch;
    private Transform currentBranch;
    private Camera mainCamera;

    public int totalSwipes = 0;

    private void Start() {
        mainCamera = Camera.main; // Cache the Camera.main reference
        
        if (lr != null) {
            Color startColor = lr.startColor;
            Color endColor = lr.endColor;
            startColor.a = 0.1f;
            endColor.a = 0.1f;
            lr.startColor = startColor;
            lr.endColor = endColor;

            lr.widthMultiplier = 0.5f;
            lr.positionCount = 0;
        } else {
            Debug.LogWarning("LineRenderer is not assigned!");
        }
    }

    private void Update() {
        if ((touchControlsEnabled && (grounded || onBranch)) && Input.touchCount > 0) {
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
        dragStartPos = mainCamera.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(0, transform.position); 
        lr.SetPosition(1, transform.position); 
    }

    void Dragging() {
        if (lr == null || lr.positionCount < 2) return;

        Vector3 draggingPos = mainCamera.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;

        Vector3 dragVector = draggingPos - dragStartPos;

        Vector3 endPos = transform.position - dragVector;
        endPos.x = Mathf.Clamp(endPos.x, transform.position.x - maxLineWidth, transform.position.x + maxLineWidth);
        endPos.y = Mathf.Clamp(endPos.y, transform.position.y, transform.position.y + maxLineHeight);

        lr.SetPosition(0, transform.position); 
        lr.SetPosition(1, endPos);
    }

    void DragRelease() {
        lr.positionCount = 0;

        Vector3 dragReleasePos = mainCamera.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, sensitivity) * power;

        clampedForce = Vector3.ClampMagnitude(clampedForce, maxForce);

        rb.AddForce(clampedForce, ForceMode2D.Impulse);
        if (onBranch) {
            onBranch = false;
            currentBranch = null;
        }

        totalSwipes++;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collision with: " + collision.gameObject.name);

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        if (collision.gameObject.CompareTag("Ground")) {
            grounded = true;
            Debug.Log("Ball is grounded.");
        } else if (collision.gameObject.CompareTag("Branch")) {
            onBranch = true;
            currentBranch = collision.transform;
            StartCoroutine(GlideToBranchCenter(collision.contacts[0].point));
            Debug.Log("Ball is on a branch.");
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Branch")) {
            if (rb.velocity.magnitude < 0.1f && rb.angularVelocity < 0.1f) {
                onBranch = true;
                currentBranch = collision.transform;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            grounded = false;
            Debug.Log("Ball left the ground.");
        } else if (collision.gameObject.CompareTag("Branch")) {
            StartCoroutine(CheckBranchContact());
            Debug.Log("Ball might have left the branch.");
        }
    }

    private IEnumerator CheckBranchContact() {
        yield return new WaitForFixedUpdate();

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        bool stillOnBranch = false;

        foreach (Collider2D hitCollider in hitColliders) {
            if (hitCollider.CompareTag("Branch")) {
                stillOnBranch = true;
                currentBranch = hitCollider.transform;

                if (rb.velocity.magnitude < 0.1f && rb.angularVelocity < 0.1f) {
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                }

                break;
            }
        }

        if (!stillOnBranch) {
            onBranch = false;
            currentBranch = null;
        }
    }

    private IEnumerator GlideToBranchCenter(Vector2 contactPoint) {
        if (currentBranch == null) yield break;

        Vector3 branchCenter = currentBranch.position;
        Vector3 startPos = transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < 1f) {
            elapsedTime += Time.deltaTime * glideSpeed;
            float t = Mathf.Clamp01(elapsedTime);

            transform.position = Vector3.Lerp(startPos, branchCenter, t);

            yield return null;
        }
        
        transform.position = branchCenter;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
