using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasePlayer : MonoBehaviour
{
    public bool isChasingPlayer;
    NavMeshAgent NM;
    public Transform target;
    public BeetleHit bh;


    // Start is called before the first frame update
    void Start()
    {
        isChasingPlayer = false;
        NM = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isChasingPlayer && !bh.attackingPlayer)
        {
            NM.destination = target.position;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            isChasingPlayer = true;
            target = col.transform;
        }
    }
}
