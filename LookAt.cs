using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform lookAtObject;
    // Start is called before the first frame update
    void Start()
    {
        if(lookAtObject == null)
        {
            lookAtObject = GameObject.Find("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(2 * transform.position - lookAtObject.position);
    }
}
