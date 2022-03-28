using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSlide6 : MonoBehaviour
{
    public void PlayGame()
    {
        LoadingScreen.sceneNumber = 15;
        SceneManager.LoadScene("PlayGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
