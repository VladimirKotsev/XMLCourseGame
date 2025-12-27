using UnityEngine;

public class CurvedTargetMovement : TargetMovement
{
    [Header("Wave Settings")]
    public float amplitude = 2.8f;
    public float frequency = 1.7f;
    // Defaults to Up to match your original script, 
    // but you can change it to Vector3.right if moving vertically.
    public Vector3 waveAxis = Vector3.up;

    private Vector3 _currentBasePos;
    private Vector3 _currentTargetPos;
    private float _timeOffset;

    void Start()
    {
        // initialize positions
        _currentBasePos = startPosition;
        transform.position = startPosition;
        _currentTargetPos = endPosition;

        // Keep the random offset so multiple objects don't wave in perfect sync
        _timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        // 1. Move the invisible "base" point linearly between start and end
        _currentBasePos = Vector3.MoveTowards(_currentBasePos, _currentTargetPos, moveSpeed * Time.deltaTime);

        // 2. Calculate the wave offset (Sine wave)
        // We add _timeOffset so every object has a slightly different wave phase
        float waveValue = Mathf.Sin((Time.time + _timeOffset) * frequency) * amplitude;
        Vector3 waveOffset = waveAxis * waveValue;

        // 3. Apply final position
        transform.position = _currentBasePos + waveOffset;

        // 4. Check if we reached the target to swap direction
        if (Vector3.Distance(_currentBasePos, _currentTargetPos) < 0.001f)
        {
            // Toggle between Start and End
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