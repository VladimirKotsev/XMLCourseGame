using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;
    public Transform playerCamera;

    private float xRotation = 0f;

    public float topClamp = 90f;
    public float bottomClamp = -90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical look — affects only the camera (up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, bottomClamp, topClamp);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal look — affects the player body (left/right)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
