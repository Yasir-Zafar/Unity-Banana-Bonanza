using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        // Check if the collision is with an enemy
        if (collision.gameObject.CompareTag("Enemy")) {
            RestartLevel();
        }
    }

    private void RestartLevel() {
        // Get the name of the current scene
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        // Reload the current scene
        SceneManager.LoadScene(currentSceneName);
    }
}
