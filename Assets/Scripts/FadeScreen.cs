using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeState { FADEIN, FADEOUT };

public class FadeScreen : MonoBehaviour
{
    [SerializeField]
    private Image Overlay;

    private Color alpha;

    [SerializeField]
    private FadeState fadeState;

    public FadeState GetFadeState
    {
        get
        {
            return fadeState;
        }
        set
        {
            fadeState = value;
        }
    }

    private void OnEnable()
    {
        alpha = Overlay.color;
    }

    private void Update()
    {
        switch (fadeState)
        {
            case (FadeState.FADEIN):
                FadeIn();
                break;
            case (FadeState.FADEOUT):
                FadeOut();
                break;
        }
    }

    private void FadeOut()
    {
        alpha.a += 2 * Time.deltaTime;
        Overlay.color = alpha;

        if (alpha.a >= 1)
        {
        }
    }

    private void FadeIn()
    {
        alpha.a -= 2 * Time.deltaTime;
        Overlay.color = alpha;

        if (alpha.a <= 0)
        {
        }
    }
}
