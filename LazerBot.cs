using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBot : MonoBehaviour
{
    public float damage;
    public float damageRate;
    float currDamageTimer;

    public LazerStand ls;

    public ParticleSystem ps;
    AudioSource As;
    // Use this for initialization
    void Start()
    {
        As = GetComponent<AudioSource>();
        
        currDamageTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(ls.detectedPlayer)
            ShootLazer();
        else
        {
            if (ps.isPlaying)
                ps.Stop();
        }
            
    }

    void ShootLazer()
    {
        if(!ps.isPlaying)
        {
            ps.Play();
        }
        if(!As.isPlaying)
            As.Play();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    if (currDamageTimer <= 0)
                    {
                        hit.collider.GetComponent<Player>().TakeDamage(damage);
                        currDamageTimer = damageRate;
                    }
                    else
                        currDamageTimer -= Time.deltaTime;
                }
            }
        }
    }
}
