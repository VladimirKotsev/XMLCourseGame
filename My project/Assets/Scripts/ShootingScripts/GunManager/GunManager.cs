using System.Collections.Generic;
using UnityEngine;


//Changes guns for the player
public class GunManager : MonoBehaviour
{
    public List<GunInput> inputList;
    public List<Gun> guns = new List<Gun>();
    public Gun currentGun;

    private void Awake()
    {
        foreach (var inputItem in inputList)
        {
            switch (inputItem.gunType)
            {
                case GunInput.Type.Pistol:
                    guns.Add(new Pistol(inputItem.GameObject));
                    break;
                case GunInput.Type.Sniper:
                    guns.Add(new Sniper(inputItem.GameObject));
                    break;
                case GunInput.Type.Shotgun:
                    guns.Add(new Shotgun(inputItem.GameObject));
                    break;
            }
        }
        this.currentGun = this.guns[0];
    }
}
