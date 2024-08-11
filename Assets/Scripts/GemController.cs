using System.Collections;
using UnityEngine;

public class Gem : MonoBehaviour
{
    Collider2D SmolBananaCollider;
    private int kelas;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            kelas++;
            GameManager.Instance.SaveBananasCollected(kelas);
            GemManager.Instance.CollectGem();
            SmolBananaCollider = GetComponent<Collider2D>();
            Destroy(SmolBananaCollider);
            StartCoroutine(LetBananaFlyAway());
        }
    }

    private IEnumerator LetBananaFlyAway()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
