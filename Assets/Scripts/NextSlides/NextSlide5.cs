using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSlide5 : MonoBehaviour
{
    public void PlayGame()
    {
        LoadingScreen.sceneNumber = 2;
        SceneManager.LoadScene("RunTestScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}