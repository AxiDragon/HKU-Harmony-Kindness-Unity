using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampHeight : MonoBehaviour
{
    void Start() => transform.Translate(50f
                                        * PlayerSwap.players[PlayerSwap.currentPlayer].GetComponent<RunnerMovement>().jumpForce
                                        * Vector3.up);
}
