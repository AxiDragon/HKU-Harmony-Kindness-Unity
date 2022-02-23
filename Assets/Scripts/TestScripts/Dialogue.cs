using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    Text dialogueBox;
    int currentDialog = 0;

    public List<string> dialogue = new List<string>();

    void Start()
    {
        dialogueBox = GetComponent<Text>();

        dialogueBox.text = dialogue[currentDialog];
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        ChangeText();
    }

    void ChangeText()
    {
        currentDialog++;

        if (currentDialog == dialogue.Count)
        {
            gameObject.SetActive(false);
            return;
        }
            
        dialogueBox.text = dialogue[currentDialog];
    }
}
