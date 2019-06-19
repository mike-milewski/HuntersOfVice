using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeState { FADEIN, FADEOUT };

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen Instance = null;

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

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        alpha = Overlay.color;

        if(fadeState == FadeState.FADEIN)
        {
            SetToMaxAlpha();
        }
        else
        {
            SetToMinAlpha();
        }
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

        if(alpha.a >= 1)
        {
            gameObject.SetActive(false);
        }
    }

    private void FadeIn()
    {
        alpha.a -= 2 * Time.deltaTime;
        Overlay.color = alpha;

        if(alpha.a <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void EnableFadeScreen()
    {
        gameObject.SetActive(true);
    }

    public void SetToMaxAlpha()
    {
        alpha.a = 1;
    }

    public void SetToMinAlpha()
    {
        alpha.a = 0;
    }
}
