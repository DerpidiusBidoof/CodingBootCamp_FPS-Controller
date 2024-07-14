using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public InputManager inputManager;

    public Rigidbody rb;

    public float speed = 10;
    public float runSpeed = 15;
    public float jumpForce = 300;

    private bool isGrounded;

    void Start()
    {
        // Subscribe to the Jump action in the input manager when it starts
        inputManager.inputMaster.Movement.Jump.started += _ => Jump();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAround();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an object tagged as "Ground"
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        // Only jump if the player is grounded, Apply upward force for jumping
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    void MoveAround()
    {
        // Read input values for forward and right movement
        float foward = inputManager.inputMaster.Movement.Forward.ReadValue<float>();
        float right = inputManager.inputMaster.Movement.Right.ReadValue<float>();

        // Calculate the movement direction based on input
        Vector3 move = transform.right * right + transform.forward * foward;

        // Adjust movement speed based on whether the player is running or walking
        move *= inputManager.inputMaster.Movement.Run.ReadValue<float>() == 0 ? speed : runSpeed;
        
        // Adjust player scale based on crouching input (not commonly used in this context)
        transform.localScale = new Vector3(1, inputManager.inputMaster.Movement.Crouch.ReadValue<float>() == 0 ? 1f : 0.72618f, 1);
        // Update the player's Rigidbody velocity to move the player
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }
}
