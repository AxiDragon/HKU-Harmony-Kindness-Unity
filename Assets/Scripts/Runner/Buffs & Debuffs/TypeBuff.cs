using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeBuff : MonoBehaviour
{
    string charString = "12345qertfgzxcv";
    char objectCharacter;
    KeyCode objectKey;
    BuffAndDebuff buffAndDebuff;

    void Start()
    {
        buffAndDebuff = GetComponent<BuffAndDebuff>();

        objectCharacter = charString[Random.Range(0, charString.Length)];
        objectKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), objectCharacter.ToString());
        print(objectKey);
    }

    void Update()
    {
        if (Input.GetKeyDown(objectKey))
            buffAndDebuff.TriggerSliceable();
    }
}
