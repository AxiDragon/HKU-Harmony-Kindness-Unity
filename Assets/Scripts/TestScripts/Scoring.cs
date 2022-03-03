using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    Text scoreText;
    PlatformLooping platformLooping;

    float score = 0;

    void Start()
    {
        scoreText = FindObjectOfType<Text>().GetComponent<Text>();
        scoreText.text = score.ToString();
        platformLooping = FindObjectOfType<PlatformLooping>().GetComponent<PlatformLooping>();
    }

    void Update()
    {
        score += Time.deltaTime * platformLooping.speed * 100;

        scoreText.text = Mathf.RoundToInt(score).ToString();
    }
}
