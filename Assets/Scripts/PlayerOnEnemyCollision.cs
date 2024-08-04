using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    public Animator StarAnimator;           // The Animator component
    public Animator LevelSelectAnimator;
    public Animator RetryAnimator;

    public Button LevelSelect;
    public Button Retry;
    public Button Next;

    private AudioSource audioSource;
    public AudioClip awww;

    void Start()
    {
        LevelSelect.gameObject.SetActive(false);
        Retry.gameObject.SetActive(false);
        Next.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Add this line
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Enemy!"); // Add this line
            LoseScreen();
        }
    }

    private void LoseScreen()
    {
        audioSource.clip = awww;
        if (AudioManager.Instance.play == true)
        {
            AudioManager.Instance.audioSource.Pause();
            StartCoroutine(PlayAgain());
            audioSource.Play();
        }
        StarAnimator.Play("StarsSpawn");

        LevelSelect.gameObject.SetActive(true);
        LevelSelectAnimator.Play("LevelSelectSpawn");

        Retry.gameObject.SetActive(true);
        RetryAnimator.Play("RetrySpawn");
    }

    private IEnumerator PlayAgain()
    {
        yield return new WaitForSeconds(4);
        AudioManager.Instance.audioSource.Play();
    }
}
