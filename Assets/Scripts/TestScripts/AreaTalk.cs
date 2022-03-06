using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTalk : MonoBehaviour
{
    GameObject dialogue;

    bool inRange;

    void Awake()
    {
        dialogue = FindObjectOfType<Dialogue>().gameObject;
        dialogue.SetActive(false);
    }

    void Update()
    {
        if (!inRange || !Input.GetKey("Use"))
            return;

        dialogue.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            inRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            inRange = false;
    }
}
