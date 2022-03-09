using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformLooping : MonoBehaviour
{
    [System.NonSerialized]
    public static GameObject[] platforms;

    GameObject player;
    [System.NonSerialized]
    public static float platformLength, baseSpeed, speed;

    public float startSpeed;
    [Tooltip("Lowest speed the player may reach before being presented with a game over.")]
    public float lowestSpeed;
    [Tooltip("Highest speed the player can reach. (Mainly to secure that the player will not phase through collisions)")]
    public float maxSpeed;

    void Awake()
    {
        speed = startSpeed;
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
            platform.transform.position += Vector3.back * speed;

        if (speed > maxSpeed)
            speed = maxSpeed;

        if (speed < lowestSpeed)
            GameOver();
    }

    public void LoopPlatforms(GameObject loopedPlatform)
    {
        loopedPlatform.transform.position += new Vector3(0, 0, platformLength * platforms.Length);
    }

    void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
