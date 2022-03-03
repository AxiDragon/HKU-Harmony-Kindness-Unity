using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMovement : MonoBehaviour
{
    Rigidbody rb;
    [Tooltip("Force multiplier applied to the player cube.")]
    public float extraForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");
        rb.AddForce(Vector3.right * horizontal * extraForce, ForceMode.Impulse);
        rb.AddForce(Vector3.up * jump * extraForce, ForceMode.Impulse);
    }
}
