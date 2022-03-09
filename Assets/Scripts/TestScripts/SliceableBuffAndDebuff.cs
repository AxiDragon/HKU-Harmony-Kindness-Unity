using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableBuffAndDebuff : MonoBehaviour
{
    BuffAndDebuff buffAndDebuff;
    GameObject sliceableObject;
    Camera mainCamera;

    void Start()
    {
        if (tag == "Basic Buff")
            transform.root.position += Vector3.up * 12f;

        buffAndDebuff = GetComponent<BuffAndDebuff>();
        mainCamera = FindObjectOfType<Camera>().GetComponent<Camera>();
        sliceableObject = transform.parent.gameObject;
    }

    void Update()
    {
        if (!Input.GetKey("mouse 0"))
            return;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, out RaycastHit hit, 1000f))
        {
            print(hit.collider.gameObject);
            if (hit.collider.gameObject == sliceableObject)
                print("yep that's the one");
        }
    }
}
