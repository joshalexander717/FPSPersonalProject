using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int critPercentage;
    public float shield;
    public float shieldRegenTime;
    public float shieldRegenSpeed;
    float currShieldRegenTime;
    float maxShield;
    float sameShield;
    float sameHealth;

    public float health;
    float maxHealth;

    public int subAmmoReserve;
    public int heavyAmmoReserve;
    public int sniperAmmoReserve;
    public int rifleAmmoReserve;
    public int shotgunAmmoReserve;

    AudioSource As;
    AudioSource AsShield;

    public AudioClip regenShield;
    public AudioClip shieldBreak;
    public AudioClip shieldDamageSound;
    public AudioClip playerDamageSound;


    public GameObject gun1;
    public GameObject scope1;
    public GameObject foregrip1;
    public GameObject mag1;

    public GameObject gun2;
    public GameObject scope2;
    public GameObject foregrip2;
    public GameObject mag2;


    public GameObject holster;
    int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        maxShield = shield;
        As = GetComponent<AudioSource>();
        AsShield = GameObject.Find("ShieldSounds").GetComponent<AudioSource>();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("SwitchWeapons"))
        {
            if(selectedWeapon == 0)
            {
                selectedWeapon = 1;
                SelectWeapon();
            }
            else
            {
                selectedWeapon = 0;
                SelectWeapon();
            }
        }

        RegenShield();
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in holster.transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                weapon.position = transform.position;
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    void RegenShield()
    {
        if (shield != maxShield && shield == sameShield && health == sameHealth)
        {
            if (currShieldRegenTime <= 0)
            {
                shield = maxShield;
                AsShield.clip = regenShield;
                AsShield.Play();
                currShieldRegenTime = shieldRegenTime;
                SimpleHealthBar.UpdateBar("PlayerShield", shield, maxShield);
            }
            else
                currShieldRegenTime -= Time.deltaTime;
        }
        else
            currShieldRegenTime = shieldRegenTime;

        sameShield = shield;
        sameHealth = health;
    }

    public void TakeDamage(float damage)
    {
        
        CameraShaker.Instance.Shake(CameraShakePresets.Flinch);
        if(shield > 0)
        {
            shield -= damage;
            As.clip = shieldDamageSound;
            As.Play();
            if (shield <= 0)
            {
                health += shield;
                shield = 0;
                SimpleHealthBar.UpdateBar("PlayerHealth", health, maxHealth);
                AsShield.clip = shieldBreak;
                AsShield.Play();
            }
            SimpleHealthBar.UpdateBar("PlayerShield", shield, maxShield);
        }
        else
        {
            As.clip = playerDamageSound;
            As.Play();
            health -= damage;
            SimpleHealthBar.UpdateBar("PlayerHealth", health, maxHealth);
        }
        //if(health <= 0)
            //PlayerDies
    }


    public void RefreshGuns()
    {
        if(gun1 != null)
        {
            gun1.GetComponent<Gun>().ResetGunAttatchments();
            if(mag1 != null)
                gun1.GetComponent<Gun>().SetMag(mag1);
            else
                gun1.GetComponent<Gun>().mag = null;
            if (scope1 != null)
                gun1.GetComponent<Gun>().SetScope(scope1);
            else
                gun1.GetComponent<Gun>().scope = null;
            if (foregrip1 != null)
                gun1.GetComponent<Gun>().SetForegrip(foregrip1);
            else
                gun1.GetComponent<Gun>().foregrip = null;

            gun1.GetComponent<Gun>().EquipAttachments();
        }
        else
        {
            Destroy(gun1);
        }
        if (gun2 != null)
        {
            gun2.GetComponent<Gun>().ResetGunAttatchments();
            if (mag2 != null)
                gun2.GetComponent<Gun>().SetMag(mag2);
            else
                gun2.GetComponent<Gun>().mag = null;
            if (scope2 != null)
                gun2.GetComponent<Gun>().SetScope(scope2);
            else
                gun2.GetComponent<Gun>().scope = null;
            if (foregrip2 != null)
                gun2.GetComponent<Gun>().SetForegrip(foregrip2);
            else
                gun2.GetComponent<Gun>().foregrip = null;

            gun2.GetComponent<Gun>().EquipAttachments();
        }
        else
            Destroy(gun2);

        SelectWeapon();
    }
}
