using UnityEngine;

public class EnemyPassThrough : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.Invincible == true)
        {
            Collider2D[] childColliders = GetComponentsInChildren<Collider2D>();

            foreach (Collider2D collider in childColliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    collider.isTrigger = true;
                }
            }
        }
        else if (GameManager.Instance.Invincible == false)
        {
            Collider2D[] childColliders = GetComponentsInChildren<Collider2D>();

            foreach (Collider2D collider in childColliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    collider.isTrigger = false;
                }
            }
        }
    }
}