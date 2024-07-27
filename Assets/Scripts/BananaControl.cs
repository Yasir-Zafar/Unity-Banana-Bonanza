using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BananaControl : MonoBehaviour
{
    public float delay = 3.0f;

    public Animator animatorF;     // The Animator component
    public string animationNameF;  // The name of the animation to play
    public Animator animatorSF;
    public string animationNameSF;
    public Animator animatorBG;
    public string animationNameBG;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animatorF.Play(animationNameF);
            animatorSF.Play(animationNameSF);
            animatorBG.Play(animationNameBG);
            EndGame();
        }
    }

    private void EndGame()
    {
        StartCoroutine(LoadLevelAfterDelay());
    }
    private IEnumerator LoadLevelAfterDelay()
    {
        SceneController sceneController = FindObjectOfType<SceneController>();
        yield return new WaitForSeconds(delay); //delay
        if (sceneController != null)
        {
            sceneController.LoadNextLevel();
        }
    }
}
