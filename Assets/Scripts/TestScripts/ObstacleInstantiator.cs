using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInstantiator : MonoBehaviour
{
    GameObject buff;
    GameObject debuff;
    float platformWidth;
    PlatformLooping platformLooping;

    GameObject rampTest;

    [Tooltip("Approximation of how long it should take to spawn in another obstacle.")]
    public float spawnCooldown = 2f;
    float nextSpawnTime = 0;

    [Tooltip("Approximation of how long it should take to nearly guarantee only negative obstacles")]
    public float limitTime = 180f;

    void Start()
    {
        platformLooping = FindObjectOfType<PlatformLooping>().GetComponent<PlatformLooping>();
        buff = GameObject.FindGameObjectWithTag("Basic Buff");
        debuff = GameObject.FindGameObjectWithTag("Basic Debuff");

        platformWidth = platformLooping.platforms[0].GetComponent<Collider>().bounds.size.x - buff.GetComponentInChildren<Collider>().bounds.size.x;
    }

    void FixedUpdate()
    {
        if (Time.time > nextSpawnTime)
        {
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

            Vector3 position = new Vector3(Random.Range(platformWidth * -0.5f, platformWidth * 0.5f), platformLooping.platforms[0].transform.position.y + 2f, platformLooping.platformLength * (platformLooping.platforms.Length - 1.7f));
            
            Instantiate(obstacle, position, new Quaternion(0f, 0f, 0f, 0f));
        }
    }
}
