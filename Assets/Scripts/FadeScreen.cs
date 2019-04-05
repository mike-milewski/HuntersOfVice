using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField]
    private Image Overlay;

    private void Awake()
    {
        FadeOut();
    }

    private void FadeIn()
    {
        Overlay.CrossFadeAlpha(1, 0.9f, false);
    }

    private void FadeOut()
    {
        Overlay.CrossFadeAlpha(0, 0.9f, false);
    }
}
