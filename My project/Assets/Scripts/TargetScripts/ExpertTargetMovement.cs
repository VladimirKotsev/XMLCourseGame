using UnityEngine;

public class ExpertTargetMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 11f;
    public float maxTravelDistance = 50f;
    public Vector3 moveAxis = Vector3.up;

    [Header("Wave Settings")]
    public float waveAmplitude = 2.9f;
    public float waveFrequency = 2.8f;

    private Vector3 startPos;
    private bool movingForward = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 direction = movingForward ? moveAxis.normalized : -moveAxis.normalized;

        Vector3 waveVelocity = Vector3.right * Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

        transform.position += (direction * speed * Time.deltaTime) + (waveVelocity * Time.deltaTime);

        float distanceFromStart = Vector3.Distance(startPos, transform.position);

        if (distanceFromStart >= maxTravelDistance)
        {
            movingForward = !movingForward;
        }
    }
}