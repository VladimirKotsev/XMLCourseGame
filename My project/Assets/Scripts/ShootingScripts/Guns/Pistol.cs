using UnityEngine;

public class Pistol : Gun
{
    private const float RANGE = 100f;

    private const float RECOIL_AMOUNT = 25f;
    private const float RECOIL_RETURN_SPEED = 4f;
    private const float RECOIL_KICKBACK = 0.50f;
    private const float SHOOTING_DELAY = 0.2f;
    private const bool CAN_ZOOM = false;

    public Pistol(GameObject weapon, GameObject muzzleFlash)
        : base(weapon, muzzleFlash, RANGE, SHOOTING_DELAY, RECOIL_AMOUNT, RECOIL_RETURN_SPEED, RECOIL_KICKBACK, CAN_ZOOM) { }
}
