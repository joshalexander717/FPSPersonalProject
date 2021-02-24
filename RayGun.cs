using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public RollerBotHead head;
    public float shootRate;
    private float m_shootRateTimeStamp;
    
    public GameObject m_shotPrefab;

    RaycastHit hit;
    float range = 1000.0f;

    public Transform turret1;
    public Transform turret2;

    void Update()
    {

        if (head.detectedPlayer)
        {
            if (Time.time > m_shootRateTimeStamp)
            {
                shootRay();
                m_shootRateTimeStamp = Time.time + shootRate;
            }
        }

    }

    void shootRay()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            GameObject laser = GameObject.Instantiate(m_shotPrefab, turret1.transform.position, turret1.transform.rotation) as GameObject;
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            GameObject.Destroy(laser, 2f);
            GameObject laser2 = GameObject.Instantiate(m_shotPrefab, turret2.transform.position, turret2.transform.rotation) as GameObject;
            laser2.GetComponent<ShotBehavior>().setTarget(hit.point);
            GameObject.Destroy(laser2, 2f);

            GetComponent<AudioSource>().Play();
        }

    }



}