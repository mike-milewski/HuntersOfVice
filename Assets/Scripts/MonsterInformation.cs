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

    [SerializeField]
    private bool IsSelected;

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

    public bool GetIsSelected
    {
        get
        {
            return IsSelected;
        }
        set
        {
            IsSelected = value;
        }
    }

    public void ShowMonsterInfo()
    {
        IsSelected = true;

        ShowLevelButtons();

        ParentObj.GetComponent<MonsterBook>().GetMonsterInfoTxt.text = "<u>" + characterData[0].CharacterName + "</u>" + "\n\n" + "<size=12>" + "Level: " +
                                                                        characterData[0].CharacterLevel + "\n" + "HP: " + characterData[0].Health +
                                                                        "\n" + "Strength: " + characterData[0].Strength + "\n" + "Defense: " +
                                                                        characterData[0].Defense + "\n" + "Intelligence: " +
                                                                        characterData[0].Intelligence + "\n\n" + GetWeaknesses() + GetResistances() + 
                                                                        GetImmunities() + GetAbsorbtions() + "\n" + "EXP: " +
                                                                        character.GetComponent<Enemy>().GetExperiencePoints + "\n" + "Coins: " +
                                                                        character.GetComponent<Enemy>().GetCoins;
    }

    public void ShowLevelButtons()
    {
        for(int j = 0; j < ParentObj.GetComponent<MonsterBook>().GetMonsterLevelButtons.Length; j++)
        {
            ParentObj.GetComponent<MonsterBook>().GetMonsterLevelButtons[j].gameObject.SetActive(false);
        }

        for(int i = 0; i < characterData.Count; i++)
        {
            ParentObj.GetComponent<MonsterBook>().GetMonsterLevelButtons[i].gameObject.SetActive(true);

            ParentObj.GetComponent<MonsterBook>().GetMonsterLevelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Lv: " + 
                                                                                                                             characterData[i].CharacterLevel.ToString();

            ParentObj.GetComponent<MonsterBook>().GetMonsterLevelButtons[i].GetComponent<MonsterButton>().GetMonsterInformation = this;

            ParentObj.GetComponent<MonsterBook>().GetMonsterLevelButtons[i].GetComponent<MonsterButton>().GetIndex = i;
        }
    }

    public string GetWeaknesses()
    {
        string weakness = "";
        string weak = "";

        if(characterData[0].Weaknesses.Length > 0)
        {
            for (int i = 0; i < character.GetCharacterData.Weaknesses.Length; i++)
            {
                weakness += "<#EFDFB8>" + character.GetCharacterData.Weaknesses[i] + "</color>" + " ";
            }
            weak = "Weak: " + weakness + "\n";
        }
        else
        {
            weak = "";
        }

        return weak;
    }

    public string GetResistances()
    {
        string resistance = "";
        string resist = "";

        if (characterData[0].Resistances.Length > 0)
        {
            for (int i = 0; i < character.GetCharacterData.Resistances.Length; i++)
            {
                resistance += "<#EFDFB8>" + character.GetCharacterData.Resistances[i] + "</color>" + " ";
            }
            resist = "Resist: " + resistance + "\n";
        }
        else
        {
            resist = "";
        }

        return resist;
    }

    public string GetImmunities()
    {
        string immunities = "";
        string immunity = "";

        if (characterData[0].Immunities.Length > 0)
        {
            for (int i = 0; i < characterData[0].Immunities.Length; i++)
            {
                immunities += "<#EFDFB8>" + characterData[0].Immunities[i] + "</color>" + " ";
            }
            immunity = "Resist: " + immunities + "\n";
        }
        else
        {
            immunity = "";
        }

        return immunity;
    }

    public string GetAbsorbtions()
    {
        string absorbtions = "";
        string absorb = "";

        if (characterData[0].Absorbtions.Length > 0)
        {
            for (int i = 0; i < characterData[0].Absorbtions.Length; i++)
            {
                absorbtions += "<#EFDFB8>" + characterData[0].Absorbtions[i] + "</color>" + " ";
            }
            absorb = "Resist: " + absorbtions + "\n";
        }
        else
        {
            absorb = "";
        }

        return absorb;
    }
}
