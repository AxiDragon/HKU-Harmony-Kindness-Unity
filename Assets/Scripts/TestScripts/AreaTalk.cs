using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTalk : MonoBehaviour
{
    GameObject dialogue;
    [Tooltip("Executes different events based on its number. Check AreaTalk.cs!")]
    public int communicatorNumber;
    bool inRange = false;
    bool talking = false;

    void Awake()
    {
        dialogue = FindObjectOfType<Dialogue>().gameObject;
        dialogue.SetActive(false);
    }

    void Update()
    {
        if ((!dialogue.activeInHierarchy) && talking)
            SceneManager.LoadScene(2);

        if (!inRange || !Input.GetKeyDown("e"))
            return;

        talking = true;
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
