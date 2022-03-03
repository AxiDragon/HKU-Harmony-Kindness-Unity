using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    PlatformLooping platformLooping;

    void Start()
    {
        platformLooping = FindObjectOfType<PlatformLooping>().GetComponent<PlatformLooping>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            platformLooping.LoopPlatforms(transform.root.gameObject);
    }
}
