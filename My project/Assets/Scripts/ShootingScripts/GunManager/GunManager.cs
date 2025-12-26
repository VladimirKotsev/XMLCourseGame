using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public List<GunInput> InputList;
    public List<Gun> Guns = new List<Gun>();

    private GunUIManager uiManager;
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

    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("GunUI").GetComponent<GunUIManager>();
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

        if (Guns.Count > 0)
        {
            EquipGun(0);
        }
    }

    void Update()
    {
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

            if (uiManager != null)
            {
                uiManager.UpdateGunUI(this.CurrentGun);
            }
        }
    }

    private void DisableGuns()
    {
        foreach(var gun in this.Guns.Where(x => x != currentGun))
        {
            gun.Weapon.SetActive(false);
        }
    }
}
