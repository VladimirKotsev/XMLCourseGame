using UnityEngine;

public class Shotgun : Gun
{
    private const float RANGE = 35f;

    private const float RECOIL_AMOUNT = 65f;
    private const float RECOIL_RETURN_SPEED = 2.5f;
    private const float RECOIL_KICKBACK = 0.90f;
    private const float SHOOTING_DELAY = 0.55f;
    private const bool CAN_ZOOM = false;

    public Shotgun(GameObject weapon, GameObject muzzleFlash)
        : base(weapon, muzzleFlash, RANGE,SHOOTING_DELAY, RECOIL_AMOUNT, RECOIL_RETURN_SPEED, RECOIL_KICKBACK, CAN_ZOOM) { }
}
