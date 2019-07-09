using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public static MenuButtons Instance = null;

    private void Awake()
    {
        #region Singleton
        Instance = this;
        #endregion
    }

    public void NewGame(GameObject Loadingbar)
    {
        Loadingbar.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        FadeScreen.Instance.GetReturnToMenu = true;
        FadeScreen.Instance.GetFadeState = FadeState.FADEOUT;
    }

    public void ToggleMenuPanel(GameObject Panel)
    {
        if(!Panel.activeInHierarchy)
        {
            Panel.SetActive(true);
        }
        else
        {
            Panel.SetActive(false);
        }
    }

    public void Volume(Slider slider)
    {
        var audio = SoundManager.Instance.GetAudioSource;

        foreach(AudioSource source in audio)
        {
            source.volume = slider.value;
        }
    }

    public void ToggleSkillsPanel()
    {
        if(!GameManager.Instance.GetSkillPanel.GetComponent<Image>().enabled)
        {
            GameManager.Instance.UnmaskSkillsPanel();
        }
        else
        {
            GameManager.Instance.MaskSkillsPanel();
        }
    }

    public void TurnOffSkillsPanel()
    {
        GameManager.Instance.MaskSkillsPanel();
    }
}
