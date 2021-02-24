using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTarget : MonoBehaviour
{
    public Transform fallPoint;
    bool isFalling = true;
    float fallTimer;

    // Start is called before the first frame update
    void Start()
    {
        fallTimer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Target>().health <= 0)
        {
            isFalling = true;
        }

        Quaternion targetRotation;
        if (isFalling)
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
            fallTimer -= Time.deltaTime;
            if(fallTimer <= 0)
            {
                isFalling = false;
                fallTimer = 5;
                GetComponent<Target>().health = GetComponent<Target>().maxHealth;
            }
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }

        fallPoint.transform.localRotation = Quaternion.Lerp(fallPoint.transform.localRotation, targetRotation, 8 * Time.deltaTime);

    }

    void Fall()
    {

    }
}
