using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    private MouseMovement playerCameraScript;
    private ShootingScript shootingScript;
    private void Start()
    {
        playerCameraScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseMovement>();
        shootingScript = GameObject.FindGameObjectWithTag("PlayerPOV").GetComponent<ShootingScript>();
    }
    private bool isOpen = false;

    public void ToggleInventory()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (playerCameraScript != null && shootingScript != null) 
            {
                playerCameraScript.enabled = false;
                shootingScript.enabled = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (playerCameraScript != null && shootingScript != null)
            {
                playerCameraScript.enabled = true;
                shootingScript.enabled = true;
            }
        }
    }
}
