using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class BananaControl : MonoBehaviour
{

    public Animator StarAnimator;           // The Animator component
    public Animator LevelSelectAnimator;
    public Animator RetryAnimator;
    public Animator NextAnimator;
    public Animator MonkeDance;
    public Animator GokuPose;

    public Button LevelSelect;
    public Button Retry;
    public Button Next;

    private AudioSource audioSource;
    public AudioClip yayyy;

    public int levelIndex;

    void Start()
    {
        MonkeDance.gameObject.SetActive(false);
        GokuPose.gameObject.SetActive(false);
        LevelSelect.gameObject.SetActive(false);
        Retry.gameObject.SetActive(false);
        Next.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        GameManager.Instance.SaveStars(levelIndex, 0);
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

            StartCoroutine(MonkeyDanceInator());
        }
    }

    private IEnumerator MonkeyDanceInator()
    {
        yield return new WaitForSeconds(0.5f);
        if (GameManager.Instance.GetStars(levelIndex) <= 2)
        {
            MonkeDance.gameObject.SetActive(true);
        }
        else if(GameManager.Instance.GetStars(levelIndex) == 3)
        {
            GokuPose.gameObject.SetActive(true);
        }
    }
}
