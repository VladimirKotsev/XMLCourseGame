using UnityEngine;

public class Sniper : Gun
{

    private const float RANGE = 100f;

    private const float RECOIL_AMOUNT = 50f;
    private const float RECOIL_RETURN_SPEED = 1f;
    private const float RECOIL_KICKBACK = 1.50f;
    private const bool CAN_ZOOM = true;

    public Sniper(GameObject weapon)
        : base(weapon, RANGE, RECOIL_AMOUNT, RECOIL_RETURN_SPEED, RECOIL_KICKBACK, CAN_ZOOM) { }

}
