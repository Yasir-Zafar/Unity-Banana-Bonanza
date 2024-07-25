using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonImageChangerWithDelay : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Sprite newImage; // Drag your new image here in the Inspector
    public float delay = 0.035f; // Delay before the image change, in seconds

    private Sprite originalImage; // To store the original image
    private Image buttonImage;
    private Coroutine changeImageCoroutine;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalImage = buttonImage.sprite; // Store the original image
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Start the coroutine to change the button's image after the delay
        changeImageCoroutine = StartCoroutine(ChangeImageAfterDelay());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Stop the coroutine if it's running and revert the button's image immediately
        if (changeImageCoroutine != null)
        {
            StopCoroutine(changeImageCoroutine);
        }
        buttonImage.sprite = originalImage;
    }

    private IEnumerator ChangeImageAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        buttonImage.sprite = newImage;
    }
}
