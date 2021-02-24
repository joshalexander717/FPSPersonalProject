using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAmmo : MonoBehaviour
{
    Transform playerPickedUp;
    bool pickedUp;

    public int smgAmmo;
    public int rifleAmmo;
    public int sniperAmmo;
    public int heavyAmmo;
    public int shotgunAmmo;

    private GameObject player;
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
            transform.position = Vector3.Slerp(transform.position, playerPickedUp.position, 15 * Time.deltaTime);
            if(Vector3.Distance(transform.position, playerPickedUp.position) <= .4f)
            {
                playerPickedUp.GetComponent<Player>().subAmmoReserve += smgAmmo;
                playerPickedUp.GetComponent<Player>().rifleAmmoReserve += rifleAmmo;
                playerPickedUp.GetComponent<Player>().sniperAmmoReserve += sniperAmmo;
                playerPickedUp.GetComponent<Player>().heavyAmmoReserve += heavyAmmo;
                playerPickedUp.GetComponent<Player>().shotgunAmmoReserve += shotgunAmmo;
                Destroy(gameObject);
            }
        }
    }
}
