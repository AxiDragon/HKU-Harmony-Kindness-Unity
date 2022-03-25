using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    GameObject player;
    bool dialogue = false;

    void Start() => player = GameObject.FindWithTag("Player");

    void Update()
    {
        if (!dialogue)
            transform.LookAt(player.transform);
    }

    public IEnumerator LookAtConversation(Vector3 communicator1, Vector3 communicator2)
    {
        dialogue = true;
        float startTime = 0f;
        Vector3 meetingPoint = ((communicator1 + communicator2) / 2f) + Vector3.up * 1f;
        Vector3 currentPos = transform.position;

        while (Vector3.Magnitude(meetingPoint - transform.position) > Vector3.Magnitude(communicator1 - communicator2) * 2f)
        {
            transform.position = Vector3.Lerp(currentPos, meetingPoint, startTime);
            startTime += Time.deltaTime;
            yield return null;
        }
    }
}
