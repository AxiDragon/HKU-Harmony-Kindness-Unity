using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerMovement : MonoBehaviour
{
    [HideInInspector]
    public static Rigidbody rb;
    [Tooltip("Force multiplier applied to the player cube.")]
    public float extraForce;
    [Tooltip("Force divider applied to the player cube for jumps (multiplied by extraForce).")]
    public float jumpDamping;
    public float groundDistance;
    bool isGrounded = true;

    Transform groundCheck;
    LayerMask groundMask;

    void Start()
    {
        groundCheck = transform.Find("GroundCheck");
        groundMask = LayerMask.GetMask("Ground");

        rb = GetComponent<Rigidbody>();
    }

    public void Reboot()
    {
        groundCheck = transform.Find("GroundCheck");
        groundMask = LayerMask.GetMask("Ground");

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && Input.GetKeyDown("space"))
            rb.AddForce(Vector3.up * extraForce / jumpDamping, ForceMode.Impulse);

        float horizontal = Input.GetAxis("Horizontal");

        rb.AddForce(Vector3.right * horizontal * extraForce * Time.deltaTime);

        Vector3 rotation = Vector3.forward * (rb.velocity.x / 5);
        transform.rotation = Quaternion.Euler(rotation);

        if (transform.position.y < -1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
