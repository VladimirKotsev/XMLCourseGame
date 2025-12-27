using UnityEngine;

public class LinearTargetMovement: TargetMovement
{
    private Vector3 currentTarget;

    void Start()
    {
        transform.position = startPosition;
        currentTarget = endPosition;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget) < 0.001f)
        {
            if (currentTarget == endPosition)
            {
                currentTarget = startPosition;
            }
            else
            {
                currentTarget = endPosition;
            }
        }
    }
}