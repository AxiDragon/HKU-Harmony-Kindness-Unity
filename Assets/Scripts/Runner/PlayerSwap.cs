using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwap : MonoBehaviour
{
    public static PlayerSwap instance;
    public static List<GameObject> players = new List<GameObject>();
    static List<Vector3> startTransforms = new List<Vector3>();
    static List<int> currentPos = new List<int>();
    static RunnerMovement[] runners;
    public static int currentPlayer = 0;
    float currentTime, randomSwitchTime, scaledMinimumTime;

    [Tooltip("Minimum time a player should be able to play the game.")]
    public float minimumTime;

    void Awake()
    {
        currentTime = 0f;
        randomSwitchTime = UnityEngine.Random.Range(0f, 1f);
        scaledMinimumTime = Time.time + minimumTime;

        currentPlayer = 0;

        instance = this;
        runners = FindObjectsOfType<RunnerMovement>();

        currentPos.Add(0);
        currentPos.Add(2);
        currentPos.Add(1);
        //this is really gross but I had to do it I'm sorry

        for (int i = 0; i < runners.Length; i++)
            players.Add(runners[i].gameObject);

        players.Sort((i, j) => i.name.CompareTo(j.name)); //sorts player list alphabetically. Hooray!

        foreach (GameObject player in players)
        {
            player.GetComponent<RunnerMovement>().enabled = player == players[currentPlayer];
            player.tag = (player == players[currentPlayer]) ? "Player" : "Untagged";
            startTransforms.Add(player.transform.localPosition); //sorts in art, des, dev (alphabetically)
        }


        if (AreaTalk.gamePhase != 1)
            foreach (GameObject donkeyHead in GameObject.FindGameObjectsWithTag("Donkey Head"))
                donkeyHead.SetActive(false);
    }

    void FixedUpdate() => RandomTimer();

    void RandomTimer()
    {
        currentTime += Time.deltaTime;

        if ((randomSwitchTime + Mathf.Max(scaledMinimumTime - Time.time, 0f)) < (1f - (1f / (1f + currentTime / 10f))))
        {
            ChangePlayer();
            currentTime = 0f;
            scaledMinimumTime = Time.time + minimumTime;
            randomSwitchTime = UnityEngine.Random.Range(0f, 1f);
        }
    }

    public static void ChangePlayer()
    {
        startTransforms[0] = players[currentPlayer].transform.position;
        currentPlayer++;
        currentPlayer %= players.Count;

        foreach (GameObject player in players)
        {
            player.GetComponent<RunnerMovement>().enabled = player == players[currentPlayer];
            player.tag = (player == players[currentPlayer]) ? "Player" : "Untagged";
            if (player.tag == "Untagged")
                instance.StartCoroutine(ResetRotation(player));

            player.GetComponent<Animator>().SetBool("isGrounded", true);
        }

        instance.StartCoroutine(ChangePosition());

        BuffAndDebuff[] buffAndDebuff = FindObjectsOfType<BuffAndDebuff>();

        foreach (BuffAndDebuff buff in buffAndDebuff)
            buff.SwapPlayer();
    }

    static IEnumerator ResetRotation(GameObject gameObject)
    {
        Quaternion rotation = Quaternion.Euler(gameObject.transform.eulerAngles);

        for (float time = 0f; time < 0.5f; time += Time.deltaTime)
        {
            gameObject.transform.rotation = Quaternion.Slerp(rotation, Quaternion.Euler(Vector3.zero), time * 2f);
            yield return new WaitForFixedUpdate();
        }
    }

    static IEnumerator ChangePosition()
    {
        float beginSpeed = PlatformLooping.speed;
        float endSpeed = Mathf.Max(PlatformLooping.speed / 1.5f, 0.85f);
        //changePos works, there's just wrong positions for some reason
        for (float time = 0f; time < 0.5f; time += Time.deltaTime)
        {
            PlatformLooping.speed = Mathf.SmoothStep(beginSpeed, endSpeed, time * 2f);

            for (int i = 0; i < players.Count; i++)
                players[i].transform.position = Vector3.Lerp(startTransforms[currentPos[i]],
                    startTransforms[(currentPos[i] + 1) % currentPos.Count], time * 2f);

            yield return new WaitForFixedUpdate();
        }

        for (int i = 0; i < currentPos.Count; i++)
            currentPos[i] = (currentPos[i] + 1) % currentPos.Count;
    }

    private void OnDestroy()
    {
        Array.Clear(runners, 0, runners.Length);
        players.Clear();
    }
}
