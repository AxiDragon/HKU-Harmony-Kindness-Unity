using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
    [Tooltip("Whether debug commands are available or not")]
    public bool active;

    public void Start()
    {
        if (!active)
            enabled = false;
    }

    void Update()
    {
        string input = Input.inputString;
        switch (input)
        {
            case "r":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "e":
                PlayerSwap.ChangePlayer();
                break;
            case "f":
                FindObjectOfType<ObstacleInstantiator>().limitTime = 1f;
                break;
            case "c":
                FindObjectOfType<Scoring>().score += 250f;
                break;
            case "q":
                PlatformLooping.speed += 0.5f;
                break;
            case "z":
                PlatformLooping.speed -= 0.5f;
                break;
        }

        //if (Input.GetKeyDown(KeyCode.R))

        //if (Input.GetKeyDown(KeyCode.E))
        //    PlayerSwap.ChangePlayer();

        //if (Input.GetKeyDown(KeyCode.F))

        //if (Input.GetKeyDown(KeyCode.C))
    }
}
