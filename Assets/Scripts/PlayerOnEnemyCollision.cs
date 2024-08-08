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

    private Color[] boom = { Color.red,Color.green,Color.blue,Color.yellow,Color.magenta,Color.cyan,Color.white};
    private int currentColorIndex = 0;
    public float colorChangeInterval = 0.1f;
    private float timer;

    void Start()
    {
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
            timer += Time.deltaTime;

            if (timer >= colorChangeInterval)
            {
                timer = 0f;

                currentColorIndex = (currentColorIndex + 1) % boom.Length;

                leaves.color = boom[currentColorIndex];
                head.color = boom[currentColorIndex];
                rightUpperArm.color = boom[currentColorIndex];
                leftUpperArm.color = boom[currentColorIndex];
                rightLowerArm.color = boom[currentColorIndex];
                leftLowerArm.color = boom[currentColorIndex];
                rightLeg.color = boom[currentColorIndex];
                leftLeg.color = boom[currentColorIndex];
                torso.color = boom[currentColorIndex];
                tail.color = boom[currentColorIndex];
            }
        }
        else if(GameManager.Instance.Invincible == false)
        {
            leaves.color = boom[6];
            head.color = boom[6];
            rightUpperArm.color = boom[6];
            leftUpperArm.color = boom[6];
            rightLowerArm.color = boom[6];
            leftLowerArm.color = boom[6];
            rightLeg.color = boom[6];
            leftLeg.color = boom[6];
            torso.color = boom[6];
            tail.color = boom[6];
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
        else if (collision.gameObject.CompareTag("Enemy") && GameManager.Instance.Invincible == true)
        {
            collision.gameObject.SetActive(false);
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

    private IEnumerator PlayAgainAfterSuper()
    {
        yield return new WaitForSeconds(13);
        audioSource.Pause();
        AudioManager.Instance.audioSource.Play();
    }

    private void super()
    {
        if (AudioManager.Instance.play == true)
        {
            if (GameManager.Instance.Invincible == true)
            {
                audioSource.clip = supah;
                AudioManager.Instance.audioSource.Pause();
                audioSource.Play();
                StartCoroutine(PlayAgainAfterSuper());
            }
        }
    }
}
