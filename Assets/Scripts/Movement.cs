using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour

{
    private Rigidbody rb;

    public float jumpForce = 112f;
    //public float groundCheckDistance;
    //private bool isGrounded = false;
    public KeyCode rotateKey;
    public float jumpInterval;
    private Vector3 movementDirection;
    bool canMove = true;

    private void Start()
    {
        movementDirection = transform.forward;
        rb = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(Jump), jumpInterval, jumpInterval);
    }

    private void Update()
    {
        if (Input.GetKeyDown(rotateKey))
        {
            canMove = false;
            Debug.Log("Stopped");
        }
        if (Input.GetKeyUp(rotateKey))
        {
            canMove = true;
        }
    }

    void Jump()
    {
        AdjustPositionAndRotation(new Vector3(0, 0, 0));
        rb.AddForce(new Vector3(0, jumpForce, 0));

        if(canMove == true)
        {
            rb.AddForce(this.transform.forward * jumpForce);
        }
        else
        {
            transform.rotation *= Quaternion.Euler(0, 90, 0);
        }
    }    

    void AdjustPositionAndRotation(Vector3 newRotation)
    {
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
    }

}
