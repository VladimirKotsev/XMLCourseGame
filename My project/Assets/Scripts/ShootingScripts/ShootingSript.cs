using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Camera playerCamera;
    public Transform weaponHolder;

    public float range = 100f;

    public float recoilAmount = 25f;
    public float recoilReturnSpeed = 4f;
    public float recoilKickback = 0.50f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        originalPosition = weaponHolder.localPosition;
        originalRotation = weaponHolder.localRotation;

        targetPosition = originalPosition;
        targetRotation = originalRotation;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            Shoot();
            ApplyRecoil();

        }

        weaponHolder.localPosition = Vector3.Lerp(weaponHolder.localPosition, targetPosition, recoilReturnSpeed * Time.deltaTime);
        weaponHolder.localRotation = Quaternion.Lerp(weaponHolder.localRotation, targetRotation, recoilReturnSpeed * Time.deltaTime);

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
    void ApplyRecoil()
    {
        float recoilX = Random.Range(-recoilAmount / 4, recoilAmount / 4);
        float recoilY = Random.Range(-recoilAmount / 2, recoilAmount / 2);

        targetRotation = originalRotation * Quaternion.Euler(-recoilAmount, recoilY, recoilX);

        targetPosition = originalPosition - Vector3.forward * recoilKickback;

        Invoke(nameof(ResetRecoil), 0.05f);
    }

    void ResetRecoil()
    {
        targetRotation = originalRotation;
        targetPosition = originalPosition;
    }

}