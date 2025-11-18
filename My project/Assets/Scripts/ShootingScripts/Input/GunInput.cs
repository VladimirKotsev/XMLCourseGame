using UnityEngine;

[System.Serializable]
public class GunInput
{
    public GameObject GameObject;
    public enum Type
    {
        Pistol,
        Shotgun,
        Sniper
    }

    public Type gunType;
}
