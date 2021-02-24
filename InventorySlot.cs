using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryItem currentItem;
    public bool gun1Slot = false;
    public bool gun2Slot = false;
    public bool scope1Slot = false;
    public bool scope2Slot = false;
    public bool foregrip1Slot = false;
    public bool foregrip2Slot = false;
    public bool mag1Slot = false;
    public bool mag2Slot = false;
    public bool isFull = false;

    private GameManager GM;
    public Image image;
    private Sprite common;
    private Sprite uncommon;
    private Sprite rare;
    private Sprite legendary;
    private Sprite epic;


    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        image = GetComponent<Image>();
        common = Resources.Load<Sprite>("Slots/slotCommon");
        uncommon = Resources.Load<Sprite>("Slots/slotUncommon");
        rare = Resources.Load<Sprite>("Slots/slotRare");
        legendary = Resources.Load<Sprite>("Slots/slotLegendary");
        epic = Resources.Load<Sprite>("Slots/slotEpic");
        RefreshIcon();
    }
        


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventorySlot OriginalSlot = newItem.originalSlot.GetComponent<InventorySlot>();

            // Original Slot != This Slot //
            if (newItem.originalSlot != this.transform)
            {
                // Slot == Full //
                if (isFull)
                {
                    // Dragging Into Weapon Slot From Normal Slot
                    if (gun1Slot && newItem.equipType == 1 && OriginalSlot.currentItem.equipType == 1)
                    {
                        Debug.Log("Dropped Item: Full Weapon Slot");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inWeaponSlot
                        currentItem.inGun1Slot = true;
                        OriginalSlot.currentItem.inGun1Slot = false;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Weapon
                        GameManager.Instance.gun1ID = currentItem.itemID;
                        GameManager.Instance.SpawnGun1();
                    }
                    else if (gun2Slot && newItem.equipType == 1 && OriginalSlot.currentItem.equipType == 1)
                    {
                        Debug.Log("Dropped Item: Full Weapon Slot");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inWeaponSlot
                        currentItem.inGun2Slot = true;
                        OriginalSlot.currentItem.inGun2Slot = false;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Weapon
                        GameManager.Instance.gun2ID = currentItem.itemID;
                        GameManager.Instance.SpawnGun2();
                    }
                    // Dragging Into Armour Slot From Normal Slot
                    else if (scope1Slot && newItem.equipType == 2 && OriginalSlot.currentItem.equipType == 2)
                    {
                        Debug.Log("Dropped Item: Full Armour Slot");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inArmourSlot
                        currentItem.inScope1Slot = false;
                        OriginalSlot.currentItem.inScope1Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Armour
                        GameManager.Instance.scope1ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnScope1();
                    }
                    else if (scope2Slot && newItem.equipType == 2 && OriginalSlot.currentItem.equipType == 2)
                    {
                        Debug.Log("Dropped Item: Full Armour Slot");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inArmourSlot
                        currentItem.inScope2Slot = false;
                        OriginalSlot.currentItem.inScope2Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Armour
                        GameManager.Instance.scope2ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnScope2();
                    }
                    // Dragging Into Armour Slot From Normal Slot
                    else if (foregrip1Slot && newItem.equipType == 3 && OriginalSlot.currentItem.equipType == 3)
                    {
                        Debug.Log("Dropped Item: Full Armour Slot");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inArmourSlot
                        currentItem.inForegrip1Slot = false;
                        OriginalSlot.currentItem.inForegrip1Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Armour
                        GameManager.Instance.foregrip1ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnForegrip1();
                    }
                    else if (foregrip2Slot && newItem.equipType == 3 && OriginalSlot.currentItem.equipType == 3)
                    {
                        Debug.Log("Dropped Item: Full Armour Slot");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inArmourSlot
                        currentItem.inForegrip2Slot = false;
                        OriginalSlot.currentItem.inForegrip2Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Armour
                        GameManager.Instance.foregrip2ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnForegrip2();
                    }
                    // Dragging Into Armour Slot From Normal Slot
                    else if (mag1Slot && newItem.equipType == 4 && OriginalSlot.currentItem.equipType == 4)
                    {
                        Debug.Log("Dropped Item: Full Armour Slot");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inArmourSlot
                        currentItem.inMag1Slot = false;
                        OriginalSlot.currentItem.inMag1Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Armour
                        GameManager.Instance.mag1ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnMag1();
                    }
                    else if (mag2Slot && newItem.equipType == 4 && OriginalSlot.currentItem.equipType == 4)
                    {
                        Debug.Log("Dropped Item: Full Armour Slot");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inArmourSlot
                        currentItem.inMag2Slot = false;
                        OriginalSlot.currentItem.inMag2Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Armour
                        GameManager.Instance.mag2ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnMag2();
                    }




                    // Dragging Into Normal Slot From Weapon Slot
                    else if (!gun1Slot && currentItem.equipType == 1 && newItem.equipType == 1
                        && newItem.inGun1Slot)
                    {
                        Debug.Log("Dropped Item: Full Weapon Slot [Other]");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inWeaponSlot
                        currentItem.inGun1Slot = false;
                        OriginalSlot.currentItem.inGun1Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Weapon
                        GameManager.Instance.gun1ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnGun1();
                    }
                    else if (!gun2Slot && currentItem.equipType == 1 && newItem.equipType == 1
                        && newItem.inGun2Slot)
                    {
                        Debug.Log("Dropped Item: Full Weapon Slot [Other]");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inWeaponSlot
                        currentItem.inGun2Slot = false;
                        OriginalSlot.currentItem.inGun2Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Weapon
                        GameManager.Instance.gun2ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnGun2();
                    }
                    // Dragging Into Normal Slot From Armour Slot
                    else if (!scope1Slot && currentItem.equipType == 2 && newItem.equipType == 2
                        && newItem.inScope1Slot)
                    {
                        Debug.Log("Dropped Item: Full Armour Slot [Other]");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inArmourSlot
                        currentItem.inScope1Slot = false;
                        OriginalSlot.currentItem.inScope1Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Armour
                        GameManager.Instance.scope1ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnScope1();
                    }
                    else if (!scope2Slot && currentItem.equipType == 2 && newItem.equipType == 2
                        && newItem.inScope2Slot)
                    {
                        Debug.Log("Dropped Item: Full Armour Slot [Other]");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inArmourSlot
                        currentItem.inScope2Slot = false;
                        OriginalSlot.currentItem.inScope2Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Armour
                        GameManager.Instance.scope2ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnScope2();
                    }
                    // Dragging Into Normal Slot From Armour Slot
                    else if (!foregrip1Slot && currentItem.equipType == 3 && newItem.equipType == 3
                        && newItem.inForegrip1Slot)
                    {
                        Debug.Log("Dropped Item: Full Armour Slot [Other]");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inArmourSlot
                        currentItem.inForegrip1Slot = false;
                        OriginalSlot.currentItem.inForegrip1Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Armour
                        GameManager.Instance.foregrip1ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnForegrip1();
                    }
                    else if (!foregrip2Slot && currentItem.equipType == 3 && newItem.equipType == 3
                        && newItem.inForegrip2Slot)
                    {
                        Debug.Log("Dropped Item: Full Armour Slot [Other]");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inArmourSlot
                        currentItem.inForegrip2Slot = false;
                        OriginalSlot.currentItem.inForegrip2Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Armour
                        GameManager.Instance.foregrip2ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnForegrip2();
                    }
                    else if (!mag1Slot && currentItem.equipType == 4 && newItem.equipType == 4
                        && newItem.inMag1Slot)
                    {
                        Debug.Log("Dropped Item: Full Armour Slot [Other]");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inArmourSlot
                        currentItem.inMag1Slot = false;
                        OriginalSlot.currentItem.inMag1Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Armour
                        GameManager.Instance.mag1ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnMag1();
                    }
                    else if (!mag2Slot && currentItem.equipType == 4 && newItem.equipType == 4
                        && newItem.inMag2Slot)
                    {
                        Debug.Log("Dropped Item: Full Armour Slot [Other]");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping InventoryItem:inArmourSlot
                        currentItem.inMag2Slot = false;
                        OriginalSlot.currentItem.inMag2Slot = true;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;

                        // Spawning Armour
                        GameManager.Instance.mag2ID = OriginalSlot.currentItem.itemID;
                        GameManager.Instance.SpawnMag2();
                    }





                    // Slot Swapping In Inventory
                    else if (!OriginalSlot.currentItem.inScope1Slot && !OriginalSlot.currentItem.inGun1Slot && !OriginalSlot.currentItem.inForegrip1Slot && !OriginalSlot.currentItem.inMag1Slot && !currentItem.inGun1Slot && !currentItem.inScope1Slot && !currentItem.inForegrip1Slot && !currentItem.inMag1Slot &&
                             !OriginalSlot.currentItem.inScope2Slot && !OriginalSlot.currentItem.inGun2Slot && !OriginalSlot.currentItem.inForegrip2Slot && !OriginalSlot.currentItem.inMag2Slot && !currentItem.inGun2Slot && !currentItem.inScope2Slot && !currentItem.inForegrip2Slot && !currentItem.inMag2Slot)
                    {
                        Debug.Log("Dropped Item: Full Inventory Slot");

                        // Swapping InventoryItem:currentItem 
                        InventoryItem swapCurrent = currentItem;
                        currentItem = newItem;
                        OriginalSlot.currentItem = swapCurrent;

                        // Swapping Parent
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        OriginalSlot.currentItem.transform.SetParent(OriginalSlot.transform);
                        OriginalSlot.currentItem.originalSlot = OriginalSlot.transform;
                    }
                    // Returning To Original Slot
                    else
                    {
                        Debug.Log("Dropped Item: Returning to original slot");
                    }
                }
                // Slot != Full //
                else if (!isFull)
                {
                    // Moving Into Weapon Slot
                    if (gun1Slot && newItem.equipType == 1)
                    {
                        Debug.Log("Dropped Item: Empty Weapon Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inGun1Slot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Weapon
                        GameManager.Instance.gun1ID = currentItem.itemID;
                        GameManager.Instance.SpawnGun1();
                    }
                    // Moving Into Weapon Slot
                    else if (foregrip1Slot && newItem.equipType == 3)
                    {
                        

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inForegrip1Slot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Weapon
                        GameManager.Instance.foregrip1ID = currentItem.itemID;
                        GameManager.Instance.SpawnForegrip1();
                        Debug.Log("Spawned Foregrip 1");
                    }
                    // Moving Into Weapon Slot
                    else if (mag1Slot && newItem.equipType == 4)
                    {
                        Debug.Log("Dropped Item: Empty Weapon Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inMag1Slot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Weapon
                        GameManager.Instance.mag1ID = currentItem.itemID;
                        GameManager.Instance.SpawnMag1();
                    }
                    // Moving Into Armour Slot
                    else if (scope1Slot && newItem.equipType == 2)
                    {
                        Debug.Log("Dropped Item: Empty Armour Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inScope1Slot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Armour
                        GameManager.Instance.scope1ID = currentItem.itemID;
                        GameManager.Instance.SpawnScope1();

                    }

                    // Moving Into Weapon Slot
                    else if (gun2Slot && newItem.equipType == 1)
                    {
                        Debug.Log("Dropped Item: Empty Weapon Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inGun2Slot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Weapon
                        GameManager.Instance.gun2ID = currentItem.itemID;
                        GameManager.Instance.SpawnGun2();
                    }
                    // Moving Into Weapon Slot
                    else if (foregrip2Slot && newItem.equipType == 3)
                    {
                        Debug.Log("Dropped Item: Empty Weapon Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inForegrip2Slot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Weapon
                        GameManager.Instance.foregrip2ID = currentItem.itemID;
                        GameManager.Instance.SpawnForegrip2();
                    }
                    // Moving Into Weapon Slot
                    else if (mag2Slot && newItem.equipType == 4)
                    {
                        Debug.Log("Dropped Item: Empty Weapon Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inMag1Slot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Weapon
                        GameManager.Instance.mag2ID = currentItem.itemID;
                        GameManager.Instance.SpawnMag2();
                    }
                    // Moving Into Armour Slot
                    else if (scope2Slot && newItem.equipType == 2)
                    {
                        Debug.Log("Dropped Item: Empty Armour Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inScope2Slot = true;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Spawning Armour
                        GameManager.Instance.scope2ID = currentItem.itemID;
                        GameManager.Instance.SpawnScope2();

                    }



                    // Moving Out Of Weapon Slot
                    else if (OriginalSlot.gun1Slot == true && newItem.inGun1Slot == true && !scope1Slot && !mag1Slot && !foregrip1Slot && !scope2Slot && !mag2Slot && !foregrip2Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inGun1Slot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Weapon
                        GameManager.Instance.gun1ID = -1;
                        GameManager.Instance.DestroyGun1();
                    }
                    else if (OriginalSlot.gun2Slot == true && newItem.inGun2Slot == true && !scope1Slot && !mag1Slot && !foregrip1Slot && !scope2Slot && !mag2Slot && !foregrip2Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inGun2Slot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Weapon
                        GameManager.Instance.gun2ID = -1;
                        GameManager.Instance.DestroyGun2();
                    }
                    // Moving Out Of Armour Slot
                    else if (OriginalSlot.scope1Slot == true && newItem.inScope1Slot == true && !gun1Slot && !mag1Slot && !foregrip1Slot && !gun2Slot && !mag2Slot && !foregrip2Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inScope1Slot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Weapon
                        GameManager.Instance.scope1ID = -1;
                        GameManager.Instance.SpawnScope1();
                    }
                    // Moving Out Of Armour Slot
                    else if (OriginalSlot.scope2Slot == true && newItem.inScope2Slot == true && !gun1Slot && !mag1Slot && !foregrip1Slot && !gun2Slot && !mag2Slot && !foregrip2Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inScope2Slot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Weapon
                        GameManager.Instance.scope2ID = -1;
                        GameManager.Instance.SpawnScope2();
                    }
                    // Moving Out Of Armour Slot
                    else if (OriginalSlot.foregrip1Slot == true && newItem.inForegrip1Slot == true && !gun1Slot && !mag1Slot && !scope1Slot && !gun2Slot && !mag2Slot && !scope2Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inForegrip1Slot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Weapon
                        GameManager.Instance.foregrip1ID = -1;
                        GameManager.Instance.SpawnForegrip1();
                    }
                    // Moving Out Of Armour Slot
                    else if (OriginalSlot.foregrip2Slot == true && newItem.inForegrip2Slot == true && !gun1Slot && !mag1Slot && !scope1Slot && !gun2Slot && !mag2Slot && !scope2Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inForegrip2Slot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Weapon
                        GameManager.Instance.foregrip2ID = -1;
                        GameManager.Instance.SpawnForegrip2();
                    }
                    // Moving Out Of Armour Slot
                    else if (OriginalSlot.mag1Slot == true && newItem.inMag1Slot == true && !gun1Slot && !scope1Slot && !foregrip1Slot && !gun2Slot && !scope2Slot && !foregrip2Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inMag1Slot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Weapon
                        GameManager.Instance.mag1ID = -1;
                        GameManager.Instance.SpawnMag1();
                    }
                    else if (OriginalSlot.mag2Slot == true && newItem.inMag2Slot == true && !gun1Slot && !scope1Slot && !foregrip1Slot && !gun2Slot && !scope2Slot && !foregrip2Slot)
                    {
                        Debug.Log("Dropped Item: Empty Inventory Slot");

                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;
                        currentItem.inMag2Slot = false;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;

                        // Destroying Weapon
                        GameManager.Instance.mag2ID = -1;
                        GameManager.Instance.SpawnMag2();
                    }



                    // Moving To Empty Slot
                    else if (!OriginalSlot.currentItem.inScope1Slot && !OriginalSlot.currentItem.inGun1Slot && !OriginalSlot.currentItem.inForegrip1Slot && !OriginalSlot.currentItem.inMag1Slot && !currentItem.inGun1Slot && !currentItem.inScope1Slot && !currentItem.inForegrip1Slot && !currentItem.inMag1Slot &&
                             !OriginalSlot.currentItem.inScope2Slot && !OriginalSlot.currentItem.inGun2Slot && !OriginalSlot.currentItem.inForegrip2Slot && !OriginalSlot.currentItem.inMag2Slot && !currentItem.inGun2Slot && !currentItem.inScope2Slot && !currentItem.inForegrip2Slot && !currentItem.inMag2Slot)
                    {
                        // Emptying Original Slot
                        OriginalSlot.isFull = false;
                        OriginalSlot.currentItem = null;

                        // Filling This Slot
                        isFull = true;
                        currentItem = newItem;

                        // Swapping Parents
                        eventData.pointerDrag.transform.SetParent(gameObject.transform);
                        currentItem.originalSlot = this.transform;
                    }
                    // Returning to Original Slot
                    else
                    {
                        Debug.Log("Dropped Item: Returning to original slot");
                    }
                }
            }

            //Reset Current Icon
            RefreshIcon();

            //Other Slot set outline
            if (OriginalSlot.currentItem == null)
            {
                OriginalSlot.image.sprite = common;
            }
            else if (GM.equipment[OriginalSlot.currentItem.itemID].name.Contains("Uncommon"))
            {
                OriginalSlot.image.sprite = uncommon;
            }
            else if (GM.equipment[OriginalSlot.currentItem.itemID].name.Contains("Rare"))
            {
                OriginalSlot.image.sprite = rare;
            }
            else if (GM.equipment[OriginalSlot.currentItem.itemID].name.Contains("Legendary"))
            {
                OriginalSlot.image.sprite = legendary;
            }
            else if (GM.equipment[OriginalSlot.currentItem.itemID].name.Contains("Epic"))
            {
                OriginalSlot.image.sprite = epic;
            }
            else
            {
                OriginalSlot.image.sprite = common;
            }
        }
    }

    public void RefreshIcon()
    {
        //Current slot set outline
        if (currentItem == null)
        {
            image.sprite = common;
        }
        else if (GM.equipment[currentItem.itemID].name.Contains("Uncommon"))
        {
            image.sprite = uncommon;
        }
        else if (GM.equipment[currentItem.itemID].name.Contains("Rare"))
        {
            image.sprite = rare;
        }
        else if (GM.equipment[currentItem.itemID].name.Contains("Legendary"))
        {
            image.sprite = legendary;
        }
        else if (GM.equipment[currentItem.itemID].name.Contains("Epic"))
        {
            image.sprite = epic;
        }
        else
        {
            image.sprite = common;
        }
    }
}