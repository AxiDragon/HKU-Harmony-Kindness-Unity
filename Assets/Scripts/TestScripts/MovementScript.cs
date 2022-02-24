using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Rigidbody rb;

    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");



        Vector3 moveVector = Quaternion.Euler(0, -45, 0) * new Vector3(horizontal, 0f, vertical) * Time.deltaTime * speed;

        rb.MovePosition(rb.transform.position + moveVector);
    }
}
