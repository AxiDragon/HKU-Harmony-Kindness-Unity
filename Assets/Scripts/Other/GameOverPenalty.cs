using UnityEngine;

public class GameOverPenalty : MonoBehaviour
{
    void Start() => AreaTalk.gamePhase = Mathf.Max(AreaTalk.gamePhase--, 0);
}
