using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    public float power = 7f;
    public float sensitivity = 2f;
    public float maxForce = 10f;
    public float maxLineHeight = 5f;
    public float maxLineWidth = 3f;
    public float glideSpeed = 5f;
    public Rigidbody2D rb;
    public LineRenderer lr;

    public LineRenderer ringRenderer;
    public float ringRadius = 0.3f;
    public int ringSegmentCount = 100;

    public static bool touchControlsEnabled = true; 

    private Animator animator;
    Vector3 dragStartPos;
    Touch touch;
    private bool grounded;
    private bool onBranch;
    private Transform currentBranch;
    private Camera mainCamera;

    public float lineStartWidth = 0.05f;
    public float lineEndWidth = 1.0f;
    private float directionBuffer = 0.1f;
    private Vector3 previousDirection;

    public Animator MHMAttempt;
    private AudioSource audioSource;
    public AudioClip Collect;

    private ScreenShake screenShake;

    private void Start()
    {
        mainCamera = Camera.main; // Cache the Camera.main reference
        animator = GetComponent<Animator>();

        GameManager.Instance.PowerUp1 = false;
        GameManager.Instance.PowerUp2 = false;

        lr.startWidth = lineStartWidth;
        lr.endWidth = lineEndWidth;
        
        if (lr != null) {
            Color startColor = lr.startColor;
            Color endColor = lr.endColor;
            startColor.a = 0.1f;
            endColor.a = 0.1f;
            lr.startColor = startColor;
            lr.endColor = endColor;

            Color ringColor = ringRenderer.startColor;
            ringColor.a = 0.5f;
            ringRenderer.startColor = ringColor;
            ringRenderer.endColor = ringColor;

            lr.widthMultiplier = 0.6f;
            lr.positionCount = 0;
        } else {
            Debug.LogWarning("LineRenderer is not assigned!");
        }

        if (mainCamera != null)
        {
            screenShake = mainCamera.GetComponent<ScreenShake>();
        }

        MHMAttempt.Play("monkey_idle");
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
        lr.SetPosition(0, dragStartPos); // Set the starting point at the touch position
        lr.SetPosition(1, dragStartPos);

        DrawRing(dragStartPos); 

        MHMAttempt.Play("monkey_idle");
    }

    void Dragging() {
        if (lr == null || lr.positionCount < 2) return;

        Vector3 draggingPos = mainCamera.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;

        Vector3 dragVector = draggingPos - dragStartPos;

        Vector3 invertedDragVector = -dragVector;

        float maxDragLength = maxLineHeight; 
        if (invertedDragVector.magnitude > maxDragLength) {
            invertedDragVector = invertedDragVector.normalized * maxDragLength;
        }

        Vector3 endPos = dragStartPos + invertedDragVector;

        lr.SetPosition(0, dragStartPos);
        lr.SetPosition(1, endPos);

        FlipSprite(dragVector);
    }

    void DragRelease() {
        GameManager.Instance.niggaPower = 0;
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

        ClearRing();

        MHMAttempt.Play("MHMAttemptForJump");
        TriggerImpactEffect();
    }

    void ClearRing() {
        if (ringRenderer != null) {
            ringRenderer.positionCount = 0; // Clear the ring
        }
    }

    private void TriggerImpactEffect() {
        if (screenShake != null) {
            screenShake.Shake(0.1f, 0.014f); // Adjust duration and magnitude as needed
        }
    }  

    void DrawRing(Vector3 center) {
        if (ringRenderer == null) {
            Debug.LogWarning("Ring Renderer is not assigned!");
            return;
        }

        ringRenderer.positionCount = ringSegmentCount + 1;
        float angle = 0f;

        for (int i = 0; i <= ringSegmentCount; i++) {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * ringRadius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * ringRadius;

            ringRenderer.SetPosition(i, new Vector3(x, y, 0) + center);
            angle += (360f / ringSegmentCount);
        }

        ringRenderer.loop = true; // Ensure the ring connects back to the start
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collision with: " + collision.gameObject.name);

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            Debug.Log("Ball is grounded.");
            MHMAttempt.Play("monkey_idle");
        } 
        else if (collision.gameObject.CompareTag("Branch")) 
        {
            onBranch = true;
            currentBranch = collision.transform;
            StartCoroutine(GlideToBranchCenter(collision.contacts[0].point));
            Debug.Log("Ball is on a branch.");
            MHMAttempt.Play("monkey_idle");
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

    private void TeleportToBranchCenter()
    {
        if (currentBranch == null) return;

        transform.position = currentBranch.position;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    private void FlipSprite(Vector3 dragVector) {
        float direction = dragVector.x;
        if (Mathf.Abs(direction - previousDirection.x) > directionBuffer) {
            Vector3 localScale = transform.localScale;
            if (direction < 0 && localScale.x != 1) {
                transform.localScale = new Vector3(1.2f, localScale.y, localScale.z);
            } else if (direction > 0 && localScale.x != -1) {
                transform.localScale = new Vector3(-1.2f, localScale.y, localScale.z);
            }
            previousDirection = dragVector;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Invincibility"))
        {
            GameManager.Instance.Invincible = true;
            collision.gameObject.SetActive(false);
            StartCoroutine(endInv());
        }
        else if (collision.gameObject.CompareTag("PowerUp1"))
        {
            GameManager.Instance.PowerUp1 = true;
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = Collect;
            audioSource.Play();
        }
        else if(collision.gameObject.CompareTag("PowerUp2"))
        {
            GameManager.Instance.PowerUp2 = true;
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = Collect;
            audioSource.Play();
        }
    }

    private IEnumerator endInv()
    {
        yield return new WaitForSeconds(9);
        GameManager.Instance.Invincible = false;
    }
}