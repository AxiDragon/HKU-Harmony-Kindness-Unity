using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMovement : MonoBehaviour
{
    Animator playerAnim;
    Rigidbody rb;
    [Tooltip("Force multiplier applied to the player cube.")]
    public float extraForce;
    public float groundDistance;
    bool isGrounded = true;

    Camera mainCamera;

    Transform groundCheck;
    LayerMask groundMask;


    void Start()
    {
        mainCamera = transform.Find("Main Camera").gameObject.GetComponent<Camera>();
        groundCheck = transform.Find("GroundCheck");
        groundMask = LayerMask.GetMask("Ground");
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();    
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float jump = 0;

        if (isGrounded)
            jump = Input.GetAxis("Jump");

        float horizontal = Input.GetAxis("Horizontal");
        rb.MovePosition(rb.position + (Vector3.right * horizontal * extraForce * Time.deltaTime));
        rb.AddForce(Vector3.right * horizontal * extraForce * Time.deltaTime, ForceMode.Impulse);
        rb.AddForce(Vector3.up * jump * extraForce * 10 * Time.deltaTime, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * rb.mass * 3);
    }
}
