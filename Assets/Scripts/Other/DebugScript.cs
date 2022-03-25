using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
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
                FindObjectOfType<Scoring>().score += 99999f;
                break;
            case "q":
                PlatformLooping.speed += 0.5f;
                break;
            case "z":
                PlatformLooping.speed -= 0.5f;
                break;
            case "t":
                LoadingScreen.sceneNumber = 3 + AreaTalk.gamePhase;
                SceneManager.LoadScene("LoadingScreen");
                break;
        }
    }
}
