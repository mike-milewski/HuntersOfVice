using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private TextMeshProUGUI CharacterName, Level, HP, MP, Strength, Defense, Intelligence;

    private void OnEnable()
    {
        SetCharacterInfoText();
    }

    public void SetCharacterInfoText()
    {
        CharacterName.text = character.GetCharacterData.name;
        Level.text = "Level: " + character.Level.ToString();
        HP.text = "HP: " + "<#5DFFB4>" + character.CurrentHealth + "</color>" + "/" + "<#5DFFB4>" + character.MaxHealth + "</color>";
        MP.text = "MP: " + "<#41E6F3>" + character.CurrentMana + "</color>" + "/" + "<#41E6F3>" + character.MaxMana + "</color>";
        Strength.text = "Strength: " + character.CharacterStrength;
        Defense.text = "Defense: " + character.CharacterDefense;
        Intelligence.text = "Intelligence: " + character.CharacterIntelligence;
    }
}
