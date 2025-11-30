using UnityEngine;

public class Pistol : Gun
{
    private const float RANGE = 65f;

    private const float RECOIL_AMOUNT = 25f;
    private const float RECOIL_RETURN_SPEED = 4f;
    private const float RECOIL_KICKBACK = 0.50f;
    private const float SHOOTING_DELAY = 0.57f;
    private const bool CAN_ZOOM = false;
    private const float ZOOM_FOV = 0;

    public Pistol(GameObject weapon, GameObject muzzleFlash)
        : base(weapon, muzzleFlash, weapon.GetComponentInChildren<AudioSource>(), RANGE, SHOOTING_DELAY, RECOIL_AMOUNT, RECOIL_RETURN_SPEED, RECOIL_KICKBACK, CAN_ZOOM, ZOOM_FOV) { }
}
