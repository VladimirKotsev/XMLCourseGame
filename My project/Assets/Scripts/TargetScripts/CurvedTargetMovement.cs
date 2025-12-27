using UnityEngine;

public class CurvedTargetMovement : TargetMovement
{
    [Header("Wave Settings")]
    public float amplitude = 2.8f;
    public float frequency = 1.7f;
    public Vector3 waveAxis = Vector3.up;

    private Vector3 _currentBasePos;
    private Vector3 _currentTargetPos;
    private float _timeOffset;

    void Start()
    {
        _currentBasePos = startPosition;
        transform.position = startPosition;
        _currentTargetPos = endPosition;

        _timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        _currentBasePos = Vector3.MoveTowards(_currentBasePos, _currentTargetPos, moveSpeed * Time.deltaTime);

        float waveValue = Mathf.Sin((Time.time + _timeOffset) * frequency) * amplitude;
        Vector3 waveOffset = waveAxis * waveValue;

        transform.position = _currentBasePos + waveOffset;
        if (Vector3.Distance(_currentBasePos, _currentTargetPos) < 0.001f)
        {
            if (_currentTargetPos == endPosition)
            {
                _currentTargetPos = startPosition;
            }
            else
            {
                _currentTargetPos = endPosition;
            }
        }
    }
}