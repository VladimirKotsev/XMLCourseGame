using UnityEngine;

public class ExpertTargetMovement : TargetMovement
{
    [Header("Expert Movement Settings")]
    [Tooltip("Number of zigzag curves along the path")]
    public int zigzagCount = 3;

    [Tooltip("How far the balloon deviates from the straight path")]
    public float zigzagAmplitude = 3f;

    [Tooltip("Adds spiral motion complexity")]
    public float spiralIntensity = 1.5f;

    [Tooltip("Speed variation factor (0 = constant, 1 = highly variable)")]
    [Range(0f, 1f)]
    public float speedVariation = 0.4f;

    [Tooltip("How smooth the speed changes are")]
    public float speedSmoothness = 2f;

    private float _journeyLength;
    private float _distanceTraveled;
    private Vector3 _pathDirection;
    private Vector3 _perpendicular1;
    private Vector3 _perpendicular2;
    private float _randomSeed;
    private bool _movingToEnd = true;

    void Start()
    {
        transform.position = startPosition;
        _randomSeed = Random.Range(0f, 1000f);
        CalculatePathData();
    }

    void CalculatePathData()
    {
        _journeyLength = Vector3.Distance(startPosition, endPosition);
        _distanceTraveled = 0f;

        Vector3 currentStart = _movingToEnd ? startPosition : endPosition;
        Vector3 currentEnd = _movingToEnd ? endPosition : startPosition;

        _pathDirection = (currentEnd - currentStart).normalized;

        // Calculate two perpendicular axes for complex 3D motion
        _perpendicular1 = Vector3.Cross(_pathDirection, Vector3.forward).normalized;
        if (_perpendicular1.magnitude < 0.1f)
            _perpendicular1 = Vector3.Cross(_pathDirection, Vector3.up).normalized;

        _perpendicular2 = Vector3.Cross(_pathDirection, _perpendicular1).normalized;
    }

    void Update()
    {
        if (_journeyLength < 0.01f) return;

        // Calculate progress (0 to 1)
        float progress = _distanceTraveled / _journeyLength;

        // Dynamic speed based on progress with smooth variation
        float speedCurve = 1f + Mathf.Sin((progress + _randomSeed) * speedSmoothness * Mathf.PI) * speedVariation;
        float currentSpeed = moveSpeed * speedCurve;

        // Move along the path
        _distanceTraveled += currentSpeed * Time.deltaTime;
        _distanceTraveled = Mathf.Clamp(_distanceTraveled, 0f, _journeyLength);

        // Recalculate progress after movement
        progress = _distanceTraveled / _journeyLength;

        // Base position along straight line
        Vector3 currentStart = _movingToEnd ? startPosition : endPosition;
        Vector3 currentEnd = _movingToEnd ? endPosition : startPosition;
        Vector3 basePosition = Vector3.Lerp(currentStart, currentEnd, progress);

        // Expert pattern: Sine wave zigzag with increasing/decreasing amplitude
        float zigzagPhase = progress * zigzagCount * Mathf.PI * 2f;
        float amplitudeEnvelope = Mathf.Sin(progress * Mathf.PI); // Peaks in the middle
        float zigzag = Mathf.Sin(zigzagPhase + _randomSeed) * zigzagAmplitude * amplitudeEnvelope;

        // Spiral motion for extra difficulty
        float spiralPhase = progress * Mathf.PI * 4f;
        float spiralX = Mathf.Cos(spiralPhase + _randomSeed) * spiralIntensity * amplitudeEnvelope;
        float spiralY = Mathf.Sin(spiralPhase * 1.3f + _randomSeed) * spiralIntensity * amplitudeEnvelope;

        // Combine offsets
        Vector3 offset = _perpendicular1 * (zigzag + spiralX) + _perpendicular2 * spiralY;

        // Apply final position
        transform.position = basePosition + offset;

        // Gentle rotation based on progress
        float rotationAngle = Mathf.Sin(progress * Mathf.PI * 6f) * 20f * Time.deltaTime;
        transform.Rotate(Vector3.forward, rotationAngle);

        // Check if reached the end
        if (progress >= 0.999f)
        {
            // Snap to exact end position
            transform.position = currentEnd;

            // Reverse direction
            _movingToEnd = !_movingToEnd;
            CalculatePathData();
        }
    }
}