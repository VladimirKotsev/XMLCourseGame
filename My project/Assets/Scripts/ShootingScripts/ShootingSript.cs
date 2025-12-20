using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Camera playerCamera;
    public Transform weaponHolder;
    public ScoreManager scoreManager;

    private UIManager uiManager;
    private GunManager gunManager;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float nextShootTime = 0f;
    private bool isScoped = false;
    private float normalFOV;

    void Start()
    {
        // THIS SHOULD WORK WHEN CHANGING WEAPONS => SHOULD BE MOVED TO A DIFFERENT METHOD/CLASS !!!!
        //if (CurrentGun().CanZoom)
        //{
        //    GameObject.FindGameObjectWithTag("Crosshair").SetActive(false);
        //}
        //else
        //{
        //    GameObject.FindGameObjectWithTag("Crosshair").SetActive(true);
        //}
        normalFOV = playerCamera.fieldOfView;
        gunManager = GameObject.FindGameObjectWithTag("Player").GetComponent<GunManager>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        originalPosition = weaponHolder.localPosition;
        originalRotation = weaponHolder.localRotation;

        targetPosition = originalPosition;
        targetRotation = originalRotation;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextShootTime)
        {
            Shoot();
            ApplyRecoil();
            CurrentAnimator().SetTrigger("Shooting");

            nextShootTime = Time.time + CurrentGun().ShootingDelay;
        }
        if (Input.GetButtonDown("Fire2") && CurrentGun().CanZoom && Time.time >= nextShootTime)
        {
            isScoped = !isScoped;

            if (isScoped)
                OnScoped();
            else
                OnUnscoped();
        }

        weaponHolder.localPosition = Vector3.Lerp(
            weaponHolder.localPosition,
            targetPosition,
            CurrentGun().RecoilReturnSpeed * Time.deltaTime
        );

        weaponHolder.localRotation = Quaternion.Lerp(
            weaponHolder.localRotation,
            targetRotation,
            CurrentGun().RecoilReturnSpeed * Time.deltaTime
        );
    }

    private GameObject MuzzleFlash()
    {
        return this.CurrentGun().MuzzleFlash;
    }

    private AudioSource AudioSource()
    {
        return this.CurrentGun().GunAudio;
    }

    private Gun CurrentGun()
    {
        return this.gunManager.CurrentGun;
    }

    private Animator CurrentAnimator()
    {
        return CurrentGun().Weapon.GetComponentInChildren<Animator>();
    }

    void OnScoped()
    {
        uiManager.State = UIState.Zoom;

        weaponHolder.gameObject.SetActive(false);

        playerCamera.fieldOfView = CurrentGun().Zoom_FOV;
    }

    void OnUnscoped()
    {
        uiManager.State = UIState.Crosshair;
        weaponHolder.gameObject.SetActive(true);

        playerCamera.fieldOfView = normalFOV;
        isScoped = false;
    }

    void Shoot()
    {
        Gun gun = CurrentGun();
        OnUnscoped();

        if (AudioSource() != null && AudioSource().clip != null)
        {
            AudioSource().PlayOneShot(AudioSource().clip);
        }

        MuzzleFlash().SetActive(true);
        Invoke(nameof(DisableMuzzleFlash), 0.05f);

        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, CurrentGun().Range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            if (hit.transform.CompareTag("Target"))
            {
                if (hit.transform.TryGetComponent(out Target target))
                    target.OnHit();
                scoreManager.AddScore(1);
            }
            else if (hit.transform.CompareTag("Reward"))
            {
                scoreManager.AddScore(-5);
            }
            else if (hit.transform.CompareTag("Other"))
            {
                scoreManager.AddScore(-1);
            }
            else
            {
                scoreManager.AddScore(-1);
            }
        }
        else
        {
            scoreManager.AddScore(-1);
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