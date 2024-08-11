using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LvlSlctToHome : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay;

    public Button HomeButton;         // The button that will be pressed
    public Animator animatorF;     // The Animator component
    public string animationNameF;  // The name of the animation to play
    public Animator animatorSF;
    public string animationNameSF;
    public Animator animatorBG;
    public string animationNameBG;

    void Start()
    {
        // Ensure the button and HomeAnimator are set
        if (HomeButton == null || animatorF == null || animatorSF == null || animatorBG == null)
        {
            return;
        }

        // Add a listener to the button to call the PlayAnimation method when clicked
        HomeButton.onClick.AddListener(PlayAnimation);
    }


    void PlayAnimation()
    {
        // Play the specified animation
        animatorF.Play(animationNameF);
        animatorSF.Play(animationNameSF);
        animatorBG.Play(animationNameBG);
    }

    // This function should be called when the "Play button" is pressed
    public void HomeWithDelay()
    {
        StartCoroutine(HomeFr());
    }

    // Coroutine to handle the delay
    private IEnumerator HomeFr()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Main Menu");
    }

    public void ShopWithDelay()
    {
        StartCoroutine(ShopFr());
    }

    private IEnumerator ShopFr()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Shop");
    }

    public void ShopToLvlSelectWithDelay()
    {
        StartCoroutine(ShopToLvlSelectFR());
    }

    private IEnumerator ShopToLvlSelectFR()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level Select");
    }
}