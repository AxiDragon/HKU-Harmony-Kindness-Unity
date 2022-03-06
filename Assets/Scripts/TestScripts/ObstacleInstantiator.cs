using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInstantiator : MonoBehaviour
{
    [Tooltip("Put a buff prefab here.")]
    public GameObject buff;
    [Tooltip("Put a debuff prefab here.")]
    public GameObject debuff;
    float platformWidth;
    PlatformLooping platformLooping;

    public bool maxSpawnsActive = false;
    public int maxSpawnTest = 1;
    int spawned = 0;

    [Tooltip("Approximation of how long it should take to spawn in another obstacle.")]
    public float spawnCooldown = 2f;
    float nextSpawnTime = 0;

    [Tooltip("Approximation of how long it should take to nearly guarantee only negative obstacles")]
    public float limitTime = 180f;

    void Start()
    {
        nextSpawnTime = Time.time;

        platformLooping = FindObjectOfType<PlatformLooping>().GetComponent<PlatformLooping>();

        platformWidth = platformLooping.platforms[0].GetComponent<Collider>().bounds.size.x - buff.GetComponentInChildren<Collider>().bounds.size.x * 3;
    }

    void FixedUpdate()
    {
        if (maxSpawnsActive && (spawned < maxSpawnTest))
            return;

        if (Time.time > nextSpawnTime)
        {
            spawned++;

            nextSpawnTime += spawnCooldown * Random.Range(0.1f, 2f) / platformLooping.speed / (3 - (3 / (Time.time + 3)));

            float obstacleRandomizer = Random.Range(0f, 2f);
            GameObject obstacle;

            if (obstacleRandomizer > (2 * Time.time / limitTime))
            {
                obstacle = buff;
            }
            else
            {
                obstacle = debuff;
            }

            Vector3 position = new Vector3(platformLooping.platforms[0].transform.position.x + Random.Range(platformWidth * -0.5f, platformWidth * 0.5f), platformLooping.platforms[0].transform.position.y + 1.5f, platformLooping.platformLength * (platformLooping.platforms.Length - 1.7f));
            
            Instantiate(obstacle, position, new Quaternion(0f, 0f, 0f, 0f));
        }
    }
}
