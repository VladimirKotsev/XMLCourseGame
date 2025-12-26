using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GunUIManager uiManager;

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

    private void Update()
    {
        // chaka 1-2-3 kato input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipGun(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipGun(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipGun(2);
        }
    }

    private void EquipGun(int index)
    {
        if (index < Guns.Count)
        {
            this.CurrentGun = this.Guns[index];

            // izvikva function change selected gun ui -drug script
            // Checks if UI manager is assigned to avoid errors
            if (uiManager != null)
            {
                // Replace 'UpdateGunUI' with the actual function name in your UI script
                uiManager.UpdateGunUI(this.CurrentGun);
            }
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
        //this.CurrentGun = this.Guns[0]; //pistol
        //this.CurrentGun = this.Guns[1]; //sniper
        this.CurrentGun = this.Guns[2]; //shotgun
    }

    private void DisableGuns()
    {
        foreach(var gun in this.Guns.Where(x => x != currentGun))
        {
            gun.Weapon.SetActive(false);
        }
    }
}
