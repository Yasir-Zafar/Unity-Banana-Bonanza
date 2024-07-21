using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Call this method to load the next level
    public void LoadNextLevel() {
        // Get the current scene index and load the next scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        // Check if the next scene index is within bounds
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            Debug.Log("Yippee");
        }
    }

    // Call this method to load a specific scene by name
    public void LoadLevel(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
