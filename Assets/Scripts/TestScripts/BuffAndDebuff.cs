using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAndDebuff : MonoBehaviour
{
    Animator playerAnim;
    GameObject player;
    PlatformLooping platformLooping;
    float speedAdjustment;

    bool originalPrefab = false;

    void Start()
    {
        player = GameObject.Find("Player");
        playerAnim = player.GetComponent<Animator>();

        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        if (gameObject.transform.root.gameObject == player)
        {
            originalPrefab = true;

            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        platformLooping = FindObjectOfType<PlatformLooping>().GetComponent<PlatformLooping>();
        if (tag == "Basic Buff")
            speedAdjustment = platformLooping.speed / 10;
        if (tag == "Basic Debuff")
            speedAdjustment = platformLooping.speed / -10;
    }

    void FixedUpdate()
    {
        if (originalPrefab)
            return;

        transform.position += Vector3.back * platformLooping.speed;

        if (player.transform.position.z > transform.position.z + 10f)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (speedAdjustment >= 0)
            {
                if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("HitBuff"))
                    playerAnim.SetTrigger("hitBuff");
            }
            else
            {
                if (!playerAnim.GetCurrentAnimatorStateInfo(0).IsName("HitDebuff"))
                    playerAnim.SetTrigger("hitDebuff");
            }

            platformLooping.speed += speedAdjustment;
            Destroy(gameObject);
        }
    }
}
