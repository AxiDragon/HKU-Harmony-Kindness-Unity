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

    [System.NonSerialized]
    public float score = 0;
    bool cutsceneTriggered = false;

    void Awake()
    {
        whiteFlash = FindObjectOfType<CanvasGroup>();
        obstacleInstantiator = FindObjectOfType<ObstacleInstantiator>();
        scoreText = FindObjectOfType<Text>();
        scoreText.text = score.ToString();

        //debug
        //AreaTalk.gamePhase = 1;
    }

    void Update()
    {
        score += Time.deltaTime * PlatformLooping.speed;

        scoreText.text = Mathf.RoundToInt(score).ToString();

        switch (AreaTalk.gamePhase)
        {
            case 2:
                if ((score > 50) && !cutsceneTriggered)
                {
                    cutsceneTriggered = true;
                    LoadingScreen.sceneNumber = 3;
                    StartCoroutine(StartFlashback());
                }
                break;
        }
    }

    public IEnumerator StartFlashback()
    {
        cutsceneTriggered = true;
        while (whiteFlash.alpha < 1)
        {

            whiteFlash.alpha += Time.deltaTime / 2;
            yield return new WaitForEndOfFrame();
        }

        switch (AreaTalk.gamePhase)
        {
            case 0:
                LoadingScreen.sceneNumber = 3;
                break;
            case 1:
                LoadingScreen.sceneNumber = 4;
                break;
            case 2:
                break;
        }
        SceneManager.LoadScene("LoadingScreen");
    }
}
