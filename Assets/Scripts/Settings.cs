using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Button toggleButton; // Reference to the button to be toggled

    private bool isToggleButtonActive = false;

    void Start()
    {
        // Ensure the MusicButton starts as inactive
        toggleButton.gameObject.SetActive(false);
    }

    public void ToggleButton()
    {
        isToggleButtonActive = !isToggleButtonActive;

        // Set the active state of the MusicButton and its interactable state
        toggleButton.gameObject.SetActive(isToggleButtonActive);
        toggleButton.interactable = isToggleButtonActive;
    }
}
