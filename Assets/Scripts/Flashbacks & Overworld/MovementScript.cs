using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Rigidbody rb;
    GameObject mainCamera;
    Vector3 moveVector;
    public float speed;
    public float rotationSpeed;

    void Start()
    {
        if (GetComponent<Rigidbody>())
            rb = GetComponent<Rigidbody>();
        else
            rb = GetComponentInChildren<Rigidbody>();

        mainCamera = FindObjectOfType<Camera>().gameObject;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveVector = mainCamera.transform.rotation * new Vector3(horizontal, 0f, vertical) * Time.deltaTime * speed;
        rb.MovePosition(rb.transform.position + moveVector);

        if (moveVector != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }
}
