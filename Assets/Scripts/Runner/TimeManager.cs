using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = .1f;
    public float slowDownLength = 2f;
    public float cameraShiftIntensity = 10f;
    Camera mainCamera;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        Time.fixedDeltaTime += (.02f / slowDownLength) * Time.unscaledDeltaTime;
        Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0f, .02f);
    }

    public void SlowDown()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        StartCoroutine(SlowDownShift());
    }

    IEnumerator SlowDownShift()
    {
        float currentFOV = mainCamera.fieldOfView;
        float shiftFOV = currentFOV - cameraShiftIntensity;

        for (float t = 0; mainCamera.fieldOfView != shiftFOV; t += Time.unscaledDeltaTime)
        {   
            mainCamera.fieldOfView = Mathf.SmoothStep(currentFOV, shiftFOV, t);
            yield return null;
        }

        for (float t = 0; mainCamera.fieldOfView != currentFOV; t += Time.unscaledDeltaTime)
        {
            mainCamera.fieldOfView = Mathf.SmoothStep(shiftFOV, currentFOV, t);
            yield return null;
        }
    }
}
