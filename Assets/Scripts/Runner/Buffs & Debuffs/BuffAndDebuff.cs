using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAndDebuff : MonoBehaviour
{
    Animator playerAnim, objectAnim;
    GameObject player;
    ObstacleInstantiator obstacle;
    float speedAdjustment;

    bool isMoving()
    {
        foreach (AnimatorControllerParameter parameter in playerAnim.parameters)
        {
            if (parameter.name == "speed")
                return true;
        }
        return false;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponent<Animator>();

        if (isMoving())
            playerAnim.SetFloat("speed", PlatformLooping.speed);

        objectAnim = transform.parent.GetComponent<Animator>();

        obstacle = FindObjectOfType<ObstacleInstantiator>();

        switch (tag)
        {
            case "Basic Buff":
                speedAdjustment = PlatformLooping.speed / 10;
                break;
            case "Basic Debuff":
                speedAdjustment = PlatformLooping.speed / -5;
                break;
        }
    }

    public void SwapPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponent<Animator>();

        if (isMoving())
            playerAnim.SetFloat("speed", PlatformLooping.speed);
    }

    void FixedUpdate()
    {
        transform.root.position += Vector3.back * PlatformLooping.speed;

        if (player.transform.position.z > transform.root.position.z + 10f)
            if (gameObject.name.Contains("Slice") && speedAdjustment < 0)
                TriggerSliceable();
            else
                Destroy(transform.root.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            PickedUpByPlayer();
    }

    IEnumerator DestroyAnimation()
    {
        if (objectAnim != null)
            objectAnim.SetTrigger("pickedUp");

        if (objectAnim != null)
        {
            while (!objectAnim.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
            yield return null;
        }

        Destroy(transform.root.gameObject);
    }

    public void TriggerSliceable()
    {
        PickedUpByPlayer();
    }

    void PickedUpByPlayer()
    {
        if (speedAdjustment >= 0)
        {
            if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("HitBuff"))
                playerAnim.SetTrigger("hitBuff");

            obstacle.StartBuffBoostMove();
        }
        else
        {
            if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("HitDebuff"))
                playerAnim.SetTrigger("hitDebuff");

            obstacle.StartCameraShake();
        }

        if (transform.root.Find("Character"))
            transform.root.Find("Character").GetComponent<Animator>().SetTrigger("destroyText");

        obstacle.StartFOVAdjust(PlatformLooping.speed, speedAdjustment);
        PlatformLooping.speed += speedAdjustment;
        PlatformLooping.UpdatePlayerSpeed();
        StartCoroutine(DestroyAnimation());
    }
}
