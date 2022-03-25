using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public EaseMode Mode;

    [Min(0.1f)]
    public float FadeDuration = 1;

    public float WaitDuration = 1;

    private float wait = 0;

    private float fade = 0;

    private CanvasRenderer target;

    private EaseMode currentAction;


    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<CanvasRenderer>();

        if (Mode == EaseMode.InOut)
            currentAction = EaseMode.In;
        else
            currentAction = Mode;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAction == EaseMode.None) return;

        fade += Time.deltaTime;

        if (currentAction == EaseMode.In)
        {
            if (fade == 0) target.SetAlpha(0);
            target.SetAlpha(Mathf.Lerp(0f, 1f, fade / FadeDuration));
        }
        else if (currentAction == EaseMode.Out)
        {
            if (fade == 0) target.SetAlpha(1);
            target.SetAlpha(Mathf.Lerp(1f, 0f, fade / FadeDuration));
        }

        if (Mode == EaseMode.InOut)
        {
            wait += Time.deltaTime;
        }

        if (fade >= FadeDuration)
        {
            if (Mode == EaseMode.InOut && currentAction == EaseMode.In)
            {
                if (wait >= WaitDuration)
                {
                    currentAction = EaseMode.Out;
                    fade = 0;
                }
            }
            else currentAction = EaseMode.None;
        }
    }
}

public enum EaseMode
{
    None,
    In,
    Out,
    InOut
}

    
    