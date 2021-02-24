using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleHit : MonoBehaviour
{
    public float damage;

    Animator anim;

    public bool attackingPlayer;

    public EnemyChasePlayer ecp;

    float damageCounter;
    float damageCounterNomral;

    // Start is called before the first frame update
    void Start()
    {
        damageCounterNomral = 1f;
        damageCounter = damageCounterNomral;
        attackingPlayer = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ecp.isChasingPlayer)
        {
            if (attackingPlayer)
            {
                anim.SetBool("Walk Forward", false);
                anim.SetTrigger("Smash Attack");
                damageCounter -= Time.deltaTime;
                if(damageCounter < 0)
                {
                    DealDamage(damage);
                    damageCounter = damageCounterNomral;
                }
            }
            else
            {
                anim.SetBool("Walk Forward", true);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            damageCounter = damageCounterNomral;
            attackingPlayer = true;
        }

    }
    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            attackingPlayer = false;
        }
    }

    void DealDamage(float damageDealt)
    {
        ecp.target.GetComponent<Player>().TakeDamage(damage);
    }
}
