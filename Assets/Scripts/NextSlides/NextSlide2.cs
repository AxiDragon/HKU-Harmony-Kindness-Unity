using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSlide2 : MonoBehaviour
{
    public void PlayGame()
    {
        LoadingScreen.sceneNumber = 14;
        SceneManager.LoadScene("TurnExplenation");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

