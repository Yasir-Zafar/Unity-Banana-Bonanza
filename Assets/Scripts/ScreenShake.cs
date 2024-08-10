using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.01f;

    private Vector3 originalPosition;
    private float shakeTime;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            transform.position = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0;
            transform.position = originalPosition;
        }
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        shakeTime = duration;
    }
}
