using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSlide : MonoBehaviour
{
    public void PlayGame()
    {
        LoadingScreen.sceneNumber = 12;
        SceneManager.LoadScene("HowToPlay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
