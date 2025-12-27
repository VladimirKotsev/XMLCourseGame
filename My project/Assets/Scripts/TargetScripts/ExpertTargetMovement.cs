using UnityEngine;

public class ExpertTargetMovement : TargetMovement
{
    [Header("Wave Settings")]
    public float waveAmplitude = 2.9f;
    public float waveFrequency = 2.8f;
    public Vector3 waveAxis = Vector3.right;

    private Vector3 _currentBasePos;
    private Vector3 _currentTargetPos;

    void Start()
    {
        _currentBasePos = startPosition;

        transform.position = startPosition;

        _currentTargetPos = endPosition;
    }

    void Update()
    {
        _currentBasePos = Vector3.MoveTowards(_currentBasePos, _currentTargetPos, moveSpeed * Time.deltaTime);

        Vector3 waveOffset = waveAxis * Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

        transform.position = _currentBasePos + waveOffset;

        if (Vector3.Distance(_currentBasePos, _currentTargetPos) < 0.001f)
        {
            // Swap the target to the other point
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