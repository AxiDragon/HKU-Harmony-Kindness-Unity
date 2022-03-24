using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampHeight : MonoBehaviour
{
    void Start()
    {
        transform.Translate(Vector3.up * FindObjectOfType<RunnerMovement>().jumpForce * 50f);
    }
}
