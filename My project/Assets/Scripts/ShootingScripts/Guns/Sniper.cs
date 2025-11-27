using UnityEngine;

public class Sniper : Gun
{

    private const float RANGE = 120f;

    private const float RECOIL_AMOUNT = 70f;
    private const float RECOIL_RETURN_SPEED = 0.95f;
    private const float RECOIL_KICKBACK = 1.70f;
    private const float SHOOTING_DELAY = 1.95f;
    private const bool CAN_ZOOM = true;

    public Sniper(GameObject weapon, GameObject muzzleFlash)
        : base(weapon, muzzleFlash, weapon.GetComponentInChildren<AudioSource>(), RANGE,SHOOTING_DELAY, RECOIL_AMOUNT, RECOIL_RETURN_SPEED, RECOIL_KICKBACK, CAN_ZOOM) { }

}
