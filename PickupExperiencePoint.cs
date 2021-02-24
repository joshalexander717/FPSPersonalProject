using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupExperiencePoint : MonoBehaviour
{
    public int xpAmount;
    public GameObject xpParticle;
    
    Transform playerPickedUp;
    bool pickedUp;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!pickedUp && Vector3.Distance(transform.position, player.transform.position) <= 10f)
        {
            playerPickedUp = player.transform;
            pickedUp = true;
        }
        else if (pickedUp)
        {
            
                xpParticle.transform.position = Vector3.Slerp(xpParticle.transform.position, playerPickedUp.position, 15 * Time.deltaTime);
            if (Vector3.Distance(transform.position, playerPickedUp.position) <= .4f)
            {
                playerPickedUp.GetComponent<Experience>().GainExp(xpAmount);
                Destroy(xpParticle);
            }
        }
    }
}
