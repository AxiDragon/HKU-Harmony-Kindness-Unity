using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeBuff : MonoBehaviour
{
    string charString = "12345QERTFGZXCV";
    KeyCode[] keycodes = {KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4,
        KeyCode.Alpha5, KeyCode.Q, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.F, KeyCode.G,
        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V};
    int randomNumber;
    BuffAndDebuff buffAndDebuff;
    TextMesh characterText;

    void Start()
    {
        if (tag == "Basic Buff")
            transform.root.position += Vector3.up * 12f;

        characterText = transform.root.GetComponentInChildren<TextMesh>();
        buffAndDebuff = GetComponent<BuffAndDebuff>();

        randomNumber = Random.Range(0, charString.Length);
        characterText.text = charString[randomNumber].ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(keycodes[randomNumber]))
            buffAndDebuff.TriggerSliceable();
    }
}
