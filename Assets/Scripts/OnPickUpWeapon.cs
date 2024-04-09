using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPickUpWeapon : MonoBehaviour
{
    public GunController weaponPrefab;
    public WeaponManager PlayerWeaponManager;

    // Start is called before the first frame update
    void Start()
    {
        PlayerWeaponManager = GameObject.Find("Player").GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerWeaponManager.gameObject)
        {
            PlayerWeaponManager.AddWeapon(weaponPrefab);
            GameObject.Destroy(gameObject);
        }
    }
}
