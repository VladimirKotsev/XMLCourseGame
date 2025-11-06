using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Camera playerCamera;

    public float range = 100f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    void Shoot()
    {
        RaycastHit hit;

        // Casts a ray from the center of the camera forward
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name + " with tag: " + hit.transform.tag);

            // Example: detect specific tags
            if (hit.transform.CompareTag("Target"))
            {
                Debug.Log("Target hit! Points should be granted!");
            }
            //To be implemented!!!!
            else if (hit.transform.CompareTag("Reward"))
            {
                Debug.Log("Undestructuble object hit! Points should be taken!");
            }
            //To be implemented!!!!!
            else if (hit.transform.CompareTag("Other"))
            {
                Debug.Log("Target miseed! Points should be taken!");
            }
        }
        else
        {
            Debug.Log("Missed!");
        }
    }
}
