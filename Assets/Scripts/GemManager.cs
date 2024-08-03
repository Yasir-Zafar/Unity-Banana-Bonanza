using UnityEngine;

public class GemManager : MonoBehaviour
{
    public static GemManager Instance { get; private set; }
    private float totalGems;
    public AudioClip GemCollected;
    private AudioSource audioSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectGem()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = GemCollected;
        if (AudioManager.Instance.play == true)
        {
            audioSource.Play();
        }
        totalGems++;
        Debug.Log("Total Gems Collected: " + totalGems);
    }

    public void ResetGemCount()
    {
        totalGems = 0;
    }

    public float GetTotalGems()
    {
        return totalGems;
    }
}
