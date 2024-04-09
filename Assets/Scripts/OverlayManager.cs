using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OverlayManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public WeaponManager weaponManager;
    public TextMeshProUGUI PlayerHealth;
    public TextMeshProUGUI TotalAmmo;
    public TextMeshProUGUI InClipAmmo;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SetHealthText());
        StartCoroutine(SetAmmoCountText());
    }

    IEnumerator SetHealthText()
    {
        PlayerHealth.text = "Health: " + playerManager.health.ToString();
        yield return null;
    }

    IEnumerator SetAmmoCountText()
    {
        TotalAmmo.text = "Total Ammo: " + weaponManager.mWeaponSlot[weaponManager.mActiveWeaponIndex].mAmmoInReserve;
        InClipAmmo.text = "Ammo in Clip: " + weaponManager.mWeaponSlot[weaponManager.mActiveWeaponIndex].mAmmoInClip;
        yield return null;
    }
}
