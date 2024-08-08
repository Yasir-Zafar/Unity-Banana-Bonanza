using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Sorcery, basically a magic instance that can be used throughout scenes
    public static GameManager Instance { get; private set; }

    public int[] stars = new int[9];
    public bool lose;
    public float niggaPower;

    private void Awake()
    {
        // Implementing the Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGameData();
        }
    }

    // Method to load game data, including starsCount for each level
    private void LoadGameData()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i] = PlayerPrefs.GetInt($"Level{i + 1}Stars", 0); // Default to 0 if not set
        }
    }

    // Method to save starsCount for a specific level
    public void SaveStars(int levelIndex, int starCount)
    {
        if (levelIndex >= 0 && levelIndex < stars.Length)
        {
            stars[levelIndex] = Mathf.Clamp(starCount, 1, 3);
            PlayerPrefs.SetInt($"Level{levelIndex + 1}Stars", stars[levelIndex]);
            PlayerPrefs.Save();
        }
    }

    // Method to retrieve starsCount for a specific level
    public int GetStars(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < stars.Length)
        {
            return stars[levelIndex];
        }
        return 0; // Default to 0 if out of range
    }

    public void ResetGameData()
    {
        // Reset stars array to default value (e.g., 0 stars for each level)
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i] = 0; // Reset to 0 stars or a default value
            PlayerPrefs.SetInt($"Level{i + 1}Stars", 0);
        }

        // Save the changes in PlayerPrefs
        PlayerPrefs.Save();
    }

    public int niggaBars()
    {
        if (niggaPower == 0)
        {
            return 0;
        }
        else if (niggaPower > 0 && niggaPower <= 1.0466)
        {
            return 1;
        }
        else if (niggaPower > 1.0466 && niggaPower <= 2.0932)
        {
            return 2;
        }
        else if (niggaPower > 2.0932 && niggaPower <= 3.05)
        {
            return 3;
        }
        else if (niggaPower > 3.05)
        {
            return 4;
        }
        else
        {
            return 100;
        }
    }
}
