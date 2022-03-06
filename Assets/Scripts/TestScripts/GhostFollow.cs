using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollow : MonoBehaviour
{
    Transform player;
    Vector3 distance;

    public float followDistance = 1;
    public float lag = 5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;    
    }


    void Update()
    {
        distance = player.position - transform.position;

        if (distance.magnitude < (followDistance / 1.2))
        {
            distance = Quaternion.Euler(0, -90, 0) * distance;
            transform.position -= distance / lag;
        }

        if (distance.magnitude < followDistance)
            return;  

        transform.position += distance / lag;
    }
}
