    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLooping : MonoBehaviour
{
    GameObject[] platforms;
    GameObject player;
    float platformLength;

    public float speed;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        platformLength = (platforms[0].GetComponent<Collider>().bounds.size).z;

        int i = 0;

        foreach (GameObject platform in platforms)
        {
            platform.transform.position = player.transform.position + new Vector3(0, -1, platformLength * i);
            i++;
        }
    }

    void Update()
    {
        foreach (GameObject platform in platforms)
        {
            platform.transform.position += Vector3.back * speed;
        }
    }

    public void LoopPlatforms(GameObject loopedPlatform)
    {
        loopedPlatform.transform.position += new Vector3(0, 0, platformLength * (platforms.Length));

    }
}
