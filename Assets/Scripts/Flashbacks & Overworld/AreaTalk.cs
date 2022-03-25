using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AreaTalk : MonoBehaviour
{
    GameObject dialogueGameObject;
    [Tooltip("Executes different events based on its number. Check AreaTalk.cs!")]
    public static int gamePhase = 0;
    bool inRange, talking, endedDialogue = false;
    [Tooltip("Will automatically start talking to the player if nearby")]
    public bool autoTrigger;

    WhiteFlash flash;
    MovementScript movement;
    LookAtPlayer lookAtPlayer;
    Animator playerAnim;

    Text dialogueBox;
    [Tooltip("At which points in the dialogue animations should be triggered")]
    public int[] animationTriggers;
    [Tooltip("At which points in the dialogue special events should be triggered")]
    public int[] specialTriggers;
    int currentDialog, currentAnimationEvent = 0;

    public List<string> dialogue = new List<string>();

    void Awake()
    {
        dialogueBox = FindObjectOfType<Text>();

        dialogueBox.text = dialogue[currentDialog];

        dialogueGameObject = dialogueBox.gameObject;
        dialogueGameObject.SetActive(false);
        flash = FindObjectOfType<WhiteFlash>();
        movement = FindObjectOfType<MovementScript>();
        lookAtPlayer = FindObjectOfType<LookAtPlayer>();

        if (GetComponentInChildren<Animator>())
            playerAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!dialogueGameObject.activeInHierarchy && talking && !endedDialogue)
        {
            endedDialogue = true;
            StartCoroutine(flash.Flash(false));
        }

        if (inRange && (Input.GetKeyDown(KeyCode.E) || autoTrigger))
        {
            talking = true;
            dialogueGameObject.SetActive(true);
            StartCoroutine(movement.LookAtCommunicator(transform.position));
            StartCoroutine(LookAtCommunicator(movement.gameObject.transform.position));
            StartCoroutine(lookAtPlayer.LookAtConversation(movement.gameObject.transform.position, transform.position));
        }

        if (!dialogueGameObject.activeInHierarchy)
            return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            currentDialog++;
            ChangeText();

            foreach (int integer in animationTriggers)
            {
                if (integer == currentDialog)
                {
                    currentAnimationEvent++;
                    AnimationEvent();
                }
            }

            foreach (int integer in specialTriggers)
            {
                if (integer == currentDialog)
                {
                    currentAnimationEvent++;
                    SpecialEvent();
                }
            }
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Q))
        {
            currentDialog = Mathf.Max(--currentDialog, 0);
            ChangeText();

            foreach (int integer in animationTriggers)
            {
                if (integer - 1 == currentDialog)
                {
                    currentAnimationEvent--;
                    AnimationEvent();
                }
            }
        }

    }

    void SpecialEvent()
    {
        switch (tag)
        {
            case "Bully":
                FindObjectOfType<Animation>().Play();
                break;
            case "Confidential Counsellor":
                FindObjectOfType<Animation>().Play();
                break;
        }
    }

    void AnimationEvent()
    {
        switch (currentAnimationEvent % 2)
        {
            case 0:
                playerAnim.SetBool("animationTriggered", false);
                break;
            case 1:
                playerAnim.SetBool("animationTriggered", true);
                break;
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

    void ChangeText()
    {
        if (currentDialog == dialogue.Count)
        {
            dialogueGameObject.SetActive(false);
            return;
        }

        dialogueBox.text = dialogue[currentDialog];
    }
    IEnumerator LookAtCommunicator(Vector3 communicatorPosition)
    {
        Vector3 direction = communicatorPosition - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);

        while (transform.rotation != toRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 15f * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);

            yield return new WaitForFixedUpdate();
        }
    }

}
