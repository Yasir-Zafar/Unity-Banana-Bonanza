using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BananaControl : MonoBehaviour
{

    public Animator StarAnimator;           // The Animator component
    public Animator LevelSelectAnimator;
    public Animator HomeAnimator;
    public Animator RetryAnimator;
    public Animator NextAnimator;

    public Button Home;
    public Button LevelSelect;
    public Button Retry;
    public Button Next;

    void Start()
    {
        Home.gameObject.SetActive(false);
        LevelSelect.gameObject.SetActive(false);
        Retry.gameObject.SetActive(false);
        Next.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StarAnimator.Play("StarsSpawn");

            Home.gameObject.SetActive(true);
            HomeAnimator.Play("HomeSpawn");

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
