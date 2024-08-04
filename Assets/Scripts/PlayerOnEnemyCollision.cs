using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Add this line
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Enemy!"); // Add this line
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        PersistentManager.Instance.IsRestarting = true;
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().name));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
