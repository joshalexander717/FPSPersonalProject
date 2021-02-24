using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotationOnX;
    public float sensitivity = 90f;
    public Transform player;
    public Overlay overlay;
    public bool isAiming;

    float xVelocity;
    float yVelocity;
    public float snappiness;

    private void Start()
    {
        overlay = GameObject.Find("Overlay").GetComponent<Overlay>();
    }

    private void Update()
    {
        if (!overlay.isSomethingOpen)
        {
            float mouseY = 0;
            float mouseX = 0;
            if (isAiming)
            {
                mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity / 2;
                mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity / 2;
            }
            else
            {
                mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
                mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;
            }

            xVelocity = Mathf.Lerp(xVelocity, mouseX, snappiness * Time.deltaTime);
            yVelocity = Mathf.Lerp(yVelocity, mouseY, snappiness * Time.deltaTime);

            rotationOnX -= yVelocity;
            rotationOnX = Mathf.Clamp(rotationOnX, -88f, 90f);



            transform.localRotation = Quaternion.Euler(rotationOnX, 0f, 0f);
            player.Rotate(Vector3.up * xVelocity);

            if (player.GetComponent<PlayerMovement>().isSliding)
            {
                Vector3 vel = player.GetComponent<Rigidbody>().velocity;
                player.GetComponent<Rigidbody>().velocity = new Vector3(vel.x * Mathf.Cos(Mathf.Deg2Rad * mouseX), vel.y, vel.z);
            }
        }
    }
}
