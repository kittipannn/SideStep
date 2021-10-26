using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public FloatingJoystick floatingJoystick;
    private Rigidbody rb;

    public float moveSpeed = 6f;
    public float movementMultiplier = 10f;
    float rbDrag = 6f;
    Vector3 movedirection;

    Vector3 slopeMoveDirection;
    RaycastHit slopeHit;
    float playerHeight = 2f;
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2))
        {
            if (slopeHit.normal != Vector3.up)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movementInput();
        controlDrag();

        slopeMoveDirection = Vector3.ProjectOnPlane(movedirection, slopeHit.normal);
    }
    private void FixedUpdate()
    {
        movementControl();
        rb.AddForce(movedirection * moveSpeed, ForceMode.Acceleration);
    }
    void movementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            movedirection = Vector3.forward * vertical + Vector3.right * horizontal;
        }
        else
        {
            movedirection = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;
        }
        if (horizontal == 0 && vertical == 0)
        {
            rb.useGravity = false;
            rb.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            rb.useGravity = true;
            Physics.gravity = new Vector3(0, -10f, 0);
        }

    }
    void controlDrag()
    {
        rb.drag = rbDrag;
    }
    void movementControl()
    {
        if (OnSlope())
        {
            rb.AddForce(slopeMoveDirection * moveSpeed, ForceMode.Acceleration);
        }
        else if (!OnSlope())
        {
            rb.AddForce(movedirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }


    }

}
