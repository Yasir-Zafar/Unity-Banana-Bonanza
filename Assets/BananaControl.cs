using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaControl : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            EndGame();
        }
    }

    private void EndGame() {
        Debug.Log("Game Over!");

        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }
}
