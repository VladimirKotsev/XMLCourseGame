using UnityEngine;

public abstract class Gun
{
    public GameObject Weapon { get; }
    public GameObject MuzzleFlash { get; }
    public AudioSource GunAudio { get; }

    public float Range { get; set; }
    public float RecoilAmount { get; set; }
    public float RecoilReturnSpeed { get; set; }
    public float ShootingDelay { get; set; }
    public float RecoilKickback { get; set; }
    public bool CanZoom { get; set; }

    public Gun(GameObject weapon, GameObject muzzleFlash, AudioSource audioSource, float range, float shootingDelay, float recoilAmount, float recoilReturnSpeed, float recoilKickback, bool canZoom)
    {
        MuzzleFlash = muzzleFlash;
        GunAudio = audioSource;
        Weapon = weapon;
        Range = range;
        ShootingDelay = shootingDelay;
        RecoilAmount = recoilAmount;
        RecoilReturnSpeed = recoilReturnSpeed;
        RecoilKickback = recoilKickback;
        CanZoom = canZoom;
    }
}
