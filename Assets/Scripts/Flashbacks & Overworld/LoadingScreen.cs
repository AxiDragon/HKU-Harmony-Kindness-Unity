using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    Image progressBar;
    public static int sceneNumber;

    IEnumerator Start()
    {
        AsyncOperation loadProgress = SceneManager.LoadSceneAsync(sceneNumber);
        
        while (loadProgress.progress < 1)
        {
            progressBar.fillAmount = loadProgress.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
