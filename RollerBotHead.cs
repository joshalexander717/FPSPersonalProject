using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerBotHead : MonoBehaviour
{
    GameObject targetObj;
    public bool betweenPoints;
    public Transform pointOne;
    public Transform pointTwo;
    public float detectionDistance;
    public Transform shootPoint;

    public float rotationSpeed;

    public bool detectedPlayer;

    void Start()
    {
        targetObj = GameObject.Find("Player");
    }
    void Update()
    {
        RaycastHit hit;
        Vector3 rayDirection = targetObj.transform.position - shootPoint.transform.position;

        if (Physics.Raycast(transform.position, rayDirection, out hit))
        {
            if (hit.transform.CompareTag("Player") && Vector3.Distance(targetObj.transform.position, shootPoint.transform.position) <= detectionDistance)
            {
                detectedPlayer = true;
            }
            else
            {
                detectedPlayer = false;
            }
        }

        if (detectedPlayer)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetObj.transform.position - shootPoint.transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }
}