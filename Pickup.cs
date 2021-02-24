
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int itemID;

    void OnMouseOver()
    {
        //if (other.gameObject == GameManager.Instance.PM.gameObject)
        //{
            if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < 5)
            {
                GameManager.Instance.PickupItem(itemID);
                Destroy(gameObject);
            }
        //}
    }
}