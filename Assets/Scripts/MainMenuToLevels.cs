using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuToLevels : MonoBehaviour
{
    public float delay = 5.0f;

    // This function should be called when the "Play Button" is pressed
    public void LoadLevel1WithDelay()
    {
        StartCoroutine(LoadLevelAfterDelay());
    }

    // Coroutine to handle the delay
    private IEnumerator LoadLevelAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level 1"); // Replace "Level1" with the exact name of your level 1 scene
    }
}
