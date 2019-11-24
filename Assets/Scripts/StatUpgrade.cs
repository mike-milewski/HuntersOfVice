using UnityEngine;
using TMPro;

public enum StatType { HP, MP, Strength, Defense, Intelligence }

public class StatUpgrade : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private Experience experience;

    [SerializeField]
    private StatType stattype;

    [SerializeField]
    private TextMeshProUGUI StatText;

    [SerializeField]
    private int StatValue;

    private int StatIncrease;

    public int GetStatIncrease
    {
        get
        {
            return StatIncrease;
        }
        set
        {
            StatIncrease = value;
        }
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

    public void PlusStatHealth()
    {
        if(experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
            experience.GetStatPointsTxt.text = "<u>Stat Points</u> \n" + "        " + experience.GetStatPoints.ToString();

            StatIncrease += character.GetCharacterData.HpIncrease;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void PlusStatMana()
    {
        if (experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
            experience.GetStatPointsTxt.text = "<u>Stat Points</u> \n" + "        " + experience.GetStatPoints.ToString();

            StatIncrease += character.GetCharacterData.MpIncrease;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void PlusStatStrength()
    {
        if (experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
            experience.GetStatPointsTxt.text = "<u>Stat Points</u> \n" + "        " + experience.GetStatPoints.ToString();

            StatIncrease += StatValue;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void PlusStatDefense()
    {
        if (experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
            experience.GetStatPointsTxt.text = "<u>Stat Points</u> \n" + "        " + experience.GetStatPoints.ToString();

            StatIncrease += StatValue;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void PlusStatIntelligence()
    {
        if (experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
            experience.GetStatPointsTxt.text = "<u>Stat Points</u> \n" + "        " + experience.GetStatPoints.ToString();

            StatIncrease += StatValue;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void MinusStatHealth()
    {
        if(experience.GetStatPoints < experience.GetMaxStatPoints && StatIncrease > 0)
        {
            experience.GetStatPoints++;
            experience.GetStatPointsTxt.text = "<u>Stat Points</u> \n" + "        " + experience.GetStatPoints.ToString();

            StatIncrease -= character.GetCharacterData.HpIncrease;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void MinusStatMana()
    {
        if (experience.GetStatPoints < experience.GetMaxStatPoints && StatIncrease > 0)
        {
            experience.GetStatPoints++;
            experience.GetStatPointsTxt.text = "<u>Stat Points</u> \n" + "        " + experience.GetStatPoints.ToString();

            StatIncrease -= character.GetCharacterData.MpIncrease;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void MinusStatStrength()
    {
        if (experience.GetStatPoints < experience.GetMaxStatPoints && StatIncrease > 0)
        {
            experience.GetStatPoints++;
            experience.GetStatPointsTxt.text = "<u>Stat Points</u> \n" + "        " + experience.GetStatPoints.ToString();

            StatIncrease -= StatValue;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void MinusStatDefense()
    {
        if (experience.GetStatPoints < experience.GetMaxStatPoints && StatIncrease > 0)
        {
            experience.GetStatPoints++;
            experience.GetStatPointsTxt.text = "<u>Stat Points</u> \n" + "        " + experience.GetStatPoints.ToString();

            StatIncrease -= StatValue;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void MinusStatIntelligence()
    {
        if (experience.GetStatPoints < experience.GetMaxStatPoints && StatIncrease > 0)
        {
            experience.GetStatPoints++;
            experience.GetStatPointsTxt.text = "<u>Stat Points</u> \n" + "        " + experience.GetStatPoints.ToString();

            StatIncrease -= StatValue;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void AddStats()
    {
        switch(stattype)
        {
            case (StatType.HP):
                AddHealth();
                break;
            case (StatType.MP):
                AddMP();
                break;
            case (StatType.Strength):
                AddStrength();
                break;
            case (StatType.Defense):
                AddDefense();
                break;
            case (StatType.Intelligence):
                AddIntelligence();
                break;
        }
    }

    public void AddHealth()
    {
        character.MaxHealth += StatIncrease;

        character.GetComponent<Health>().ModifyHealth(StatIncrease);

        StatIncrease = 0;
        StatText.text = StatIncrease.ToString();
    }

    public void AddMP()
    {
        character.MaxMana += StatIncrease;

        character.GetComponent<Mana>().ModifyMana(StatIncrease);

        StatIncrease = 0;
        StatText.text = StatIncrease.ToString();
    }

    public void AddStrength()
    {
        character.CharacterStrength += StatIncrease;

        character.GetCharacterData.Strength = character.CharacterStrength;

        StatIncrease = 0;
        StatText.text = StatIncrease.ToString();
    }

    public void AddDefense()
    {
        character.CharacterDefense += StatIncrease;

        character.GetCharacterData.Defense = character.CharacterDefense;

        StatIncrease = 0;
        StatText.text = StatIncrease.ToString();
    }

    public void AddIntelligence()
    {
        character.CharacterIntelligence += StatIncrease;

        character.GetCharacterData.Intelligence = character.CharacterIntelligence;

        StatIncrease = 0;
        StatText.text = StatIncrease.ToString();
    }
}