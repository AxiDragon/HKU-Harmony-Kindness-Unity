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

    GameObject cameraParent;
    Camera playerCamera;

    public bool maxSpawnsActive = false;
    public int maxSpawnTest = 1;
    int spawned = 0;
    Vector3 originalPos;

    [Tooltip("Approximation of how long it should take to spawn in another obstacle.")]
    public float spawnCooldown = 2f;
    float nextSpawnTime = 0;

    [Tooltip("Approximation of how long it should take to nearly guarantee only negative obstacles")]
    public float limitTime = 180f;

    void Awake()
    {
        nextSpawnTime = Time.time;
        
        cameraParent = FindObjectOfType<Camera>().transform.parent.gameObject;
        playerCamera = FindObjectOfType<Camera>().GetComponent<Camera>();

        originalPos = cameraParent.transform.position;
        platformLooping = FindObjectOfType<PlatformLooping>().GetComponent<PlatformLooping>();

        platformWidth = platformLooping.platforms[0].GetComponent<Collider>().bounds.size.x * 0.8f;
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

    public void StartCameraShake()
    {
        StartCoroutine(CameraShake());
    }

    public void StartBuffBoostMove()
    {
        StartCoroutine(BuffBoostMove());
    }

    public void StartFOVAdjust(float currentSpeed, float speedAdjust)
    {
        float currentFOV = 60f * currentSpeed / platformLooping.baseSpeed;
        float futureFOV = Mathf.Clamp(60f * (currentSpeed + speedAdjust) / platformLooping.baseSpeed, 30f, 100f);

        StartCoroutine(CameraFOVAdjust(currentFOV, futureFOV));
    }

    IEnumerator CameraShake()
    {
        float duration = platformLooping.speed / 5;
        float magnitude = duration / 20;

        while (duration > 0f)
        {
            cameraParent.transform.localPosition = originalPos + new Vector3(Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude), Random.Range(-magnitude, magnitude));
            cameraParent.transform.localRotation = Quaternion.Euler(Random.Range(-magnitude * 90, magnitude * 90), Random.Range(-magnitude * 90, magnitude * 90), Random.Range(-magnitude * 90, magnitude * 90));
            duration -= Time.deltaTime;
            magnitude = duration / 20;
            yield return null;
        }
        cameraParent.transform.localPosition = originalPos;
        cameraParent.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    IEnumerator CameraFOVAdjust(float startFOV, float endFOV)
    {
        for (float t = 0;  playerCamera.fieldOfView != endFOV; t += Time.deltaTime)
        {
            playerCamera.fieldOfView = Mathf.SmoothStep(startFOV, endFOV, t);
            yield return null;
        }
    }

    IEnumerator BuffBoostMove()
    {
        Vector3 movePos = originalPos + Vector3.back * 10f;

        for (float t = 0; cameraParent.transform.localPosition != movePos; t += Time.deltaTime)
        {
            cameraParent.transform.localPosition = Vector3.Slerp(originalPos, movePos, t);
            yield return null;
        }

        for (float t = 0; cameraParent.transform.localPosition != originalPos; t += Time.deltaTime)
        {
            cameraParent.transform.localPosition = Vector3.Slerp(movePos, originalPos, t);
            yield return null;
        }
    }
    
}
