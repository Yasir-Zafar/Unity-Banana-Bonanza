using System.Collections;
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

            collision.gameObject.SetActive(false);

            EndGame();
        }
    }

    private void EndGame()
    {
        StartCoroutine(LoadLevelAfterDelay());
    }

    private IEnumerator LoadLevelAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        
        SceneController sceneController = FindObjectOfType<SceneController>();
        if (sceneController != null)
        {
            sceneController.LoadNextLevel();
        }
    }
}
