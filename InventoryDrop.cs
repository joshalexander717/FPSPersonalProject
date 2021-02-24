using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            InventoryItem newItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            InventorySlot OriginalSlot = newItem.originalSlot.GetComponent<InventorySlot>();

            // Revert OnDrag Changes
            newItem.canvasGroup.blocksRaycasts = true;

            if (newItem.inGun1Slot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.gun1ID = -1;
                GameManager.Instance.DestroyGun1();
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inGun2Slot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.gun2ID = -1;
                GameManager.Instance.DestroyGun2();
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inMag1Slot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.mag1ID = -1;
                GameManager.Instance.DestroyMag1();
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inMag2Slot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.mag2ID = -1;
                GameManager.Instance.DestroyMag2();
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inScope1Slot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.scope1ID = -1;
                GameManager.Instance.DestroyScope1();
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inScope2Slot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.scope2ID = -1;
                GameManager.Instance.DestroyScope2();
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inForegrip1Slot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.foregrip1ID = -1;
                GameManager.Instance.DestroyForegrip1();
                GameManager.Instance.DropItem(newItem);
            }
            else if (newItem.inForegrip2Slot)
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.foregrip2ID = -1;
                GameManager.Instance.DestroyForegrip2();
                GameManager.Instance.DropItem(newItem);
            }
            else
            {
                OriginalSlot.currentItem = null;
                OriginalSlot.isFull = false;
                GameManager.Instance.DropItem(newItem);
            }

        }
    }
}