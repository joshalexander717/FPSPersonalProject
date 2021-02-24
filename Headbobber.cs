using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbobber : MonoBehaviour
{

    float timer = 0.0f;
    float bobbingSpeed;
    float bobbingAmount;
    public float bobbingWalkSpeed;
    public float bobbingWalkAmount;
    public float bobbingSprintSpeed;
    public float bobbingSprintAmount;
    float midpoint = 0;
    public Transform player;

    void Update()
    {
        if(!player.GetComponent<PlayerMovement>().isGrounded)
        {
            bobbingAmount = 0;
            bobbingSpeed = 0;
        }
        else if(player.GetComponent<PlayerMovement>().isSprinting)
        {
            bobbingAmount = bobbingSprintAmount;
            bobbingSpeed = bobbingSprintSpeed;
        }
        else if(player.GetComponent<PlayerMovement>().isAiming)
        {
            bobbingAmount = bobbingWalkAmount / 1.5f; //STABILITY VARIABLE????
            bobbingSpeed = bobbingWalkSpeed / 1.5f;
        }
        else
        {
            bobbingAmount = bobbingWalkAmount;
            bobbingSpeed = bobbingWalkSpeed;
        }
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cSharpConversion = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer = timer + bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }
        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            cSharpConversion.y = midpoint + translateChange;
        }
        else
        {
            cSharpConversion.y = midpoint;
        }

        transform.localPosition = cSharpConversion;
    }



}
