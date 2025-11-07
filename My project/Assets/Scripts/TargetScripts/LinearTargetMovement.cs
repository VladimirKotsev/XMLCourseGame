using UnityEngine;

public class LinearTargetMovement : MonoBehaviour
{
    public float speed = 5f;
    public float moveDistance = 100f; // how far it should move before turning back

    private Vector3 startPos;
    private bool movingForward = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Move in current direction
        if (movingForward)
            transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
        else
            transform.Translate(-transform.up * speed * Time.deltaTime, Space.World);

        // Check distance traveled
        float distanceFromStart = Vector3.Distance(startPos, transform.position);

        // Reverse direction when reaching max distance
        if (distanceFromStart >= moveDistance)
        {
            movingForward = !movingForward;
            startPos = transform.position; // reset for the next leg
        }
    }
}
