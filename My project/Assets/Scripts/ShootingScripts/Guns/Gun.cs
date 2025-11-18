using UnityEngine;

public abstract class Gun
{
    public GameObject Weapon { get; }
    public GameObject MuzzleFlash { get; }

    public float Range { get; set; }
    public float RecoilAmount { get; set; }
    public float RecoilReturnSpeed { get; set; }

    public float RecoilKickback { get; set; }
    public bool CanZoom { get; set; }

    public Gun(GameObject weapon, GameObject muzzleFlash, float range, float recoilAmount, float recoilReturnSpeed, float recoilKickback, bool canZoom)
    {
        MuzzleFlash = muzzleFlash;
        Weapon = weapon;
        Range = range;
        RecoilAmount = recoilAmount;
        RecoilReturnSpeed = recoilReturnSpeed;
        RecoilKickback = recoilKickback;
        CanZoom = canZoom;
    }
}
