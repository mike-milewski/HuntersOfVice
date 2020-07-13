#pragma warning disable 0649
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum FadeState { FADEIN, FADEOUT, NONE };

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen Instance = null;

    [SerializeField]
    private Image Overlay;

    private Color alpha;

    private bool ReturnToMenu;

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

    public bool GetReturnToMenu
    {
        get
        {
            return ReturnToMenu;
        }
        set
        {
            ReturnToMenu = value;
        }
    }

    private void Awake()
    {
        Instance = this;

        alpha.a = 1;

        fadeState = FadeState.FADEIN;
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
            case (FadeState.NONE):
                return;
        }
    }

    private void FadeOut()
    {
        alpha.a += 2 * Time.deltaTime;
        Overlay.color = alpha;

        if(alpha.a >= 1)
        {
            fadeState = FadeState.NONE;
            if(ReturnToMenu)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    private void FadeIn()
    {
        alpha.a -= 2 * Time.deltaTime;
        Overlay.color = alpha;

        if(alpha.a <= 0)
        {
            fadeState = FadeState.NONE;
        }
    }
}
