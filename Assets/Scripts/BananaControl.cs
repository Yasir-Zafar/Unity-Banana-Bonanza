using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BananaControl : MonoBehaviour
{

    public Animator StarAnimator;           // The Animator component
    public Animator LevelSelectAnimator;
    public Animator RetryAnimator;
    public Animator NextAnimator;

    public Button LevelSelect;
    public Button Retry;
    public Button Next;

    private AudioSource audioSource;
    public AudioClip yayyy;

    void Start()
    {
        LevelSelect.gameObject.SetActive(false);
        Retry.gameObject.SetActive(false);
        Next.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.clip = yayyy;
            if (AudioManager.Instance.play == true)
            {
                audioSource.Play();
            }
            StarAnimator.Play("StarsSpawn");

            LevelSelect.gameObject.SetActive(true);
            LevelSelectAnimator.Play("LevelSelectSpawn");

            Retry.gameObject.SetActive(true);
            RetryAnimator.Play("RetrySpawn");

            Next.gameObject.SetActive(true);
            NextAnimator.Play("NextSpawn");

            collision.gameObject.SetActive(false);
        }
    }
}
