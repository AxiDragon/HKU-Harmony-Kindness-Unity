using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour
{
    Text scoreText;
    PlatformLooping platformLooping;
    ObstacleInstantiator obstacleInstantiator;

    float score = 0;
    bool cutsceneTriggered = false;

    void Start()
    {
        obstacleInstantiator = FindObjectOfType<ObstacleInstantiator>().GetComponent<ObstacleInstantiator>();
        scoreText = FindObjectOfType<Text>().GetComponent<Text>();
        scoreText.text = score.ToString();
        platformLooping = FindObjectOfType<PlatformLooping>().GetComponent<PlatformLooping>();
    }

    void Update()
    {
        score += Time.deltaTime * platformLooping.speed;

        scoreText.text = Mathf.RoundToInt(score).ToString();

        switch (AreaTalk.gamePhase)
        {
            case 1:
                if ((score > 75) && !cutsceneTriggered)
                {
                    cutsceneTriggered = true;
                    LoadingScreen.sceneNumber = AreaTalk.gamePhase + 2;
                    SceneManager.LoadScene("LoadingScreen");
                }
                break;
            default:
                break;
        }
    }

    IEnumerator StartFlashback()
    {
        yield return new WaitForEndOfFrame();
    }
}
