using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour
{
    CanvasGroup whiteFlash;
    Text scoreText;
    ObstacleInstantiator obstacleInstantiator;

    float score = 0;
    bool cutsceneTriggered = false;

    void Awake()
    {
        whiteFlash = FindObjectOfType<CanvasGroup>().GetComponent<CanvasGroup>();
        obstacleInstantiator = FindObjectOfType<ObstacleInstantiator>().GetComponent<ObstacleInstantiator>();
        scoreText = FindObjectOfType<Text>().GetComponent<Text>();
        scoreText.text = score.ToString();

        //debug
        AreaTalk.gamePhase = 1;
    }

    void Update()
    {
        score += Time.deltaTime * PlatformLooping.speed;

        scoreText.text = Mathf.RoundToInt(score).ToString();

        switch (AreaTalk.gamePhase)
        {
            case 1:
                if ((score > 50) && !cutsceneTriggered)
                {
                    cutsceneTriggered = true;
                    LoadingScreen.sceneNumber = 3;
                    StartCoroutine(StartFlashback());
                }
                break;
            default:
                break;
        }
    }

    IEnumerator StartFlashback()
    {
        while (whiteFlash.alpha < 1)
        {

            whiteFlash.alpha += Time.deltaTime / 2;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("LoadingScreen");
    }
}
