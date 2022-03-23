using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnOffScreen : MonoBehaviour
{
    void OnBecameInvisible() => Destroy(transform.root.gameObject);
}
