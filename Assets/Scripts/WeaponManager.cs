using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Starting Weapons")]
    public List<GunController> StartingWeapons;

    [Header("Weapon's parent and positions")]
    public Transform WeaponParent;
    public Transform DefaultPosition;
    public Transform AimingPosition;
    public Transform WeaponBulletParent;

    public GunController[] mWeaponSlot= new GunController[9];
    public int mActiveWeaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        mActiveWeaponIndex = 0;

        // Add starting weapons
        foreach (var weapon in StartingWeapons)
        {
            AddWeapon(weapon);
        }

        mWeaponSlot[mActiveWeaponIndex].ShowWeapon(true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSwitchingWeapon();
    }

    public bool AddWeapon(GunController weaponPrefab)
    {
        for(int i = 0; i < mWeaponSlot.Length; i++)
        {
            if (mWeaponSlot[i] == null)
            {
                GunController weaponInstance = Instantiate(weaponPrefab, WeaponParent);
                weaponInstance.mDefaultPosition = DefaultPosition.localPosition;
                weaponInstance.mAimingPosition = AimingPosition.localPosition;
                weaponInstance.mBulletParent = WeaponBulletParent;
                

                weaponInstance.ShowWeapon(false);
                mWeaponSlot[i] = weaponInstance;
                return true;
            }
        }
        return false;
    }

    public bool UpdateSwitchingWeapon()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            int newActiveWeaponIndex = mActiveWeaponIndex - 1;
            if(newActiveWeaponIndex < 0) return false;
            return SwitchWeapon(newActiveWeaponIndex);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            int newActiveWeaponIndex = mActiveWeaponIndex + 1;
            if (newActiveWeaponIndex >= mWeaponSlot.Length || mWeaponSlot[newActiveWeaponIndex] == null) return false;
            return SwitchWeapon(newActiveWeaponIndex);
        }
        return false ;
    }

    public bool SwitchWeapon(int newActiveWeaponIndex)
    {
        if(mActiveWeaponIndex == newActiveWeaponIndex) return false;

        mWeaponSlot[mActiveWeaponIndex].ShowWeapon(false);
        mWeaponSlot[newActiveWeaponIndex].ShowWeapon(true);
        mActiveWeaponIndex = newActiveWeaponIndex;
        return true;
    }
}
