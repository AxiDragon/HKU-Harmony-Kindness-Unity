using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableBuffAndDebuff : MonoBehaviour
{
    BuffAndDebuff buffAndDebuff;

    int requiredSliceScore = 20;
    int sliceScore = 0;
    bool slicing = false;


    void Start()
    {
        transform.root.position += transform.root.position.x > 0f ? Vector3.left * 5f : Vector3.right * 5f;

        buffAndDebuff = GetComponent<BuffAndDebuff>();
    }

    void FixedUpdate()
    {
        if (slicing)
            sliceScore++;

        if (sliceScore > requiredSliceScore / PlatformLooping.speed)
            buffAndDebuff.TriggerSliceable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            slicing = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            slicing = false;
    }
}
