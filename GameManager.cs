using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class ingameEquipment
{
    public string name = "Equipment";
    public GameObject prefab;
    public GameObject inventoryItem;
    public GameObject worldItem;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Gun1;
    public GameObject Gun2;

    public int gun1ID = -1;
    public int foregrip1ID = -1;
    public int mag1ID = -1;
    public int scope1ID = -1;

    public int gun2ID = -1;
    public int foregrip2ID = -1;
    public int mag2ID = -1;
    public int scope2ID = -1;

    public GameObject holster;
    public Player PM;

    public InventorySlot[] inventorySlots;
    public Canvas interfaceCanvas;
    public Transform draggables;

    public ingameEquipment[] equipment;

    // Main //
    void Awake()
    {
        GameManager.Instance = this;
    }
    public void DestroyGun1()
    {
        if (PM.gun1 != null)
        {
            Destroy(Gun1);
            PM.RefreshGuns();
        }
    }
    public void DestroyMag1()
    {
        if (PM.mag1 != null)
        {
            //Destroy(PM.mag1);
            PM.mag1 = null;
            PM.RefreshGuns();
        }
    }
    public void DestroyScope1()
    {
        if (PM.scope1  != null)
        {
            //Destroy(PM.scope1);
            PM.scope1 = null;
            PM.RefreshGuns();
        }
    }
    public void DestroyForegrip1()
    {
        if (PM.foregrip1 != null)
        {
            //Destroy(PM.foregrip1);
            PM.foregrip1 = null;
            PM.RefreshGuns();
        }
    }

    public void DestroyGun2()
    {
        if (PM.gun2 != null)
        {
            Destroy(Gun2);
            PM.RefreshGuns();
        }
    }
    public void DestroyMag2()
    {
        if (PM.mag2 != null)
        {
            PM.mag2 = null;
            PM.RefreshGuns();
        }
    }
    public void DestroyScope2()
    {
        if (PM.scope2 != null)
        {
            PM.scope2 = null;
            PM.RefreshGuns();
        }
    }
    public void DestroyForegrip2()
    {
        if (PM.foregrip2 != null)
        {
            PM.foregrip2 = null;
            PM.RefreshGuns();
        }
    }

    //SPAWNING
    public void SpawnGun1()
    {
        DestroyGun1();

        if (gun1ID != -1)
        {
            Gun1 = Instantiate(equipment[gun1ID].prefab, holster.transform);
            Gun1.transform.position = PM.transform.position;
            Gun1.name = equipment[gun1ID].prefab.name;
            PM.gun1 = Gun1;
            PM.RefreshGuns();
        }
    }
    public void SpawnMag1()
    {
        DestroyMag1();

        if (mag1ID != -1)
        {
            PM.mag1 = equipment[mag1ID].prefab;
            PM.RefreshGuns();
        }
    }
    public void SpawnScope1()
    {
        DestroyScope1();

        if (scope1ID != -1)
        {
            PM.scope1 = equipment[scope1ID].prefab;
            PM.RefreshGuns(); 
        }
    }
    public void SpawnForegrip1()
    {
        DestroyForegrip1();

        if (foregrip1ID != -1)
        {
            PM.foregrip1 = equipment[foregrip1ID].prefab;
            PM.RefreshGuns();
        }
    }
    public void SpawnGun2()
    {
        DestroyGun2();

        if (gun2ID != -1)
        {
            Gun2 = Instantiate(equipment[gun2ID].prefab, holster.transform);
            Gun2.name = equipment[gun2ID].prefab.name;
            PM.gun2 = Gun2;
            PM.RefreshGuns();
        }
    }
    public void SpawnMag2()
    {
        DestroyMag2();

        if (mag2ID != -1)
        {
            PM.mag2 = equipment[mag2ID].prefab;
            PM.RefreshGuns();
        }
    }
    public void SpawnScope2()
    {
        DestroyScope2();

        if (scope2ID != -1)
        {
            PM.scope2 = equipment[scope2ID].prefab;
            PM.RefreshGuns();
        }
    }
    public void SpawnForegrip2()
    {
        DestroyForegrip2();

        if (foregrip2ID != -1)
        {
            PM.foregrip2 = equipment[foregrip2ID].prefab;
            PM.RefreshGuns();
        }
    }


    public void PickupItem(int itemID)
    {
        bool foundSlot = false;

        for (int i = 8; i < inventorySlots.Length; i++)
        {
            if (!inventorySlots[i].isFull)
            {
                GameObject GO = Instantiate(equipment[itemID].inventoryItem, inventorySlots[i].gameObject.transform);
                inventorySlots[i].currentItem = GO.GetComponent<InventoryItem>();
                inventorySlots[i].isFull = true;
                inventorySlots[i].RefreshIcon();
                foundSlot = true;
                break;
            }
        }

        if (foundSlot == false)
        {
            Instantiate(equipment[itemID].worldItem, PM.transform.position + new Vector3(0, 0, 5), Quaternion.identity);
        }
    }
    public void DropItem(InventoryItem item)
    {
        Instantiate(equipment[item.itemID].worldItem, PM.transform.position + new Vector3(0, 0, 5), Quaternion.identity);
        Destroy(item.gameObject);
    }
}
