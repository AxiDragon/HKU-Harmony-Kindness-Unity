using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaTalk : MonoBehaviour
{
    GameObject dialogue;
    [Tooltip("Executes different events based on its number. Check AreaTalk.cs!")]
    public static int gamePhase;
    bool inRange, talking, endedDialogue = false;
    [Tooltip("Will automatically start talking to the player if nearby")]
    public bool autoTrigger;

    WhiteFlash flash;
    MovementScript movement;

    void Awake()
    {
        dialogue = FindObjectOfType<Dialogue>().gameObject;
        dialogue.SetActive(false);
        flash = FindObjectOfType<WhiteFlash>();
        movement = FindObjectOfType<MovementScript>();
    }

    void Update()
    {
        if (!dialogue.activeInHierarchy && talking && !endedDialogue)
        {
            endedDialogue = true;
            StartCoroutine(flash.Flash(false));
        }

        if (inRange && (Input.GetKeyDown("e") || autoTrigger))
        {
            talking = true;
            dialogue.SetActive(true);
            StartCoroutine(movement.LookAtCommunicator(transform.position));
        }
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
