using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatAmplitude = 0.1f;
    public float floatFrequency = 1.5f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
