#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField]
    private SelectedCharacter selectedCharacter;

    [SerializeField]
    private Animator CharacterInfoPanel, StartButton;

    [SerializeField]
    private GameObject Knight, ShadowPriest;

    [SerializeField]
    private GameObject[] SkillExamples;

    [SerializeField]
    private Sprite[] SkillImages;

    [SerializeField]
    private TextMeshProUGUI CharacterInformationText, CharacterNameText;

    [SerializeField][TextArea]
    private string CharacterInformaion;

    [SerializeField]
    private string CharacterClass;

    private void OnEnable()
    {
        SelectedCharacter SC = FindObjectOfType<SelectedCharacter>();

        selectedCharacter = SC;
    }

    public string GetCharacterClass
    {
        get
        {
            return CharacterClass;
        }
        set
        {
            CharacterClass = value;
        }
    }

    public SelectedCharacter GetSelectedCharacter
    {
        get
        {
            return selectedCharacter;
        }
        set
        {
            selectedCharacter = value;
        }
    }

    public void DisableKnightCollider()
    {
        Knight.GetComponent<BoxCollider>().enabled = false;
    }

    public void DisableShadowPriestCollider()
    {
        ShadowPriest.GetComponent<BoxCollider>().enabled = false;
    }

    public void EnableKnightCollider()
    {
        Knight.GetComponent<BoxCollider>().enabled = true;
    }

    public void EnableShadowPriestCollider()
    {
        ShadowPriest.GetComponent<BoxCollider>().enabled = true;
    }

    public void EndCharacterSelectionAnimation()
    {
        gameObject.GetComponent<Animator>().SetBool("CharacterSelection", false);

        EnableKnightCollider();
        EnableShadowPriestCollider();
    }

    public void PlayPanelAndButtonAnimations()
    {
        if(!CharacterInfoPanel.GetBool("OpenMenu"))
        {
            CharacterInfoPanel.GetComponent<AudioSource>().Play();
        }

        CharacterInfoPanel.SetBool("OpenMenu", true);
        StartButton.SetBool("ShowButton", true);
    }

    public void ShowCharacterInformation()
    {
        CharacterInformationText.text = CharacterInformaion;
        CharacterNameText.text = CharacterClass;
    }

    public void ShowCharacterSkills()
    {
        for(int i = 0; i < SkillExamples.Length; i++)
        {
            SkillExamples[i].GetComponent<Image>().sprite = SkillImages[i];
        }
    }

    public void PlayPoseSoundEffect()
    {
        if(selectedCharacter.GetKnightSelected)
        {
            SoundManager.Instance.SwordSwing();
        }
        else if(selectedCharacter.GetShadowPriestSelected)
        {
            SoundManager.Instance.ContractCast();
        }
    }
}
