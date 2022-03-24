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

    static Animator[] playerAnims;

    bool gameOvered = false;

    public static bool HasSpeed(Animator anim)
    {
        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            if (parameter.name == "speed")
                return true;
        }
        return false;
    }

    void Start()
    {
        baseSpeed = startSpeed;
        speed = startSpeed / (3f / (AreaTalk.gamePhase + 3f));

        FindObjectOfType<Camera>().fieldOfView = 60f * speed / baseSpeed;

        playerAnims = FindObjectsOfType<Animator>();
        player = GameObject.Find("Players");
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

        speed = Mathf.Min(speed, maxSpeed);

        if ((speed < lowestSpeed) && !gameOvered)
        {
            gameOvered = true;
            GameOver();
        }
    }

    public static void UpdatePlayerSpeed()
    {
        foreach (Animator anim in playerAnims)
        {
            if (HasSpeed(anim))
                anim.SetFloat("speed", speed);
        }
    }

    public void LoopPlatforms(GameObject loopedPlatform) => loopedPlatform.transform.position += new Vector3(0, 0, platformLength * platforms.Length);

    void GameOver() => StartCoroutine(FindObjectOfType<Scoring>().StartFlashback());
}
