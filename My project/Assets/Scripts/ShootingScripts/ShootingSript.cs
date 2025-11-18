using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Camera playerCamera;
    public Transform weaponHolder;

    //public float range = 100f;

    //public float recoilAmount = 25f;
    //public float recoilReturnSpeed = 4f;
    //public float recoilKickback = 0.50f;

    private GunManager gunManager;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        gunManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GunManager>();
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

        weaponHolder.localPosition = Vector3.Lerp(weaponHolder.localPosition, targetPosition, CurrentGun().RecoilReturnSpeed * Time.deltaTime);
        weaponHolder.localRotation = Quaternion.Lerp(weaponHolder.localRotation, targetRotation, CurrentGun().RecoilReturnSpeed * Time.deltaTime);
    }

    private GameObject MuzzleFlash()
    {
        return this.CurrentGun().MuzzleFlash;
    }

    private Gun CurrentGun()
    {
        return this.gunManager.CurrentGun;
    }

    void Shoot()
    {
        // Enable muzzle flash briefly
        MuzzleFlash().SetActive(true);
        Invoke(nameof(DisableMuzzleFlash), 0.05f);

        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, CurrentGun().Range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // If the hit object has a Target component, deal damage
            // Check tag types
            if (hit.transform.CompareTag("Target"))
            {
                if (hit.transform.TryGetComponent(out Target target))
                    target.OnHit();
            }
            else if (hit.transform.CompareTag("Reward"))
            {
                // You have shot the target. Points should be taken!!!!
                Debug.Log("Reward has beeen hit!");
            }
            else if (hit.transform.CompareTag("Other"))
            {
                // You have missed your shot. Points should be taken!!!!!
                Debug.Log("Hit object with Other tag.");
            }
            else
            {
                // In case we have more tags in the scene!!
                Debug.Log("Hit object with unrecognized tag.");
            }
        }
        else
        {
            // You have missed your shot. Points should be taken!!!!!
            Debug.Log("Missed!");
        }
    }
    void ApplyRecoil()
    {
        float recoilX = Random.Range(-CurrentGun().RecoilAmount / 4, CurrentGun().RecoilAmount / 4);
        float recoilY = Random.Range(-CurrentGun().RecoilAmount / 2, CurrentGun().RecoilAmount / 2);

        targetRotation = originalRotation * Quaternion.Euler(-CurrentGun().RecoilAmount, recoilY, recoilX);

        targetPosition = originalPosition - Vector3.forward * CurrentGun().RecoilKickback;

        Invoke(nameof(ResetRecoil), 0.05f);
    }

    void DisableMuzzleFlash()
    {
        MuzzleFlash().SetActive(false);
    }

    void ResetRecoil()
    {
        targetRotation = originalRotation;
        targetPosition = originalPosition;
    }

}