using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainMenuToLevels : MonoBehaviour
{
    public float delay = 10.0f;

    public Button playButton;         // The button that will be pressed
    public Animator animatorF;     // The Animator component
    public string animationNameF;  // The name of the animation to play
    public Animator animatorSF;
    public string animationNameSF;
    public Animator animatorBG;
    public string animationNameBG;

    void Start()
    {
        // Ensure the button and animator are set
        if (playButton == null || animatorF == null || animatorSF == null || animatorBG == null)
        {
            return;
        }

        // Add a listener to the button to call the PlayAnimation method when clicked
        playButton.onClick.AddListener(PlayAnimation);
    }


    void PlayAnimation()
    {
        // Play the specified animation
        animatorF.Play(animationNameF);
        animatorSF.Play(animationNameSF);
        animatorBG.Play(animationNameBG);
    }

    // This function should be called when the "Play Button" is pressed
    public void LoadLevel1WithDelay()
    {
        StartCoroutine(LoadLevelAfterDelay());
    }

    // Coroutine to handle the delay
    private IEnumerator LoadLevelAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level 1"); // Replace "Level1" with the exact name of your level 1 scene
    }
}
