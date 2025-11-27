using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public AudioSource walkingSound;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // We check if the move vector has length (magnitude > 0.1) AND we are on the ground
        if (move.magnitude > 0.1f && isGrounded)
        {
            // Only play if it is not ALREADY playing (prevents stuttering)
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }
        else
        {
            // Stop the sound if we stop moving OR if we jump
            walkingSound.Stop();
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(Vector3.up * velocity.y * Time.deltaTime);
    }
}
