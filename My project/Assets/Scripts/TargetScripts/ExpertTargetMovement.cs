using UnityEngine;

public class ExpertTargetMovement : MonoBehaviour
{
    public float speed = 10f;
    public float maxTravelDistance = 50f;
    public float stopChancePerSecond = 0.30f;
    public float minStopTime = 0.1f;
    public float maxStopTime = 0.6f;

    public float waveAmplitude = 2.5f;
    public float waveFrequency = 2.5f;
    public Vector3 moveAxis = Vector3.up;

    private Vector3 startPos;
    private bool movingForward = true;
    private bool isStopped = false;
    private float stopTimer = 0f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isStopped)
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0f)
            {
                isStopped = false;
                movingForward = !movingForward;
            }
            return;
        }

        if (Random.value < stopChancePerSecond * Time.deltaTime)
        {
            isStopped = true;
            stopTimer = Random.Range(minStopTime, maxStopTime);
            return;
        }

        Vector3 direction = movingForward ? moveAxis.normalized : -moveAxis.normalized;

        Vector3 waveOffset = Vector3.right * Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

        transform.position += (direction * speed * Time.deltaTime) + waveOffset * Time.deltaTime;

        float distanceFromStart = Vector3.Distance(startPos, transform.position);
        if (distanceFromStart >= maxTravelDistance)
            movingForward = !movingForward;
    }
}
