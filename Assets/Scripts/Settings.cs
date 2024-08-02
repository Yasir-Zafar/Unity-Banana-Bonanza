using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Button MusicButton;
    public Button FactoryResetButton;

    private bool isMusicButtonActive = false;
    private bool isFactoryResetActive = false;

    void Start()
    {
        MusicButton.gameObject.SetActive(false);
        FactoryResetButton.gameObject.SetActive(false);
    }

    public void ToggleButton()
    {
        isMusicButtonActive = !isMusicButtonActive;
        isFactoryResetActive = !isFactoryResetActive;

        MusicButton.gameObject.SetActive(isMusicButtonActive);
        MusicButton.interactable = isMusicButtonActive;

        FactoryResetButton.gameObject.SetActive(isFactoryResetActive);
        FactoryResetButton.interactable = isFactoryResetActive;
    }
}
