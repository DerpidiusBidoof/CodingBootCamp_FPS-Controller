using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public InputManager inputManager;

    public Rigidbody rb;

    public float speed = 10;
    public float runSpeed = 15;
    public float jumpForce = 200;

    private bool isGrounded;

    private void Awake()
    {
        //inputManager.inputMaster.Movement.Jump.started += _ => Jump();
    }

    // Start is called before the first frame update
    void Start()
    {
        inputManager.inputMaster.Movement.Jump.started += _ => Jump();
    }

    // Update is called once per frame
    void Update()
    {
        float foward = inputManager.inputMaster.Movement.Forward.ReadValue<float>();
        float right = inputManager.inputMaster.Movement.Right.ReadValue<float>();
        Vector3 move = transform.right * right + transform.forward * foward;

        move *= inputManager.inputMaster.Movement.Run.ReadValue<float>() == 0 ? speed : runSpeed;
        transform.localScale = new Vector3(1, inputManager.inputMaster.Movement.Crouch.ReadValue<float>() == 0 ? 1f : 0.72618f, 1);

        rb.velocity = new Vector3 (move.x, rb.velocity.y, move.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
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
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}
