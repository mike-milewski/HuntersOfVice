#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    [SerializeField]
    private Character character, Knight, ShadowPriest;

    [SerializeField]
    private TextMeshProUGUI CharacterName, Level, HP, MP, Strength, Defense, Intelligence;

    private string StrengthStatColor = "<#FFFFFF>";

    private string DefenseStatColor = "<#FFFFFF>";

    private string IntelligenceStatColor = "<#FFFFFF>";

    public string GetStrengthStatColor
    {
        get
        {
            return StrengthStatColor;
        }
        set
        {
            StrengthStatColor = value;
        }
    }

    public string GetDefenseStatColor
    {
        get
        {
            return DefenseStatColor;
        }
        set
        {
            DefenseStatColor = value;
        }
    }

    public string GetIntelligenceStatColor
    {
        get
        {
            return IntelligenceStatColor;
        }
        set
        {
            IntelligenceStatColor = value;
        }
    }

    private void OnEnable()
    {
        if(Knight.gameObject.activeInHierarchy)
        {
            character = Knight;
        }
        else if(ShadowPriest.gameObject.activeInHierarchy)
        {
            character = ShadowPriest;
        }

        SetCharacterInfoText();
    }

    public void SetCharacterInfoText()
    {
        CharacterName.text = character.GetCharacterData.CharacterName;
        Level.text = "Level: " + character.Level.ToString();
        HP.text = "HP: " + "<#5DFFB4>" + character.CurrentHealth + "</color>" + " / " + "<#5DFFB4>" + character.MaxHealth + "</color>";
        MP.text = "MP: " + "<#41E6F3>" + character.CurrentMana + "</color>" + " / " + "<#41E6F3>" + character.MaxMana + "</color>";
        Strength.text = "Strength: " + StrengthStatColor + character.CharacterStrength;
        Defense.text = "Defense: " + DefenseStatColor + character.CharacterDefense;
        Intelligence.text = "Intelligence: " + IntelligenceStatColor + character.CharacterIntelligence;
    }

    public void TogglePanel()
    {
        if(!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
