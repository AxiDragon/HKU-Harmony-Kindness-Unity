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

    void Awake()
    { 
        currentPlayer = 0;

        instance = this;
        runners = FindObjectsOfType<RunnerMovement>();

        currentPos.Add(0);
        currentPos.Add(2);
        currentPos.Add(1);

        for (int i = 0; i < runners.Length; i++)
        {
            players.Add(runners[i].gameObject);
            print((currentPos[i] + 1) % currentPos.Count);
        }


        //this is really gross but I had to do it I'm sorry

        players.Sort((i, j) => i.name.CompareTo(j.name)); //sorts player list alphabetically. Hooray!

        foreach (GameObject player in players)
        {
            player.GetComponent<RunnerMovement>().enabled = player == players[currentPlayer];
            player.tag = (player == players[currentPlayer]) ? "Player" : "Untagged";

            startTransforms.Add(player.transform.position); //sorts in art, des, dev (alphabetically)
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ChangePlayer();
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
            {
                instance.StartCoroutine(ResetRotation(player));
            }
        }

        instance.StartCoroutine(ChangePosition());
        runners[currentPlayer].Reboot();

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
        for (float time = 0f; time < 0.5f; time += Time.deltaTime)
        {
            for (int i = 0; i < players.Count; i++)
                players[i].transform.position = Vector3.Slerp(startTransforms[currentPos[i]], 
                    startTransforms[currentPos[(currentPos[i] + 1) % currentPos.Count]], time * 2f);

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
