using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public void DisableButtons(GameObject ButtonPanel)
    {
        foreach(Button button in ButtonPanel.GetComponentsInChildren<Button>())
        {
            button.interactable = false;
        }
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

    public void EnableMenuPanel(GameObject Panel)
    {
        Panel.SetActive(true);
    }

    public void Volume(Slider slider)
    {
        var audio = SoundManager.Instance.GetAudioSource;

        foreach(AudioSource source in audio)
        {
            source.volume = slider.value;
        }
        GameManager.Instance.GetCamera.transform.parent.GetComponent<AudioSource>().volume = slider.value;
    }

    public void PlayButtonClick()
    {
        SoundManager.Instance.ButtonClick();
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

    public void TurnParticleEffectsOn(Settings setting)
    {
        if(!setting.UseParticleEffects)
        {
            setting.UseParticleEffects = true;
        }
        else
        {
            setting.UseParticleEffects = false;
        }
    }

    public void PlayPanelAnimation(GameObject panel)
    {
        panel.SetActive(true);
        panel.GetComponent<Animator>().SetBool("OpenMenu", true);
    }

    public void ClosePanelAnimation(GameObject panel)
    {
        panel.GetComponent<Animator>().SetBool("OpenMenu", false);
    }

    public void PlayMenuAnimation(Animator animator)
    {
        animator.SetBool("FadeIn", true);
    }

    public void ReverseMenuAnimation(Animator animator)
    {
        animator.SetBool("FadeIn", false);
    }

    public void ReverseShopLevelUpAnimation(GameObject go)
    {
        go.SetActive(false);
    }

    public void ShowArtCreditsText(TextMeshProUGUI ArtText)
    {
        ArtText.text = "Space Hydra \n\n StumpyStrust \n\n Lamoot";
    }

    public void ShowAudioCreditsText(TextMeshProUGUI AudioText)
    {
        AudioText.text = "Matthew Pablo \n\n HitCtrl \n\n HydroGene";
    }

    public void ShowFontsCreditsText(TextMeshProUGUI FontsText)
    {
        FontsText.text = "Jonathan S. Harris";
    }

    public void ShowWeaponsText(TextMeshProUGUI WeaponsText)
    {
        WeaponsText.text = "Weapons";
    }

    public void ShowArmorText(TextMeshProUGUI ArmorText)
    {
        ArmorText.text = "Armor";
    }

    public void ShowMonstersText(TextMeshProUGUI MonstersText)
    {
        MonstersText.text = "Monsters";
    }

    public void ShowBossesText(TextMeshProUGUI BossesText)
    {
        BossesText.text = "Bosses";
    }
}
