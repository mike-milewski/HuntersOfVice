using UnityEngine;
using TMPro;

public class MonsterButton : MonoBehaviour
{
    [SerializeField]
    private MonsterInformation monsterInformation = null;

    [SerializeField]
    private TextMeshProUGUI MonsterInfoText;

    [SerializeField]
    private int Index;

    public MonsterInformation GetMonsterInformation
    {
        get
        {
            return monsterInformation;
        }
        set
        {
            monsterInformation = value;
        }
    }

    public int GetIndex
    {
        get
        {
            return Index;
        }
        set
        {
            Index = value;
        }
    }

    public void ShowMonsterInfo()
    {
        MonsterInfoText.text = "<u>" + monsterInformation.GetCharacterData[Index].CharacterName + "</u>" + "\n\n" + "<size=12>" + "Level: " +
                                                                        monsterInformation.GetCharacterData[Index].CharacterLevel + "\n" + "HP: " +
                                                                        monsterInformation.GetCharacterData[Index].Health +
                                                                        "\n" + "Strength: " + monsterInformation.GetCharacterData[Index].Strength + "\n" + "Defense: " +
                                                                        monsterInformation.GetCharacterData[Index].Defense + "\n" + "Intelligence: " +
                                                                        monsterInformation.GetCharacterData[Index].Intelligence + "\n\n" + GetWeaknesses()
                                                                        + GetResistances() + GetImmunities() + GetAbsorbtions() + "\n"
                                                                        + "EXP: " + monsterInformation.GetCharacter.GetComponent<Enemy>().GetExperiencePoints + "\n" + 
                                                                        "Coins: " + monsterInformation.GetCharacter.GetComponent<Enemy>().GetCoins;
    }

    public string GetWeaknesses()
    {
        string weakness = "";
        string weak = "";

        if (monsterInformation.GetCharacterData[Index].Weaknesses.Length > 0)
        {
            for (int i = 0; i < monsterInformation.GetCharacterData[Index].Weaknesses.Length; i++)
            {
                weakness += "<#EFDFB8>" + monsterInformation.GetCharacterData[Index].Weaknesses[i] + "</color>" + " ";
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

        if (monsterInformation.GetCharacterData[Index].Resistances.Length > 0)
        {
            for (int i = 0; i < monsterInformation.GetCharacterData[Index].Resistances.Length; i++)
            {
                resistance += "<#EFDFB8>" + monsterInformation.GetCharacterData[Index].Resistances[i] + "</color>" + " ";
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

        if (monsterInformation.GetCharacterData[Index].Immunities.Length > 0)
        {
            for (int i = 0; i < monsterInformation.GetCharacterData[Index].Immunities.Length; i++)
            {
                immunities += "<#EFDFB8>" + monsterInformation.GetCharacterData[Index].Immunities[i] + "</color>" + " ";
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

        if (monsterInformation.GetCharacterData[Index].Absorbtions.Length > 0)
        {
            for (int i = 0; i < monsterInformation.GetCharacterData[Index].Absorbtions.Length; i++)
            {
                absorbtions += "<#EFDFB8>" + monsterInformation.GetCharacterData[Index].Absorbtions[i] + "</color>" + " ";
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
