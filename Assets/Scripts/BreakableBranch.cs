using UnityEngine;
using System.Collections;

public class BreakableBranch : MonoBehaviour
{
    public float breakTime = 2f; 
    public float regenerateTime = 3f; 
    public GameObject branchBase; 
    public float fallDistance = 0.05f;
    public float fadeDuration = 0.05f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private bool isBroken = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        originalPosition = transform.localPosition;

        if (branchBase != null)
        {
            branchBase.SetActive(false); 
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
            branchBase.SetActive(true); 
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
        boxCollider.isTrigger = true;
        rb.simulated = false; 
    }

    private IEnumerator RegenerateBranch()
    {
        yield return new WaitForSeconds(regenerateTime);

        if (branchBase != null)
        {
            branchBase.SetActive(false); 
        }

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        spriteRenderer.enabled = true;
        boxCollider.isTrigger = false; 
        rb.simulated = true; 

        transform.localPosition = originalPosition; 

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
