using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{
    public Canvas HUD;
    public Canvas Inventory;

    public bool isSomethingOpen;

    // Start is called before the first frame update
    void Start()
    {
        CloseInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            if(!isSomethingOpen)
                OpenInventory();
            else
                CloseInventory();
        }

    }

    void OpenInventory()
    {
        HUD.enabled = false;
        Inventory.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        isSomethingOpen = true;
    }

    void CloseInventory()
    {
        HUD.enabled = true;
        Inventory.enabled = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isSomethingOpen = false;
    }
}
