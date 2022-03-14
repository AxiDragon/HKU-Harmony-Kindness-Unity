using UnityEngine;

public class TurnOnCursor : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }