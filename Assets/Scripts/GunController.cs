using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int AmmoCapa = 120;

    // Gun Infos
    private bool mCanShoot;
    public int mAmmoInClip;
    public int mAmmoInReserve;

    [Header("Bullets Info")]
    public GameObject mBulletPrefab;
    public Transform mBulletSpawnPosition;
    public Transform mBulletParent;

    [Header("Muzzle Flash")]
    public Image mMuzzleFlashImage;
    public Sprite[] mMuzzleFlashSprites;

    [Header("Aiming")]
    public Vector3 mDefaultPosition;
    public Vector3 mAimingPosition;
    public Vector3 mAimingAdjust;
    private float mSmoothAim = 10.0f;

    [Header("Audio")]
    public AudioClip mFireClip;
    public AudioClip mReloadClip;
    private AudioSource mGunAudioSource;

    private bool mPlayReloadAudio;
    private bool mPlayFireAudio;

    private void Awake()
    {
        mGunAudioSource = gameObject.GetComponent<AudioSource>();
        gameObject.transform.localPosition += mDefaultPosition;

        mCanShoot = true;
        mAmmoInClip = clipSize;
        mAmmoInReserve = AmmoCapa;

        mPlayReloadAudio = false;
        mPlayFireAudio = false;
    }

    private void Update()
    {
        
        // Handle the Aiming
        UpdateWeaponAiming();

        // Handle Fire
        if (Input.GetMouseButton(0) && mCanShoot && mAmmoInClip > 0)
        {
            mCanShoot = false;
            mAmmoInClip--;
            StartCoroutine(GunShoot());
        }
        //Handle Reload
        else if (Input.GetKeyDown(KeyCode.R) && mAmmoInClip < clipSize && mAmmoInReserve > 0)
        {
            int ammoNeeded = clipSize - mAmmoInClip;
            if (ammoNeeded > mAmmoInReserve)
            {
                mAmmoInClip += mAmmoInReserve;
                mAmmoInReserve = 0;
            }
            else
            {
                mAmmoInClip += ammoNeeded;
                mAmmoInReserve -= ammoNeeded;
            }

            mPlayReloadAudio = true;
            StartCoroutine(PlayAudio());
        }
    }

    public void ShowWeapon(bool show)
    {
        gameObject.SetActive(show);
    }

    public void UpdateWeaponAiming()
    {
        Vector3 target = mDefaultPosition;
        if (Input.GetMouseButton(1)) target = mAimingPosition + mAimingAdjust;

        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * mSmoothAim);

        transform.localPosition = desiredPosition;
    }

    IEnumerator PlayAudio()
    {
        if (mPlayFireAudio)
        {
            mGunAudioSource.clip = mFireClip;
            mGunAudioSource.Play();
            mPlayFireAudio = false;
        }

        if (mPlayReloadAudio)
        {
            mGunAudioSource.clip = mReloadClip;
            mGunAudioSource.Play();
            yield return new WaitForSeconds(0.5f);
            mGunAudioSource.Play();
            mPlayReloadAudio = false;
        }

        yield return new WaitForSeconds(fireRate);
        mGunAudioSource.clip = null;
    }

    IEnumerator GunShoot()
    {
        StartCoroutine(MuzzleFlash());
        yield return new WaitForSeconds(fireRate);
        mPlayFireAudio = true;
        StartCoroutine(PlayAudio());
        GameObject.Instantiate(mBulletPrefab, mBulletSpawnPosition.position, mBulletSpawnPosition.rotation, mBulletParent);
        mCanShoot = true;
    }

    IEnumerator MuzzleFlash()
    {
        mMuzzleFlashImage.sprite = mMuzzleFlashSprites[Random.Range(0, mMuzzleFlashSprites.Length)];
        mMuzzleFlashImage.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        mMuzzleFlashImage.sprite = null;
        mMuzzleFlashImage.color = new Color(0, 0, 0, 0);
    }
}
