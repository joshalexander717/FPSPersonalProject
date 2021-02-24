using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerBotFollow : MonoBehaviour
{
    public GameObject followObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followObj.transform.position;
    }
}
