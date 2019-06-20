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
        HP.text = "HP: " + character.CurrentHealth + "/" + character.MaxHealth;
        MP.text = "MP: " + character.CurrentMana + "/" + character.MaxMana;
        Strength.text = "Strength: " + character.GetCharacterData.Strength;
        Defense.text = "Defense: " + character.GetCharacterData.Defense;
        Intelligence.text = "Intelligence: " + character.GetCharacterData.Intelligence;
    }
}
