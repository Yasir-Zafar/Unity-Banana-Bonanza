using UnityEngine;
using System.Collections;

public class PantherSwipe : MonoBehaviour
{
    public Transform swipeArcParent;
    public GameObject swipeArc;
    public float swipeAngle = 30f;
    public float swipeSpeed = 2f;
    public float swipeInterval = 1f;
    public float visibilityDuration = 0.1f;
    public bool swipeFromTopToBottom = true; 

    private bool isSwiping = false;
    private float startAngle;
    private float endAngle;
    private float currentAngle;
    private LineRenderer swipeArcLineRenderer;
    private SpriteRenderer swipeArcSpriteRenderer;
    private Collider2D swipeArcCollider;

    private void Start()
    {
        if (swipeFromTopToBottom)
        {
            startAngle = -swipeAngle / 2;
            endAngle = swipeAngle / 2;
        }
        else
        {
            startAngle = swipeAngle / 2;
            endAngle = -swipeAngle / 2;
        }

        swipeArcLineRenderer = swipeArc.GetComponent<LineRenderer>();
        swipeArcSpriteRenderer = swipeArc.GetComponent<SpriteRenderer>();
        swipeArcCollider = swipeArc.GetComponent<Collider2D>();

        if (swipeArcLineRenderer != null)
            swipeArcLineRenderer.enabled = false;
        if (swipeArcSpriteRenderer != null)
            swipeArcSpriteRenderer.enabled = false;
        if (swipeArcCollider != null)
            swipeArcCollider.enabled = false;

        StartCoroutine(SwipeRoutine());
    }

    private IEnumerator SwipeRoutine()
    {
        while (true)
        {
            if (!isSwiping)
            {
                SetSwipeArcVisibility(true);

                yield return new WaitForSeconds(visibilityDuration);

                yield return StartCoroutine(Swipe());

                SetSwipeArcVisibility(false);
            }
            yield return new WaitForSeconds(swipeInterval);
        }
    }

    private IEnumerator Swipe()
    {
        isSwiping = true;
        currentAngle = startAngle;

        while (swipeFromTopToBottom ? currentAngle < endAngle : currentAngle > endAngle)
        {
            currentAngle += swipeFromTopToBottom ? Time.deltaTime * swipeSpeed * swipeAngle : -Time.deltaTime * swipeSpeed * swipeAngle;
            swipeArcParent.localRotation = Quaternion.Euler(0f, 0f, currentAngle);
            yield return null;
        }

        swipeArcParent.localRotation = Quaternion.Euler(0f, 0f, startAngle);
        isSwiping = false;
    }

    private void SetSwipeArcVisibility(bool visible)
    {
        if (swipeArcLineRenderer != null)
            swipeArcLineRenderer.enabled = visible;
        if (swipeArcSpriteRenderer != null)
            swipeArcSpriteRenderer.enabled = visible;
        if (swipeArcCollider != null)
            swipeArcCollider.enabled = visible;
    }
}
