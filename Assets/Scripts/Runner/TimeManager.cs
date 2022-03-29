using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownFactor = .1f;
    public float slowDownLength = 2f;
    public float cameraShiftIntensity = 10f;
    RectTransform rotationReminder;
    Camera mainCamera;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        rotationReminder = GameObject.Find("RotationReminder").GetComponent<RectTransform>();
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
            rotationReminder.transform.localPosition = new Vector2(0f, Mathf.SmoothStep(540f, 240f, t / (slowDownLength / 2f)));
            mainCamera.fieldOfView = Mathf.SmoothStep(currentFOV, shiftFOV, t / (slowDownLength / 2f));
            yield return null;
        }

        for (float t = 0; mainCamera.fieldOfView != currentFOV; t += Time.unscaledDeltaTime)
        {
            rotationReminder.transform.localPosition = new Vector2(0f, Mathf.SmoothStep(240f, 540f, t / (slowDownLength / 2f)));
            mainCamera.fieldOfView = Mathf.SmoothStep(shiftFOV, currentFOV, t / (slowDownLength / 2f));
            yield return null;
        }
    }
}
