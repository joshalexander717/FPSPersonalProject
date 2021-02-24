using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    public Transform originalSlot;

    public bool inGun1Slot = false;
    public bool inGun2Slot = false;
    public bool inScope1Slot = false;
    public bool inScope2Slot = false;
    public bool inForegrip1Slot = false;
    public bool inForegrip2Slot = false;
    public bool inMag1Slot = false;
    public bool inMag2Slot = false;


    public int itemID;
    public int equipType = 0; // 0 = None | 1 = Weapon | 2 = Armour //

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GameManager.Instance.interfaceCanvas.scaleFactor;
        gameObject.transform.SetParent(GameManager.Instance.draggables);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        rectTransform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
        originalSlot = transform.parent.transform;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // Revert State //
        canvasGroup.blocksRaycasts = true;
        rectTransform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
        if (transform.parent == GameManager.Instance.draggables)
        {
            transform.SetParent(originalSlot);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("DSGSD");
        if (eventData.button == PointerEventData.InputButton.Right && !eventData.dragging)
        {
            InventorySlot currentSlot = transform.parent.GetComponent<InventorySlot>();

            // If this item is in the weapon slot
            if (currentSlot.gun1Slot)
            {
                for (int i = 8; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;

                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;

                        // Changing 
                        inGun1Slot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyGun1();

                        break;
                    }
                }
            }
            // If this item is in the armour slot
            else if (currentSlot.scope1Slot)
            {
                for (int i = 8; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;

                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;

                        // Changing 
                        inScope1Slot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyScope1();

                        break;
                    }
                }
            }
            else if (currentSlot.foregrip1Slot)
            {
                for (int i = 8; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;

                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;

                        // Changing 
                        inForegrip1Slot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyForegrip1();

                        break;
                    }
                }
            }
            else if (currentSlot.mag1Slot)
            {
                for (int i = 8; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;

                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;

                        // Changing 
                        inMag1Slot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyMag1();

                        break;
                    }
                }
            }
            // If this item is in the weapon slot
            if (currentSlot.gun2Slot)
            {
                for (int i = 8; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;

                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;

                        // Changing 
                        inGun2Slot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyGun2();

                        break;
                    }
                }
            }
            // If this item is in the armour slot
            else if (currentSlot.scope2Slot)
            {
                for (int i = 8; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;

                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;

                        // Changing 
                        inScope2Slot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyScope2();

                        break;
                    }
                }
            }
            else if (currentSlot.foregrip2Slot)
            {
                for (int i = 8; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;

                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;

                        // Changing 
                        inForegrip2Slot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyForegrip2();

                        break;
                    }
                }
            }
            else if (currentSlot.mag2Slot)
            {
                for (int i = 8; i < GameManager.Instance.inventorySlots.Length; i++)
                {
                    Debug.Log("Searching for Slot...");
                    if (GameManager.Instance.inventorySlots[i].isFull == false)
                    {
                        Debug.Log("Found slot: " + GameManager.Instance.inventorySlots[i].name);

                        // Emptying Previous Slot
                        currentSlot.isFull = false;
                        currentSlot.currentItem = null;

                        // Occupying New Slot
                        GameManager.Instance.inventorySlots[i].currentItem = this;
                        GameManager.Instance.inventorySlots[i].isFull = true;

                        // Changing 
                        inMag2Slot = false;
                        transform.SetParent(GameManager.Instance.inventorySlots[i].transform);
                        GameManager.Instance.DestroyMag2();

                        break;
                    }
                }
            }



            // If this item is NOT in the weapon slot and Equip Type == Weapon
            else if (!currentSlot.gun1Slot && equipType == 1)
            {
                if (GameManager.Instance.inventorySlots[0].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[0].currentItem;
                    currentSlot.currentItem.inGun1Slot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[0].currentItem = this;
                    inGun1Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.gun1ID = itemID;
                    GameManager.Instance.SpawnGun1();
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[0].currentItem = this;
                    GameManager.Instance.inventorySlots[0].isFull = true;
                    inGun1Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.gun1ID = itemID;
                    GameManager.Instance.SpawnGun1();
                }
            }
            // If this item is NOT in the armour slot and Equip Type == Armour
            else if (!currentSlot.scope1Slot && equipType == 2)
            {
                if (GameManager.Instance.inventorySlots[1].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[1].currentItem;
                    currentSlot.currentItem.inScope1Slot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[1].currentItem = this;
                    inScope1Slot = true;
                    transform.SetParent(GameManager.Instance.inventorySlots[1].transform);
                    GameManager.Instance.scope1ID = itemID;
                    GameManager.Instance.SpawnScope1();
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[1].currentItem = this;
                    GameManager.Instance.inventorySlots[1].isFull = true;
                    inScope1Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[1].transform);
                    GameManager.Instance.scope1ID = itemID;
                    GameManager.Instance.SpawnScope1();
                }
            }
            else if (!currentSlot.foregrip1Slot && equipType == 3)
            {
                if (GameManager.Instance.inventorySlots[2].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[0].currentItem;
                    currentSlot.currentItem.inForegrip1Slot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[2].currentItem = this;
                    inForegrip1Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.foregrip1ID = itemID;
                    GameManager.Instance.SpawnForegrip1();
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[2].currentItem = this;
                    GameManager.Instance.inventorySlots[2].isFull = true;
                    inForegrip1Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.foregrip1ID = itemID;
                    GameManager.Instance.SpawnForegrip1();
                }
            }
            else if (!currentSlot.mag1Slot && equipType == 4)
            {
                if (GameManager.Instance.inventorySlots[3].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[0].currentItem;
                    currentSlot.currentItem.inMag1Slot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[3].currentItem = this;
                    inMag1Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.mag1ID = itemID;
                    GameManager.Instance.SpawnMag1();
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[3].currentItem = this;
                    GameManager.Instance.inventorySlots[3].isFull = true;
                    inMag1Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.mag1ID = itemID;
                    GameManager.Instance.SpawnMag1();
                }
            }
            // If this item is NOT in the weapon slot and Equip Type == Weapon
            else if (!currentSlot.gun2Slot && equipType == 1)
            {
                if (GameManager.Instance.inventorySlots[0].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[0].currentItem;
                    currentSlot.currentItem.inGun2Slot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[0].currentItem = this;
                    inGun2Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.gun2ID = itemID;
                    GameManager.Instance.SpawnGun2();
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[0].currentItem = this;
                    GameManager.Instance.inventorySlots[0].isFull = true;
                    inGun1Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.gun1ID = itemID;
                    GameManager.Instance.SpawnGun1();
                }
            }
            // If this item is NOT in the armour slot and Equip Type == Armour
            else if (!currentSlot.scope2Slot && equipType == 2)
            {
                if (GameManager.Instance.inventorySlots[1].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[1].currentItem;
                    currentSlot.currentItem.inScope2Slot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[1].currentItem = this;
                    inScope2Slot = true;
                    transform.SetParent(GameManager.Instance.inventorySlots[1].transform);
                    GameManager.Instance.scope2ID = itemID;
                    GameManager.Instance.SpawnScope2();
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[1].currentItem = this;
                    GameManager.Instance.inventorySlots[1].isFull = true;
                    inScope2Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[1].transform);
                    GameManager.Instance.scope2ID = itemID;
                    GameManager.Instance.SpawnScope2();
                }
            }
            else if (!currentSlot.foregrip2Slot && equipType == 3)
            {
                if (GameManager.Instance.inventorySlots[2].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[2].currentItem;
                    currentSlot.currentItem.inForegrip1Slot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[2].currentItem = this;
                    inForegrip2Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.foregrip2ID = itemID;
                    GameManager.Instance.SpawnForegrip2();
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[2].currentItem = this;
                    GameManager.Instance.inventorySlots[2].isFull = true;
                    inForegrip2Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.foregrip2ID = itemID;
                    GameManager.Instance.SpawnForegrip2();
                }
            }
            else if (!currentSlot.mag2Slot && equipType == 4)
            {
                if (GameManager.Instance.inventorySlots[3].isFull)
                {
                    // Setting Inventory Slot
                    currentSlot.currentItem = GameManager.Instance.inventorySlots[0].currentItem;
                    currentSlot.currentItem.inMag2Slot = false;
                    currentSlot.currentItem.gameObject.transform.SetParent(currentSlot.transform);

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[3].currentItem = this;
                    inMag2Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.mag2ID = itemID;
                    GameManager.Instance.SpawnMag2();
                }
                else
                {
                    // Resetting Old Slot
                    currentSlot.currentItem = null;
                    currentSlot.isFull = false;

                    // Setting Weapon Slot
                    GameManager.Instance.inventorySlots[3].currentItem = this;
                    GameManager.Instance.inventorySlots[3].isFull = true;
                    inMag2Slot = true;
                    gameObject.transform.SetParent(GameManager.Instance.inventorySlots[0].transform);
                    GameManager.Instance.mag2ID = itemID;
                    GameManager.Instance.SpawnMag2();
                }
            }
        }
    }
}