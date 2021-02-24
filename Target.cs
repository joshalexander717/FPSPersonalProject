using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    public GameObject mainObject;
    public GameObject floatingText;
    public float offsetText;

    public bool destroyOnDeath = false;
    public GameObject destroyParticle;


    public float health = 100;

    public float maxHealth;

    public SimpleHealthBar healthBar;

    public int xpAwarded;
    public GameObject ExperiencePoint;

    public bool DropsLoot;

    public float LootExplosionVelocity;

    public int ItemsDroppedAmount;

    public float PercentageToDrop;

    public List<GameObject> Loot;
    public List<int> PercentagesForLootSlots;

    public Transform LootExplosionPoint;


    public int subAmmoAmount;
    public GameObject subAmmo;
    public int sniperAmmoAmount;
    public GameObject sniperAmmo;
    public int shotgunAmmoAmount;
    public GameObject shotgunAmmo;
    public int heavyAmmoAmount;
    public GameObject heavyAmmo;
    public int assaultAmmoAmount;
    public GameObject assaultAmmo;

    // Start is called before the first frame update
    void Start()
    {
        if(mainObject == null)
        {
            mainObject = gameObject;
        }
        maxHealth = health;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(destroyOnDeath && health < 0)
        {
            SpawnXP();
            SpawnAmmo();
            if(DropsLoot)
            {
                for(int i = 0; i < ItemsDroppedAmount; i++)
                    SpawnLoot();
            }
            GameObject destroyPart = Instantiate(destroyParticle, transform.position, transform.rotation);
            Destroy(destroyPart, 2f);
            Destroy(mainObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateBar(health, maxHealth);

        if(floatingText)
        {
            ShowFloatingText(damage);
        }
    }

    void ShowFloatingText(float damage)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + offsetText, transform.position.z);
        GameObject damageText = Instantiate(floatingText, pos, Quaternion.identity);
        damageText.GetComponent<TextMeshPro>().text = damage + "";
        Destroy(damageText, 3f);
    }

    void SpawnXP()
    {
        for (int i = 0; i < xpAwarded; i++)
        {
            GameObject expPoint = Instantiate(ExperiencePoint, LootExplosionPoint.position, transform.rotation);
            expPoint.GetComponent<LootExplosion>().velocity = LootExplosionVelocity;
        }
    }

    void SpawnAmmo()
    {
        for (int i = 0; i < subAmmoAmount; i++)
        {
            GameObject ammo = Instantiate(subAmmo, LootExplosionPoint.position, transform.rotation);
            ammo.GetComponent<LootExplosion>().velocity = LootExplosionVelocity;
        }
        for (int i = 0; i < assaultAmmoAmount; i++)
        {
            GameObject ammo = Instantiate(assaultAmmo, LootExplosionPoint.position, transform.rotation);
            ammo.GetComponent<LootExplosion>().velocity = LootExplosionVelocity;
        }
        for (int i = 0; i < sniperAmmoAmount; i++)
        {
            GameObject ammo = Instantiate(sniperAmmo, LootExplosionPoint.position, transform.rotation);
            ammo.GetComponent<LootExplosion>().velocity = LootExplosionVelocity;
        }
        for (int i = 0; i < shotgunAmmoAmount; i++)
        {
            GameObject ammo = Instantiate(shotgunAmmo, LootExplosionPoint.position, transform.rotation);
            ammo.GetComponent<LootExplosion>().velocity = LootExplosionVelocity;
        }
        for (int i = 0; i < heavyAmmoAmount; i++)
        {
            GameObject ammo = Instantiate(heavyAmmo, LootExplosionPoint.position, transform.rotation);
            ammo.GetComponent<LootExplosion>().velocity = LootExplosionVelocity;
        }
    }

    void SpawnLoot()
    {
        
        if(Random.Range(0, 100) <= PercentageToDrop)
        {
            int PickLoot = Random.Range(0, 100);
            for(int i = 0; i < PercentagesForLootSlots.Count; i++)
            {
                if(PickLoot <= PercentagesForLootSlots[i])
                {
                    GameObject loot = Instantiate(Loot[i], LootExplosionPoint.position, transform.rotation);
                    loot.GetComponent<LootExplosion>().velocity = LootExplosionVelocity;
                    break;
                }
            }
        }
    }
}
