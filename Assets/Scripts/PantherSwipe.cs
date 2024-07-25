using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PantherSwipe : MonoBehaviour
{
    public Transform swipeArcParent; 
    public GameObject swipeArc; 
    public float swipeAngle = 30f;
    public float swipeSpeed = 2f; 
    public float swipeInterval = 1f; 

    private bool isSwiping = false;
    private float startAngle;
    private float endAngle;
    private float currentAngle;
    private Renderer swipeArcRenderer;
    private Collider2D swipeArcCollider;

    private void Start()
    {
        startAngle = -swipeAngle / 2;
        endAngle = swipeAngle / 2;
        swipeArcRenderer = swipeArc.GetComponent<Renderer>();
        swipeArcCollider = swipeArc.GetComponent<Collider2D>();
        
        swipeArcRenderer.enabled = false; 
        swipeArcCollider.enabled = false; 

        StartCoroutine(SwipeRoutine());
    }

    private IEnumerator SwipeRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(Swipe());
            swipeArcRenderer.enabled = false; 
            swipeArcCollider.enabled = false; 
            yield return new WaitForSeconds(swipeInterval);
            swipeArcRenderer.enabled = true; 
            swipeArcCollider.enabled = true; 
        }
    }

    private IEnumerator Swipe()
    {
        isSwiping = true;
        currentAngle = startAngle;
        while (currentAngle < endAngle)
        {
            currentAngle += Time.deltaTime * swipeSpeed * swipeAngle;
            swipeArcParent.localRotation = Quaternion.Euler(0f, 0f, currentAngle);
            yield return null;
        }
        swipeArcParent.localRotation = Quaternion.Euler(0f, 0f, startAngle);
        isSwiping = false;
    }
}
