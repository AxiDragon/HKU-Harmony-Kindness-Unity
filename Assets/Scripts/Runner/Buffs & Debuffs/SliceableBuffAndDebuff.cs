using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableBuffAndDebuff : MonoBehaviour
{
    BuffAndDebuff buffAndDebuff;

    int triggersHit = 0;

    void Start()
    {
        transform.root.position += transform.root.position.x > 0f ? Vector3.left * 5f : Vector3.right * 5f;

        buffAndDebuff = GetComponent<BuffAndDebuff>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggersHit++;
         
            if (triggersHit == 2)
                buffAndDebuff.TriggerSliceable();
        }
    }
}
