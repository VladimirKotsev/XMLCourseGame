using UnityEngine;

public class LinearTargetMovement : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed = 7f;

    private Vector3 currentTarget;

    void Start()
    {
        transform.position = startPosition;
        currentTarget = endPosition;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

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