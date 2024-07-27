using UnityEngine;
using System.Collections;

public class BreakableBranch : MonoBehaviour
{
    public float breakTime = 2f; // Time after which the branch breaks
    public float regenerateTime = 3f; // Time after which the branch regenerates
    public GameObject branchBase; // The small portion that remains on the wall
    public float fallDistance = 0.05f; // Distance the branch falls when it breaks
    public float fadeDuration = 0.05f; // Duration of the fade-out and fade-in

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private bool isBroken = false; // Track whether the branch is broken

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        originalPosition = transform.localPosition; // Save the original local position

        if (branchBase != null)
        {
            branchBase.SetActive(false); // Hide the base portion initially
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isBroken)
        {
            StartCoroutine(BreakBranch());
        }
    }

    private IEnumerator BreakBranch()
    {
        yield return new WaitForSeconds(breakTime);

        if (branchBase != null)
        {
            branchBase.SetActive(true); // Show the base portion
        }

        StartCoroutine(FallAndFadeOut());

        isBroken = true;
        StartCoroutine(RegenerateBranch());
    }

    private IEnumerator FallAndFadeOut()
    {
        Vector3 fallPosition = originalPosition + Vector3.down * fallDistance;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            transform.localPosition = Vector3.Lerp(originalPosition, fallPosition, t);

            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(1f, 0f, t);
            spriteRenderer.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = fallPosition;

        spriteRenderer.enabled = false;
        boxCollider.isTrigger = true; // Make the collider a trigger
        rb.simulated = false; // Disable the Rigidbody2D
    }

    private IEnumerator RegenerateBranch()
    {
        yield return new WaitForSeconds(regenerateTime);

        if (branchBase != null)
        {
            branchBase.SetActive(false); // Hide the base portion again
        }

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        spriteRenderer.enabled = true;
        boxCollider.isTrigger = false; // Re-enable the collider as non-trigger
        rb.simulated = true; // Re-enable the Rigidbody2D

        transform.localPosition = originalPosition; // Reset the position before fading in

        float elapsedTime = 0f;
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            color.a = Mathf.Lerp(0f, 1f, t);
            spriteRenderer.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        spriteRenderer.color = color;

        isBroken = false;
    }
}
