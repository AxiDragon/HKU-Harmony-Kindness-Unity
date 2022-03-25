using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour
{
    CanvasGroup whiteFlash;
    Text scoreText;
    float difficulty = 350f;

    [System.NonSerialized]
    public float score = 0;

    void Awake()
    {
        whiteFlash = FindObjectOfType<CanvasGroup>();
        scoreText = FindObjectOfType<Text>();
        scoreText.text = score.ToString();

        //debug
        //AreaTalk.gamePhase = 1;
    }

    void Update()
    {
        score += Time.deltaTime * PlatformLooping.speed;

        scoreText.text = Mathf.RoundToInt(score).ToString();
    }

    public IEnumerator StartFlashback()
    {
        while (whiteFlash.alpha < 1)
        {
            whiteFlash.alpha += Time.deltaTime / 2;
            yield return new WaitForEndOfFrame();
        }

        if (score > (AreaTalk.gamePhase + 1f) * difficulty)
            LoadingScreen.sceneNumber = 3 + AreaTalk.gamePhase;
        else
            LoadingScreen.sceneNumber = SceneManager.sceneCountInBuildSettings - 1;

        SceneManager.LoadScene("LoadingScreen");
    }
}
