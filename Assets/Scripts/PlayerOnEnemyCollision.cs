using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

public class PlayerCollision : MonoBehaviour
{
    public Animator StarAnimator;           // The Animator component
    public Animator LevelSelectAnimator;
    public Animator RetryAnimator;
    public Animator SadMonkeAnimator;
    public Animator Dead;

    public Button LevelSelect;
    public Button Retry;
    public Button Next;

    private AudioSource audioSource;
    public AudioClip awww;
    public AudioClip supah;

    public int wtf=0;

    //fk you animator
    public SpriteRenderer leaves;
    public SpriteRenderer head;
    public SpriteRenderer rightUpperArm;
    public SpriteRenderer leftUpperArm;
    public SpriteRenderer rightLowerArm;
    public SpriteRenderer leftLowerArm;
    public SpriteRenderer rightLeg;
    public SpriteRenderer leftLeg;
    public SpriteRenderer torso;
    public SpriteRenderer tail;

    public int levelIndex;

    private Color kaka;
    private Color not_kaka;
    private int currentColorIndex = 0;
    private float timer;

    void Start()
    {
        not_kaka = leaves.color;
        GameManager.Instance.lose = false;
        LevelSelect.gameObject.SetActive(false);
        Retry.gameObject.SetActive(false);
        Next.gameObject.SetActive(false);
        SadMonkeAnimator.gameObject.SetActive(false);
        Dead.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager.Instance.Invincible == true)
        {
            wtf++;
            if (wtf == 1)
            {
                super();
            }

            kaka = leaves.color;
            kaka.a = Mathf.Clamp01(0.5f);
            leaves.color = kaka;
            head.color = kaka;
            rightUpperArm.color = kaka;
            leftUpperArm.color = kaka;
            rightLowerArm.color = kaka;
            leftLowerArm.color = kaka;
            rightLeg.color = kaka;
            leftLeg.color = kaka;
            torso.color = kaka;
            tail.color = kaka;


        }
        else if(GameManager.Instance.Invincible == false)
        {
            leaves.color = not_kaka;
            head.color = not_kaka;
            rightUpperArm.color = not_kaka;
            leftUpperArm.color = not_kaka;
            rightLowerArm.color = not_kaka;
            leftLowerArm.color = not_kaka;
            rightLeg.color = not_kaka;
            leftLeg.color = not_kaka;
            torso.color = not_kaka;
            tail.color = not_kaka;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Add this line
        if (collision.gameObject.CompareTag("Enemy") && GameManager.Instance.Invincible == false)
        {
            leaves.enabled = false;
            head.enabled = false;
            rightUpperArm.enabled = false;
            leftUpperArm.enabled = false;
            rightLowerArm.enabled = false;
            leftLowerArm.enabled = false;
            rightLeg.enabled = false;
            leftLeg.enabled = false;
            torso.enabled = false;
            tail.enabled = false;
            Dead.gameObject.SetActive(true);
            GameManager.Instance.lose = true;
            GameManager.Instance.SaveStars(levelIndex, 0);
            Debug.Log("Collided with Enemy!"); // Add this line
            LoseScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemy") && GameManager.Instance.Invincible == true)
        {
            Debug.Log("Passed through" + collision.gameObject.name);
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

        StartCoroutine(MonkeySadInator());
    }

    private IEnumerator MonkeySadInator()
    {
        yield return new WaitForSeconds(0.5f);
        SadMonkeAnimator.gameObject.SetActive(true);
        SadMonkeAnimator.Play("sad");
    }
    private IEnumerator PlayAgain()
    {
        yield return new WaitForSeconds(4);
        AudioManager.Instance.audioSource.Play();
    }

    private void super()
    {
        if (AudioManager.Instance.play == true)
        {
            if (GameManager.Instance.Invincible == true)
            {
                audioSource.clip = supah;
                audioSource.Play();
            }
        }
    }
}
