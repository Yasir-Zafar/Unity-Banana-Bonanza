using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource
    public Button MusicButton; // Reference to the UI button
    private bool isPlaying = true; // To keep track of the audio state

    void Start()
    {
        // Ensure the button and audio source are assigned
        if (MusicButton != null && audioSource != null)
        {
            // Add a listener to the button to call the ToggleAudio method when clicked
            MusicButton.onClick.AddListener(ToggleAudio);
        }
    }

    void ToggleAudio()
    {
        if (isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
        isPlaying = !isPlaying;
    }
}

