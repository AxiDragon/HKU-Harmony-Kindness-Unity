using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInstantiator : MonoBehaviour
{
    [Tooltip("Put buff prefabs here.")]
    public GameObject[] buffs;
    [Tooltip("Put debuff prefabs here.")]
    public GameObject[] debuffs;

    Animator[] animators;

    float platformWidth;

    GameObject cameraParent;
    Camera playerCamera;

    public bool maxSpawnsActive = false;
    public int maxSpawnTest = 1;
    int spawned = 0;
    Vector3 originalPos;

    [Tooltip("Approximation of how long it should take to spawn in another obstacle.")]
    public float spawnCooldown = 2f;
    float nextSpawnTime, startingTime;
    float difficulty = 4f;

    [Tooltip("Approximation of how long it should take to nearly guarantee only negative obstacles")]
    public float limitTime = 180f;

    void Start()
    {
        animators = FindObjectsOfType<Animator>();
        limitTime /= 6f / (AreaTalk.gamePhase + 6f);
        limitTime += Time.time;
        nextSpawnTime = startingTime = Time.time;

        playerCamera = FindObjectOfType<Camera>();
        cameraParent = playerCamera.transform.parent.gameObject;

        originalPos = cameraParent.transform.position;

        platformWidth = PlatformLooping.platforms[0].GetComponent<Collider>().bounds.size.x * 0.8f;
    }

    void FixedUpdate()
    {
        if (maxSpawnsActive && (spawned < maxSpawnTest))
            return;

        if (Time.time > nextSpawnTime)
        {
            spawned++;

            nextSpawnTime += (Mathf.Sin(Time.time / 15f) + 2.5f) / 2f * ((spawnCooldown * Random.Range(0.1f, 2f) / Mathf.Max(Mathf.Sqrt((Time.time - startingTime) / 4f),
                1f) / PlatformLooping.speed) - ((Time.time - startingTime) / limitTime)) * (difficulty / (AreaTalk.gamePhase + difficulty));

            float obstacleRandomizer = Random.Range(0f, 2f);
            GameObject obstacle;

            if (obstacleRandomizer > (2 * (Time.time - startingTime) / limitTime))
                obstacle = buffs[Random.Range(0, buffs.Length)];
            else
                obstacle = debuffs[Random.Range(0, debuffs.Length)];

            Vector3 position = new Vector3(PlatformLooping.platforms[0].transform.position.x + Random.Range(platformWidth * -0.5f, platformWidth * 0.5f),
                PlatformLooping.platforms[0].transform.position.y + 1.5f,
                PlatformLooping.platformLength * (PlatformLooping.platforms.Length - 1.7f));

            Instantiate(obstacle, position, new Quaternion(0f, 0f, 0f, 0f));
        }
    }


    public void StartFOVAdjust(float currentSpeed, float speedAdjust)
    {
        float currentFOV = 60f * currentSpeed / PlatformLooping.baseSpeed;
        float futureFOV = Mathf.Clamp(60f * (currentSpeed + speedAdjust) / PlatformLooping.baseSpeed, 30f, 100f);

        StartCoroutine(CameraFOVAdjust(currentFOV, futureFOV));
    }

    IEnumerator CameraShake(float magnitude)
    {
        float duration = PlatformLooping.speed / 5f;

        while (duration > 0f)
        {
            cameraParent.transform.localPosition = originalPos + new Vector3(Random.Range(-magnitude, magnitude),
                Random.Range(-magnitude, magnitude),
                Random.Range(-magnitude, magnitude));
            cameraParent.transform.localRotation = Quaternion.Euler(Random.Range(-magnitude * 90f, magnitude * 90f),
                Random.Range(-magnitude * 90f, magnitude * 90f),
                Random.Range(-magnitude * 90f, magnitude * 90f));
            duration -= Time.deltaTime;
            magnitude = duration / 20f;
            yield return null;
        }
        cameraParent.transform.localPosition = originalPos;
        cameraParent.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    IEnumerator CameraFOVAdjust(float startFOV, float endFOV)
    {
        for (float t = 0; playerCamera.fieldOfView != endFOV; t += Time.deltaTime)
        {
            playerCamera.fieldOfView = Mathf.SmoothStep(startFOV, endFOV, t);
            yield return null;
        }
    }

    IEnumerator BuffBoostMove()
    {
        Vector3 movePos = originalPos + Vector3.back * 10f;
        float currentAnimSpeed = animators[0].GetFloat("speed");
        float boostAnimSpeed = currentAnimSpeed * 4f;

        for (float t = 0; Vector3.Magnitude(movePos - cameraParent.transform.localPosition) > 0.01f; t += Time.deltaTime)
        {
            foreach (Animator anim in animators)
                if (PlatformLooping.HasSpeed(anim))
                    anim.SetFloat("speed", Mathf.SmoothStep(currentAnimSpeed, boostAnimSpeed, t));

            cameraParent.transform.localPosition = Vector3.Slerp(originalPos, movePos, t);
            yield return null;
        }

        for (float t = 0; Vector3.Magnitude(originalPos - cameraParent.transform.localPosition) > 0.01f; t += Time.deltaTime)
        {
            foreach (Animator anim in animators)
                if (PlatformLooping.HasSpeed(anim))
                    anim.SetFloat("speed", Mathf.SmoothStep(boostAnimSpeed, currentAnimSpeed, t));

            cameraParent.transform.localPosition = Vector3.Slerp(movePos, originalPos, t);
            yield return null;
        }

        foreach (Animator anim in animators)
            if (PlatformLooping.HasSpeed(anim))
                anim.SetFloat("speed", PlatformLooping.speed);
    }
    public void StartCameraShake(float speedAdjust) => StartCoroutine(CameraShake(speedAdjust));

    public void StartBuffBoostMove() => StartCoroutine(BuffBoostMove());

}