using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerBotMovement : MonoBehaviour
{
    public float distanceToStop;
    public float speed;
    public Rigidbody rb;
    Transform target;
    public RollerBotHead head;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var direction = Vector3.zero;
        if (Vector3.Distance(transform.position, target.position) > distanceToStop && head.detectedPlayer)
        {
            rb.AddForce((target.position - transform.position).normalized * speed);
        }
    }


}
