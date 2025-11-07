using UnityEngine;

public class CurvedTargetMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float amplitude = 2.8f;
    public float frequency = 1.7f;
    public float moveDistance = 100f;

    public Vector3 moveDirection = Vector3.forward;

    private Vector3 startPos;
    private float timeOffset;

    void Start()
    {
        startPos = transform.position;
        timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        transform.position = startPos + moveDirection.normalized * Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        float wave = Mathf.Sin((Time.time + timeOffset) * frequency) * amplitude;
        transform.position += Vector3.up * wave;
    }
}
