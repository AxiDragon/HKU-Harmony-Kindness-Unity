using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAndDebuff : MonoBehaviour
{
    Animator playerAnim;
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
        player = GameObject.Find("Player");

        playerAnim = player.GetComponent<Animator>();

        obstacle = FindObjectOfType<ObstacleInstantiator>().GetComponent<ObstacleInstantiator>();

        if (isMoving())
            playerAnim.SetFloat("speed", PlatformLooping.speed);

        switch (tag)
        {
            case "Basic Buff":
                speedAdjustment = PlatformLooping.speed / 10;
                break;
            case "Basic Debuff":
                speedAdjustment = PlatformLooping.speed / -5;
                break;
            default:
                break;
        }
    }

    void FixedUpdate()
    {
        transform.root.position += Vector3.back * PlatformLooping.speed;

        if (player.transform.position.z > transform.root.position.z + 10f)
            Destroy(transform.parent.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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

            obstacle.StartFOVAdjust(PlatformLooping.speed, speedAdjustment);
            PlatformLooping.speed += speedAdjustment;
            Destroy(transform.root.gameObject);
        }
    }
}
