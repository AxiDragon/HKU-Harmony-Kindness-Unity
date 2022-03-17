using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Rigidbody rb;
    GameObject mainCamera;
    Vector3 moveVector;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>().gameObject;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveVector = mainCamera.transform.rotation * new Vector3(horizontal, 0f, vertical) * Time.deltaTime * speed;
        rb.MovePosition(rb.transform.position + moveVector);
    }
}
