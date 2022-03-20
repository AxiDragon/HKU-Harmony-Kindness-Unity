using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    Rigidbody rb;
    GameObject mainCamera;
    Vector3 moveVector;
    public float speed, rotationSpeed, animSpeed;
    Animator playerAnim;
    bool talking = false;

    void Start()
    {
        if (GetComponent<Rigidbody>())
            rb = GetComponent<Rigidbody>();
        else
            rb = GetComponentInChildren<Rigidbody>();

        playerAnim = GetComponent<Animator>();
        mainCamera = FindObjectOfType<Camera>().gameObject;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveVector = mainCamera.transform.rotation * new Vector3(horizontal, 0f, vertical) * Time.deltaTime * speed;

        if ((moveVector != Vector3.zero) && !talking)
        {
            playerAnim.SetBool("isRunning", true);
            playerAnim.SetFloat("speed", Vector3.Magnitude(moveVector) * animSpeed);

            rb.MovePosition(rb.transform.position + moveVector);
            Quaternion toRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                          toRotation,
                                                          rotationSpeed * Time.deltaTime);
        }
        else
            playerAnim.SetBool("isRunning", false);

        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }

    public IEnumerator LookAtCommunicator(Vector3 communicatorPosition)
    {
        talking = true;
        Vector3 direction = communicatorPosition - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);

        while (transform.rotation != toRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                                          toRotation,
                                                          rotationSpeed * Time.deltaTime);

            yield return new WaitForFixedUpdate();
        }
    }
}
