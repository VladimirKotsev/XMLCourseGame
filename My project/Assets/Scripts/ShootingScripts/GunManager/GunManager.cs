using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public List<GunInput> InputList;
    public List<Gun> Guns = new List<Gun>();

    private Gun currentGun;
    public Gun CurrentGun {
        get => currentGun;
        set
        {
            if (currentGun == value) return;

            currentGun = value;
            DisableGuns();
            currentGun.Weapon.SetActive(true);
        }
    }


    private void Awake()
    {
        foreach (var inputItem in InputList)
        {
            switch (inputItem.gunType)
            {
                case GunInput.Type.Pistol:
                    Guns.Add(new Pistol(inputItem.GameObject, inputItem.muzzleFlash));
                    break;
                case GunInput.Type.Sniper:
                    Guns.Add(new Sniper(inputItem.GameObject, inputItem.muzzleFlash));
                    break;
                case GunInput.Type.Shotgun:
                    Guns.Add(new Shotgun(inputItem.GameObject, inputItem.muzzleFlash));
                    break;
            }
        }
        this.CurrentGun = this.Guns[1];
    }

    private void DisableGuns()
    {
        foreach(var gun in this.Guns.Where(x => x != currentGun))
        {
            gun.Weapon.SetActive(false);
        }
    }
}
