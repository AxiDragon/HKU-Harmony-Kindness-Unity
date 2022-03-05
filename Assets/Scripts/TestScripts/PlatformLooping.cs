    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLooping : MonoBehaviour
{
    [System.NonSerialized]
    public GameObject[] platforms;

    GameObject player;
    [System.NonSerialized]
    public float platformLength;

    public float speed;
    float baseSpeed;

    Camera mainCamera;

    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>();
        baseSpeed = speed;

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

    void FixedUpdate()
    {
        foreach (GameObject platform in platforms)
        {
            platform.transform.position += Vector3.back * speed;
        }

        mainCamera.fieldOfView = Mathf.Clamp(70f * (speed / baseSpeed), 30f, 90f);
    }

    public IEnumerator ChangeCameraFieldOfView()
    {

        yield return new WaitForFixedUpdate();
    }

    public void LoopPlatforms(GameObject loopedPlatform)
    {
        loopedPlatform.transform.position += new Vector3(0, 0, platformLength * platforms.Length);
    }
}
