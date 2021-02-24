using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveForward;
    float moveSide;
    bool moveUp;
    bool crouch;
    public bool isSliding;

    public float speed = 5f;
    public float sprintSpeed;
    public bool isSprinting;
    public bool isAiming;
    public float jumpSpeed = 3f;
    public int jumpsAllowed = 2;
    int currJumpsAllowed;

    Rigidbody rb;

    public bool isGrounded;

    CapsuleCollider playerCol;
    float originalHeight;
    public bool isCrouched;
    public float crouchHeight;
    public float slideSpeed = 3f;

    float keepSpeed;

    AudioSource As;
    public AudioClip jumpSound;
    public AudioClip flightSound;
    public ParticleSystem flightParticles;

    public bool isUsing;

    Overlay overlay;

    CameraMovement cm;

    // Start is called before the first frame update
    void Start()
    {
        isCrouched = false;
        As = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        currJumpsAllowed = jumpsAllowed;
        playerCol = GetComponent<CapsuleCollider>();
        originalHeight = playerCol.height;
        isSliding = false;
        overlay = GameObject.Find("Overlay").GetComponent<Overlay>();
        cm = GameObject.Find("Head").GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        cm.isAiming = isAiming;
        if(isUsing || overlay.isSomethingOpen)
        {
            moveForward = 0;
            moveForward = 0;
        }
        //Get user input
        else if(!isGrounded)
        {
            moveForward = Input.GetAxis("Vertical") * keepSpeed;
            moveSide = Input.GetAxis("Horizontal") * keepSpeed;
            if(!flightParticles.isPlaying)
                flightParticles.Play();
        }
        else if (isGrounded && !isCrouched && isSprinting)
        {
            flightParticles.Stop();
            moveForward = Input.GetAxis("Vertical") * sprintSpeed;
            moveSide = Input.GetAxis("Horizontal") * sprintSpeed;
            keepSpeed = sprintSpeed;
            isSprinting = true;
        }
        else if(isAiming)
        {
            flightParticles.Stop();
            moveForward = Input.GetAxis("Vertical") * speed / 1.5f;
            moveSide = Input.GetAxis("Horizontal") * speed / 1.5f;
            keepSpeed = speed / 1.5f;
            isSprinting = false;
        }
        else
        {
            flightParticles.Stop();
            moveForward = Input.GetAxis("Vertical") * speed;
            moveSide = Input.GetAxis("Horizontal") * speed;
            keepSpeed = speed;
            isSprinting = false;
        }
        moveUp = Input.GetButtonDown("Jump");
        crouch = Input.GetButtonDown("Crouch");

        //Move player
        if(!isSliding)
            rb.velocity = transform.forward * moveForward + transform.right * moveSide + transform.up * rb.velocity.y;
        

        //Make character jump
        Jump();

        Crouch();
    }

    //Method for jumping
    void Jump()
    {
        if(isCrouched)
            return;
        
        if (moveUp)
        {
            //Single jump
            if (isGrounded)
            {
                As.clip = jumpSound;
                As.Play();
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
                isGrounded = false;
                currJumpsAllowed--;
            }
            //Double jump (or more)
            else if(!isGrounded && currJumpsAllowed > 0)
            {
                As.clip = jumpSound;
                As.Play();
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
                currJumpsAllowed--;
            }
        }
    }

    void Crouch()
    {
        if (!isGrounded)
            return;
        if(crouch && !isCrouched)
        {
            isCrouched = true;
            playerCol.height = crouchHeight;
            if (moveForward > 0)
                StartCoroutine(Slide());
        }
        else if (crouch && isCrouched)
        {
            playerCol.height = originalHeight;
            isCrouched = false;
            isSliding = false;
        }
    }

    IEnumerator Slide()
    {
        isSliding = true;
        rb.AddForce(transform.forward * slideSpeed, ForceMode.VelocityChange);
        yield return new WaitForSeconds(1f);
        isSliding = false;
    }

    //Check for player grounded for jump
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ground")
        {
            isGrounded = true;
            currJumpsAllowed = jumpsAllowed;
        }
    }
}
