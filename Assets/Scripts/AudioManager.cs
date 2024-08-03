using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip Welcome;
    public AudioClip LevelsInGame;

    public AudioSource audioSource;
    private int lastSceneIndex;
    public bool play;

    MusicController musicController;

    private void Awake()
    {
        // Singleton pattern to persist the AudioManager between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Start playing the appropriate audio
        UpdateAudio(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateAudio(scene.buildIndex);
    }

    private void UpdateAudio(int sceneIndex)
    {
        if (sceneIndex == 0 || sceneIndex == 10)
        {
            if (audioSource.clip != Welcome && play == true)
            {
                audioSource.clip = Welcome;
                audioSource.Play();
            }
        }
        else if (sceneIndex >= 1 && sceneIndex <= 9)
        {
            if (audioSource.clip != LevelsInGame && play == true)
            {
                audioSource.clip = LevelsInGame;
                audioSource.Play();
            }
        }

        // Store the last scene index to check if the scene has changed
        lastSceneIndex = sceneIndex;
    }
}
