using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterInformation : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI MonsterName;

    [SerializeField]
    private GameObject ParentObj;

    [SerializeField]
    private Button button;

    [SerializeField]
    private Character character = null;

    private void Awake()
    {
        ParentObj = transform.parent.parent.gameObject;
    }

    public Character GetCharacter
    {
        get
        {
            return character;
        }
        set
        {
            character = value;
        }
    }

    public TextMeshProUGUI GetMonsterName
    {
        get
        {
            return MonsterName;
        }
        set
        {
            MonsterName = value;
        }
    }

    public void ShowMonsterInfo()
    {
        ParentObj.GetComponent<MonsterBook>().GetMonsterInfoTxt.text = "<u>" + character.GetCharacterData.CharacterName + "</u>" + "\n\n" + "<size=12>" + "Level: " +
                                                                        character.GetCharacterData.CharacterLevel + "\n" + "HP: " + character.GetCharacterData.Health +
                                                                        "\n" + "Strength: " + character.GetCharacterData.Strength + "\n" + "Defense: " +
                                                                        character.GetCharacterData.Defense + "\n" + "Intelligence: " +
                                                                        character.GetCharacterData.Intelligence + "\n\n" + GetWeaknesses() + "EXP: " +
                                                                        character.GetComponent<Enemy>().GetExperiencePoints + "\n" + "Coins: " +
                                                                        character.GetComponent<Enemy>().GetCoins;
    }

    private string GetWeaknesses()
    {
        string weakness = "";

        if(character.GetCharacterData.Weaknesses[0] != ElementalWeaknesses.NONE && character.GetCharacterData.Resistances[0] == ElementalResistances.NONE
           && character.GetCharacterData.Immunities[0] == ElementalImmunities.NONE)
        {
            weakness = "Weak: " + "<#EFDFB8>" + character.GetCharacterData.Weaknesses[0] + "</color>" + "\n\n";
        }
        else if(character.GetCharacterData.Weaknesses[0] != ElementalWeaknesses.NONE && character.GetCharacterData.Resistances[0] != ElementalResistances.NONE
                && character.GetCharacterData.Immunities[0] == ElementalImmunities.NONE)
        {
            weakness = "Weak: " + "<#EFDFB8>" + character.GetCharacterData.Weaknesses[0] + "</color>" + "\n" + "Resist: " +
                       "<#EFDFB8>" + character.GetCharacterData.Resistances[0] + "</color>" + "\n\n";
        }
        else if (character.GetCharacterData.Weaknesses[0] != ElementalWeaknesses.NONE && character.GetCharacterData.Resistances[0] != ElementalResistances.NONE
                && character.GetCharacterData.Immunities[0] != ElementalImmunities.NONE)
        {
            weakness = "Weak: " + "<#EFDFB8>" + character.GetCharacterData.Weaknesses[0] + "</color>" + "\n" + "Resist: " + "<#EFDFB8>" + 
                       character.GetCharacterData.Resistances[0] + "</color>" + "\n" + "Immune: " + "<#EFDFB8>" + character.GetCharacterData.Immunities[0] + "</color>" + 
                       "\n\n";
        }
        else if (character.GetCharacterData.Weaknesses[0] == ElementalWeaknesses.NONE && character.GetCharacterData.Resistances[0] != ElementalResistances.NONE
                && character.GetCharacterData.Immunities[0] != ElementalImmunities.NONE)
        {
            weakness = "Resist: " + "<#EFDFB8>" + character.GetCharacterData.Resistances[0] + "</color>" + "\n" + "Immune: " + "<#EFDFB8>" + 
                       character.GetCharacterData.Immunities[0] + "</color>" + "\n\n";
        }
        else if (character.GetCharacterData.Weaknesses[0] == ElementalWeaknesses.NONE && character.GetCharacterData.Resistances[0] == ElementalResistances.NONE
                && character.GetCharacterData.Immunities[0] != ElementalImmunities.NONE)
        {
            weakness = "Immune: " + "<#EFDFB8>" + character.GetCharacterData.Immunities[0] + "</color>" + "\n\n";
        }
        else if (character.GetCharacterData.Weaknesses[0] == ElementalWeaknesses.NONE && character.GetCharacterData.Resistances[0] != ElementalResistances.NONE
                && character.GetCharacterData.Immunities[0] == ElementalImmunities.NONE)
        {
            weakness = "Resist: " + "<#EFDFB8>" + character.GetCharacterData.Resistances[0] + "</color>" + "\n\n";
        }
        else
        {
            weakness = "";
        }

        return weakness;
    }
}
