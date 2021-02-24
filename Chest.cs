using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool isOpen;
    Quaternion targetRotation;
    public ParticleSystem openParticles;

    public float LootExplosionVelocity;

    public int ItemsDroppedAmount;

    public float PercentageToDrop;

    public List<GameObject> Loot;
    public List<int> PercentagesForLootSlots;

    Transform LootExplosionPoint;

    bool lootSpawned;

    AudioSource AS;
    bool hasPlayed;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();
        hasPlayed = false;
        isOpen = false;
        lootSpawned = false;
        targetRotation = Quaternion.Euler(0, 0, -90);
        LootExplosionPoint = openParticles.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            if(!hasPlayed)
            {
                AS.Play();
                hasPlayed = true;
            }
            
            if (!lootSpawned && Quaternion.Angle(transform.localRotation, targetRotation) < 80)
            {
                Debug.Log("Chest reached loot open point");
                for (int i = 0; i < ItemsDroppedAmount; i++)
                {
                    SpawnLoot();
                }
                openParticles.Play();
                lootSpawned = true;
            }
            else if (Quaternion.Angle(transform.localRotation, targetRotation) < 2)
            {
                Debug.Log("Chest reached fully open");
                isOpen = false; // reached the target rotation
            }
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, 2 * Time.deltaTime);
        }
    }

    void SpawnLoot()
    {
        Debug.Log("Began spawning chest loot");
        if (Random.Range(0, 100) <= PercentageToDrop)
        {
            int PickLoot = Random.Range(0, 100);
            for (int i = 0; i < PercentagesForLootSlots.Count; i++)
            {
                if (PickLoot <= PercentagesForLootSlots[i])
                {
                    GameObject loot = Instantiate(Loot[i], LootExplosionPoint.position, new Quaternion(0,Random.Range(0,360),0,0));
                    loot.GetComponent<LootExplosion>().isChest = true;
                    loot.GetComponent<LootExplosion>().velocity = LootExplosionVelocity;
                    Debug.Log("Spawned item"); 
                    break;
                }
            }
        }
    }
}
