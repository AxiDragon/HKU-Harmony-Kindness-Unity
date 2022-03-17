using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
    [Tooltip("Whether debug commands are available or not")]
    public bool active;

    public void Start()
    {
        if (!active)
            enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (Input.GetKeyDown(KeyCode.E))
            PlayerSwap.ChangePlayer();
    }
}
