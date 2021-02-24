using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public GameObject roof;
    public GameObject ship;


    public Boolean isOpen = false;

    void Start()
    {
        isOpen = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation;
        if (isOpen)
        {
            targetRotation = Quaternion.Euler(0,0,-45);
        }
        else
        {
            targetRotation = Quaternion.Euler(0,0,0);
        }
        
        
            roof.transform.localRotation = Quaternion.Lerp(roof.transform.localRotation, targetRotation, 2 * Time.deltaTime);
        
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
             isOpen = false;
        }
    }
}
