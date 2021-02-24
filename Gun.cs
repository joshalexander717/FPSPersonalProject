using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class Gun : MonoBehaviour
{
    [Header("Gun Type")]
    public bool rifle;
    public bool submachineGun;
    public bool sniper;
    public bool shotgun;
    public bool heavy;

    [Header("Gun Variables")]
    public bool semiAuto;
    public float damagePerBullet;
    public float firerate;
    float firerateTimer;
    public float shootDistance;
    public float recoilAmount;
    public float kickbackAmount;
    public float spread;
    public float currentSpread;

    [Header("Aim Variables")]
    public Vector3 hipPos;
    public Vector3 aimPos;
    public Vector3 sprintPos;
    public float aimSpeed;
    bool isAiming;

    [Header("Mag Variables")]
    public float reloadTime;
    public int magSize;
    int bulletsInMag;
    Vector3 reloadPos;
    bool isReloading;

    [Header("Gun Sounds")]
    public AudioClip shootSound;
    AudioSource aSource;

    [Header("Crosshair Settings")]
    float minCrosshair;
    float maxCrosshair;
    CrosshairGenerator crosshair;

    [Header("Gun Extras")]
    public Transform muzzleTip;
    public GameObject muzzleParticle;
    public GameObject impactParticle;
    public GameObject holster;
    Transform head;
    Transform player;

    [Header("Attatchments")]
    public GameObject scope;
    public Transform scopeSpot;
    public GameObject mag;
    public Transform magSpot;
    public GameObject foregrip;
    public Transform foregripSpot;

    GameObject currScope;
    GameObject currForegrip;
    GameObject currMag;

    bool isSprinting;
    bool isWalking;
    Hitmarker hitmarker;
    Player playerStats;
    TextMeshProUGUI gunAmmoText;

    //Default vals
    float _firerate;
    float _damagePerBullet;
    float _shootDistance;
    float _recoilAmount;
    float _kickbackAmount;
    float _spread;
    Vector3 _aimPos;
    float _aimSpeed;
    float _reloadTime;
    int _magSize;
    float _minCrosshair;
    float _maxCrosshair;


    public bool isUsing;
    Overlay overlay;
    PlayerMovement PM;

    // Start is called before the first frame update
    void Awake()
    {
        holster = GameObject.Find("Holster");
        minCrosshair += spread * 2;
        maxCrosshair += spread * 2 * 5;
        _firerate = firerate;
        _shootDistance = shootDistance;
        _recoilAmount = recoilAmount;
        _kickbackAmount = kickbackAmount;
        _spread = spread;
        _aimPos = aimPos;
        _aimSpeed = aimSpeed;
        _reloadTime = reloadTime;
        _magSize = magSize;
        _minCrosshair = minCrosshair;
        _maxCrosshair = maxCrosshair;
        _damagePerBullet = damagePerBullet;
        overlay = GameObject.Find("Overlay").GetComponent<Overlay>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PM = player.GetComponent<PlayerMovement>();
        head = GameObject.Find("Head").transform;
        gunAmmoText = GameObject.Find("GunAmmo").GetComponent<TextMeshProUGUI>();
        playerStats = player.GetComponent<Player>();
        hitmarker = GameObject.Find("hitmarker").GetComponent<Hitmarker>();
        crosshair = GameObject.Find("Crosshair").GetComponent<CrosshairGenerator>();
        bulletsInMag = magSize;
        aSource = GetComponent<AudioSource>();
        transform.localPosition = hipPos;
        firerateTimer = 0;
        isReloading = false;
        reloadPos = hipPos - new Vector3(0, .1f, 0);
        EquipAttachments();
    }

    // Update is called once per frame
    void Update()
    {
        Use();

        isWalking = Mathf.Abs(player.GetComponent<Rigidbody>().velocity.magnitude) > .2f;
        if(Input.GetAxis("Vertical") > 0 && !PM.isCrouched && !isAiming)
            isSprinting = Input.GetButton("Sprint");

        if (!isUsing && !overlay.isSomethingOpen)
        {
            Reloading();

            Shoot();

            SetAmmoGUI();

            Aim();

            Sprinting();

            Crosshair();
        }
        GunPositioning();
    }

    void Use()
    {
        RaycastHit hit;

        if (Physics.Raycast(head.position, head.forward, out hit, 3))
        {
            if (hit.transform.CompareTag("Usable"))
            {
                Use useItem = hit.transform.GetComponent<Use>();
                if (useItem.used)
                {
                    isUsing = false;
                    return;
                }
                else if (Input.GetButton("Use"))
                {
                    if (useItem.TimeToUse > 0)
                    {
                        isUsing = true;
                        useItem.TimeToUse -= Time.deltaTime;
                    }
                    else
                    {
                        isUsing = false;
                        useItem.UseObject();
                    }
                }
                else
                {
                    isUsing = false;
                }
            }
            else
            {
                isUsing = false;
            }
        }
        else
        {
            isUsing = false;
        }
        player.GetComponent<PlayerMovement>().isUsing = isUsing;
    }

    void Shoot()
    {
        if (bulletsInMag > 0 && Input.GetButton("Fire1"))
            isSprinting = false;
        if (!semiAuto)
        {
            if (bulletsInMag > 0 && Input.GetButton("Fire1") && firerateTimer <= 0)
            {
                Fire();
                firerateTimer = firerate;
            }
            else
            {
                firerateTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (bulletsInMag > 0 && Input.GetButtonDown("Fire1"))
                Fire();
        }
    }

    void Sprinting()
    {
        Quaternion targetRotation;
        if (isSprinting && PM.isGrounded)
        {
            targetRotation = Quaternion.Euler(0, -75, 0);

        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }


        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, 8 * Time.deltaTime);

        player.GetComponent<PlayerMovement>().isSprinting = isSprinting;
        
    }

    void Fire()
    {

        KickbackAndRecoil();

        aSource.clip = shootSound;
        aSource.Play();

        RayCastShot();

        bulletsInMag--;
    }

    void SetAmmoGUI()
    {
        if (submachineGun)
            gunAmmoText.text = gameObject.name + "\n" + bulletsInMag + " | " + playerStats.subAmmoReserve;
        else if (rifle)
            gunAmmoText.text = gameObject.name + "\n" + bulletsInMag + " | " + playerStats.rifleAmmoReserve;
        else if (shotgun)
            gunAmmoText.text = gameObject.name + "\n" + bulletsInMag + " | " + playerStats.shotgunAmmoReserve;
        else if (sniper)
            gunAmmoText.text = gameObject.name + "\n" + bulletsInMag + " | " + playerStats.sniperAmmoReserve;
        else if (heavy)
            gunAmmoText.text = gameObject.name + "\n" + bulletsInMag + " | " + playerStats.heavyAmmoReserve;
    }

    void Crosshair()
    {
        if (isSprinting)
        {
            CrosshairGenerator.instance.UpdateCrosshairSpread(maxCrosshair);
        }
        else if (isWalking)
        {
            CrosshairGenerator.instance.UpdateCrosshairSpread((maxCrosshair + minCrosshair) / 2);
        }
        else
        {
            CrosshairGenerator.instance.UpdateCrosshairSpread(minCrosshair);
        }
    }

    void Reloading()
    {
        if (Input.GetButtonDown("Reload"))
        {
            isReloading = true;
            StartCoroutine(Reload());
        }

        if (isReloading)
        {
            isSprinting = false;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, reloadPos, 8 * Time.deltaTime);
            return;
        }
    }

    IEnumerator Reload()
    {
        crosshair.visible = false;
        yield return new WaitForSeconds(reloadTime);
        crosshair.visible = true;
        Debug.Log("Reloaded");

        //Set from reserve
        int magSpace = magSize - bulletsInMag;

        if (submachineGun)
        {
            int reloadedBullets = playerStats.subAmmoReserve - magSpace;
            if (reloadedBullets > 0)
            {
                playerStats.subAmmoReserve -= magSpace;
                bulletsInMag = magSize;
            }
            else
            {
                bulletsInMag = playerStats.subAmmoReserve;
                playerStats.subAmmoReserve = 0;
            }
        }
        else if (rifle)
        {
            int reloadedBullets = playerStats.rifleAmmoReserve - magSpace;
            if (reloadedBullets > 0)
            {
                playerStats.rifleAmmoReserve -= magSpace;
                bulletsInMag = magSize;
            }
            else
            {
                bulletsInMag = playerStats.rifleAmmoReserve;
                playerStats.rifleAmmoReserve = 0;
            }
        }
        else if (sniper)
        {
            int reloadedBullets = playerStats.sniperAmmoReserve - magSpace;
            if (reloadedBullets > 0)
            {
                playerStats.sniperAmmoReserve -= magSpace;
                bulletsInMag = magSize;
            }
            else
            {
                bulletsInMag = playerStats.sniperAmmoReserve;
                playerStats.sniperAmmoReserve = 0;
            }
        }
        else if (shotgun)
        {
            int reloadedBullets = playerStats.shotgunAmmoReserve - magSpace;
            if (reloadedBullets > 0)
            {
                playerStats.shotgunAmmoReserve -= magSpace;
                bulletsInMag = magSize;
            }
            else
            {
                bulletsInMag = playerStats.shotgunAmmoReserve;
                playerStats.shotgunAmmoReserve = 0;
            }
        }
        else if (heavy)
        {
            int reloadedBullets = playerStats.heavyAmmoReserve - magSpace;
            if (reloadedBullets > 0)
            {
                playerStats.heavyAmmoReserve -= magSpace;
                bulletsInMag = magSize;
            }
            else
            {
                bulletsInMag = playerStats.heavyAmmoReserve;
                playerStats.heavyAmmoReserve = 0;
            }
        }

        isReloading = false;
    }

    void Aim()
    {
        if (Input.GetButtonDown("Fire2") && !isAiming)
            isAiming = true;
        else if (Input.GetButtonDown("Fire2"))
            isAiming = false;

        
        player.GetComponent<PlayerMovement>().isAiming = isAiming;
    }

    void KickbackAndRecoil()
    {
        if (isAiming)
        {
            holster.GetComponent<Recoil>().AimRecoil();
        }
        else
        {
            holster.GetComponent<Recoil>().HipRecoil();
        }
    }

    void GunPositioning()
    {
        if (isAiming && !isReloading)
        {
            crosshair.visible = false;
            isSprinting = false;

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, aimPos, aimSpeed * Time.deltaTime);
        }
        else if (!isReloading && isSprinting && PM.isGrounded)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, sprintPos, aimSpeed * Time.deltaTime);
        }
        else if (!isReloading)
        {
            crosshair.visible = true;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, hipPos, aimSpeed * Time.deltaTime);
        }
    }

    void RayCastShot()
    {
        RaycastHit hit;
        
        GameObject muzzleInstance = Instantiate(muzzleParticle, muzzleTip.position, muzzleTip.rotation);
        muzzleInstance.transform.parent = muzzleTip;
        Destroy(muzzleInstance, 3f);
        Vector3 dir;
        if (isAiming)
            dir = head.forward;
        else if(isWalking)
        {
            dir = (head.transform.forward + UnityEngine.Random.insideUnitSphere * spread * 2).normalized;
        }
        else
            dir = (head.transform.forward + UnityEngine.Random.insideUnitSphere * spread).normalized;
        if (Physics.Raycast(head.position, dir, out hit, shootDistance))
        {
            Debug.Log("Hit" + transform.position);
            
            GameObject impactInstance = Instantiate(impactParticle, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactInstance, .5f);

            if (hit.transform.CompareTag("Target"))
            {
                hit.transform.GetComponent<TargetBodyPart>().BodyDamage(damagePerBullet);
                if(!isAiming)
                    hitmarker.Sethitmarker();
                hitmarker.PlayHitMarkerNoise();
            }
            //if (hit.transform.tag != "Ground" && hit.transform.tag != "Player")
            //Destroy(hit.transform.gameObject);
        }
        else
            Debug.Log("No Hit");
    }
    

    public void EquipAttachments()
    {
        transform.position = player.position;
        if(scope != null)
        {
            currScope = Instantiate(scope, scopeSpot);
            AttatchmentStat(scope.GetComponent<GunAttatchment>());
        }
        else
        {
            Destroy(currScope);
        }

        if(foregrip != null)
        {
            currForegrip = Instantiate(foregrip, foregripSpot);
            AttatchmentStat(foregrip.GetComponent<GunAttatchment>());
        }
        else
        {
            Destroy(currForegrip);
        }
        if (mag != null)
        {
            currMag = Instantiate(mag, magSpot);
            AttatchmentStat(mag.GetComponent<GunAttatchment>());
        }
        else
        {
            Destroy(currMag);
        }
        holster.GetComponent<Recoil>().RecoilRotation = new Vector3(recoilAmount,recoilAmount,recoilAmount);
        holster.GetComponent<Recoil>().RecoilRotationAiming = new Vector3(recoilAmount / 2, recoilAmount / 2, recoilAmount / 2);
    }

    public void ResetGunAttatchments()
    {
        if (currScope != null)
            Destroy(currScope);
        if (currMag != null)
            Destroy(currMag);
        if (currForegrip != null)
            Destroy(currForegrip);
        firerate = _firerate;
        damagePerBullet = _damagePerBullet;
        shootDistance = _shootDistance;
        recoilAmount = _recoilAmount;
        kickbackAmount = _kickbackAmount;
        spread = _spread;
        aimPos = _aimPos;
        aimSpeed = _aimSpeed;
        reloadTime = _reloadTime;
        magSize = _magSize;
        minCrosshair = _minCrosshair;
        maxCrosshair = _maxCrosshair;
    }

    void AttatchmentStat(GunAttatchment attatchment)
    {
        firerate -= attatchment.increaseFirerate;
        damagePerBullet += attatchment.increaseDamage;
        shootDistance += attatchment.increaseShootDistance;
        recoilAmount -= attatchment.decreaseRecoilAmount;
        kickbackAmount -= attatchment.decreaseKickbackAmount;
        spread -= attatchment.decreaseSpread;
        minCrosshair += spread * 2;
        maxCrosshair += spread * 2 * 5;
        aimSpeed += attatchment.increaseAimSpeed;
        
        if (attatchment.magSize > 0)
            magSize = attatchment.magSize;
        if (!attatchment.aimPos.Equals(Vector3.zero))
            aimPos = attatchment.aimPos;
        if(attatchment.reloadTime > 0)
            reloadTime = attatchment.reloadTime;
    }

    public void SetScope(GameObject newScope)
    {
        scope = newScope;
    }

    public void SetMag(GameObject newMag)
    {
        mag = newMag;
    }

    public void SetForegrip(GameObject newForegrip)
    {
        foregrip = newForegrip;;
    }
}
