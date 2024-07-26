using UnityEngine;
using UnityEngine.UI;

public class Music_On_Off : MonoBehaviour
{
    public Sprite image1; // First image
    public Sprite image2; // Second image

    private Image buttonImage;
    private bool isImage1Active = true; // Track which image is currently active

    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = image1; // Set the initial image
    }

    public void ToggleImage()
    {
        if (isImage1Active)
        {
            buttonImage.sprite = image2;
        }
        else
        {
            buttonImage.sprite = image1;
        }
        isImage1Active = !isImage1Active; // Toggle the flag
    }
}
