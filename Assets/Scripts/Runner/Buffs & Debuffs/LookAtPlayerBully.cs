using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerBully : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(PlayerSwap.players[PlayerSwap.currentPlayer].transform);

        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }
}
