using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerMovement : MonoBehaviour
{
    Animator playerAnim;
    Rigidbody rb;
    [Tooltip("Force multiplier applied to the player cube.")]
    public float extraForce;
    [Tooltip("Force multiplier applied to the player cube for jumps (multiplied by extraForce).")]
    public float jumpForce;
    public float groundDistance;
    bool isGrounded = true;

    Transform groundCheck;
    LayerMask groundMask;

    void Awake()
    {
        groundCheck = transform.Find("GroundCheck");
        groundMask = LayerMask.GetMask("Ground");
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();    
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && Input.GetKeyDown("space"))
            rb.AddForce(Vector3.up * extraForce * jumpForce, ForceMode.Impulse);

        float horizontal = Input.GetAxis("Horizontal");

        rb.AddForce(Vector3.right * horizontal * extraForce * Time.deltaTime);

        Vector3 rotation = Vector3.forward * (rb.velocity.x / 5);
        transform.rotation = Quaternion.Euler(rotation);

        if (transform.position.y < -1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * rb.mass * 3);
    }
}
