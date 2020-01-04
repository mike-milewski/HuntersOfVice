using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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

    [SerializeField]
    private List<CharacterData> characterData = new List<CharacterData>();

    private void Awake()
    {
        ParentObj = transform.parent.parent.parent.gameObject;
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

    public List<CharacterData> GetCharacterData
    {
        get
        {
            return characterData;
        }
        set
        {
            characterData = value;
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
        ShowLevelButtons();

        ParentObj.GetComponent<MonsterBook>().GetMonsterInfoTxt.text = "<u>" + characterData[0].CharacterName + "</u>" + "\n\n" + "<size=12>" + "Level: " +
                                                                        characterData[0].CharacterLevel + "\n" + "HP: " + characterData[0].Health +
                                                                        "\n" + "Strength: " + characterData[0].Strength + "\n" + "Defense: " +
                                                                        characterData[0].Defense + "\n" + "Intelligence: " +
                                                                        characterData[0].Intelligence + "\n\n" + GetWeaknesses() + "EXP: " +
                                                                        character.GetComponent<Enemy>().GetExperiencePoints + "\n" + "Coins: " +
                                                                        character.GetComponent<Enemy>().GetCoins;
    }

    private void ShowLevelButtons()
    {
        for(int i = 0; i < characterData.Count; i++)
        {
            ParentObj.GetComponent<MonsterBook>().GetMonsterLevelButtons[i].gameObject.SetActive(true);

            ParentObj.GetComponent<MonsterBook>().GetMonsterLevelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Lv: " + characterData[i].CharacterLevel.ToString();
        }
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
