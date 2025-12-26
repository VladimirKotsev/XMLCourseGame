using UnityEngine;

public class ExpertTargetMovement : MonoBehaviour
{
    [Header("Trajectory Settings")]
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed = 11f;

    [Header("Wave Settings")]
    public float waveAmplitude = 2.9f;
    public float waveFrequency = 2.8f;
    // Defaults to Right, but you can change this to Vector3.up if your path is horizontal
    public Vector3 waveAxis = Vector3.right;

    private Vector3 _currentBasePos;
    private Vector3 _currentTargetPos;

    void Start()
    {
        // Initialize the internal tracker at the start position
        _currentBasePos = startPosition;

        // Ensure the object snaps to the start immediately
        transform.position = startPosition;

        // Set the first destination
        _currentTargetPos = endPosition;
    }

    void Update()
    {
        // 1. Move the "Base" position strictly linearly towards the target
        _currentBasePos = Vector3.MoveTowards(_currentBasePos, _currentTargetPos, speed * Time.deltaTime);

        // 2. Calculate the Wave Offset (Sine wave along the chosen axis)
        Vector3 waveOffset = waveAxis * Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

        // 3. Apply the combined position (Linear Path + Wave)
        transform.position = _currentBasePos + waveOffset;

        // 4. Check if the Base Position has reached the target
        // We use a tiny threshold (0.001f) to handle floating point precision
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