using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public static MenuButtons Instance = null;

    [SerializeField]
    private Slider slider = null;

    public Slider GetSlider
    {
        get
        {
            return slider;
        }
        set
        {
            slider = value;
        }
    }

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

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
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
        if (!GameManager.Instance.GetIsDead)
        {
            FadeScreen.Instance.GetReturnToMenu = true;
            FadeScreen.Instance.GetFadeState = FadeState.FADEOUT;

            GameManager.Instance.GetCharacter.DefaultStats();
        }
        else return;
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

    public void DisableBoxCollider(BoxCollider boxCollider)
    {
        boxCollider.enabled = false;
        boxCollider.size = new Vector3(0, 0, 0);
    }

    public void EnableMenuPanel(GameObject Panel)
    {
        Panel.SetActive(true);
    }

    public void Volume()
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

    public void PlayMenuSE()
    {
        SoundManager.Instance.Menu();
    }

    public void PlayReverseMenuSE()
    {
        SoundManager.Instance.ReverseMenu();
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

    public void ToggleAudio(Settings setting)
    {
        if (!setting.MuteAudio)
        {
            setting.MuteAudio = true;
            GameManager.Instance.GetAudioCamera.GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            setting.MuteAudio = false;
            GameManager.Instance.GetAudioCamera.GetComponent<AudioSource>().volume = 1;
        }
    }

    public void PlayPanelAnimation(GameObject panel)
    {
        panel.SetActive(true);
        panel.GetComponent<Animator>().SetBool("OpenMenu", true);
    }

    public void DisableStartButton(Button button)
    {
        button.interactable = false;
    }

    public void DisableMainMenuButton(Button button)
    {
        button.interactable = false;
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
        AudioText.text = "Music by Matthew Pablo http://www.matthewpablo.com \n\n HitCtrl \n\n HydroGene \n\n NenadSimic";
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

    public void ShowArtCreditType(TextMeshProUGUI ArtCredit)
    {
        ArtCredit.text = "ART";
    }

    public void ShowAudioCreditType(TextMeshProUGUI AudioCredit)
    {
        AudioCredit.text = "AUDIO";
    }

    public void ShowFontCreditType(TextMeshProUGUI FontCredit)
    {
        FontCredit.text = "FONTS";
    }

    public void HideAllCreditTypes(TextMeshProUGUI CreditType)
    {
        CreditType.text = "";
    }

    public void HideCreditsText(TextMeshProUGUI CreditsText)
    {
        CreditsText.text = "";
    }
}
