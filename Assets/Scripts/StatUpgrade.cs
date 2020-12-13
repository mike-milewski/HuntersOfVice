#pragma warning disable 0649
using UnityEngine;
using TMPro;

public enum StatType { HP, MP, Strength, Defense, Intelligence }

public class StatUpgrade : MonoBehaviour
{
    [SerializeField]
    private Character character = null, Knight, ShadowPriest, Toadstool;

    [SerializeField]
    private Experience experience;

    [SerializeField]
    private StatType stattype;

    [SerializeField]
    private TextMeshProUGUI StatText;

    private int StatIncrease, StrengthValue, IntelligenceValue;

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

    private void OnEnable()
    {
        if(Knight.gameObject.activeInHierarchy)
        {
            character = Knight;
            experience = Knight.GetComponent<Experience>();

            StrengthValue = 2;
            IntelligenceValue = 1;
        }
        if(ShadowPriest.gameObject.activeInHierarchy)
        {
            character = ShadowPriest;
            experience = ShadowPriest.GetComponent<Experience>();

            StrengthValue = 1;
            IntelligenceValue = 2;
        }
        if (Toadstool.gameObject.activeInHierarchy)
        {
            character = Toadstool;
            experience = Toadstool.GetComponent<Experience>();

            StrengthValue = 2;
            IntelligenceValue = 2;
        }
    }

    public void PlusStatHealth()
    {
        if(experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
            experience.GetStatPointsTxt.text = experience.GetStatPoints.ToString();

            StatIncrease += character.GetCharacterData.HpIncrease;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void PlusStatMana()
    {
        if (experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
            experience.GetStatPointsTxt.text = experience.GetStatPoints.ToString();

            StatIncrease += character.GetCharacterData.MpIncrease;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void PlusStatStrength()
    {
        if (experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
            experience.GetStatPointsTxt.text = experience.GetStatPoints.ToString();

            StatIncrease += StrengthValue;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void PlusStatDefense()
    {
        if (experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
            experience.GetStatPointsTxt.text = experience.GetStatPoints.ToString();

            StatIncrease++;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void PlusStatIntelligence()
    {
        if (experience.GetStatPoints > 0)
        {
            experience.GetStatPoints--;
            experience.GetStatPointsTxt.text = experience.GetStatPoints.ToString();

            StatIncrease += IntelligenceValue;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void MinusStatHealth()
    {
        if(experience.GetStatPoints < experience.GetMaxStatPoints && StatIncrease > 0)
        {
            experience.GetStatPoints++;
            experience.GetStatPointsTxt.text = experience.GetStatPoints.ToString();

            StatIncrease -= character.GetCharacterData.HpIncrease;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void MinusStatMana()
    {
        if (experience.GetStatPoints < experience.GetMaxStatPoints && StatIncrease > 0)
        {
            experience.GetStatPoints++;
            experience.GetStatPointsTxt.text = experience.GetStatPoints.ToString();

            StatIncrease -= character.GetCharacterData.MpIncrease;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void MinusStatStrength()
    {
        if (experience.GetStatPoints < experience.GetMaxStatPoints && StatIncrease > 0)
        {
            experience.GetStatPoints++;
            experience.GetStatPointsTxt.text = experience.GetStatPoints.ToString();

            StatIncrease -= StrengthValue;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void MinusStatDefense()
    {
        if (experience.GetStatPoints < experience.GetMaxStatPoints && StatIncrease > 0)
        {
            experience.GetStatPoints++;
            experience.GetStatPointsTxt.text = experience.GetStatPoints.ToString();

            StatIncrease--;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void MinusStatIntelligence()
    {
        if (experience.GetStatPoints < experience.GetMaxStatPoints && StatIncrease > 0)
        {
            experience.GetStatPoints++;
            experience.GetStatPointsTxt.text = experience.GetStatPoints.ToString();

            StatIncrease -= IntelligenceValue;
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
        if(StatIncrease > 0)
        {
            character.MaxHealth += StatIncrease;

            character.GetComponent<Health>().ModifyHealth(StatIncrease);

            StatIncrease = 0;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void AddMP()
    {
        if(StatIncrease > 0)
        {
            character.MaxMana += StatIncrease;

            character.GetComponent<Mana>().ModifyMana(StatIncrease);

            StatIncrease = 0;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void AddStrength()
    {
        if(StatIncrease > 0)
        {
            character.CharacterStrength += StatIncrease;

            character.GetCharacterData.Strength += StatIncrease;

            StatIncrease = 0;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void AddDefense()
    {
        if(StatIncrease > 0)
        {
            character.CharacterDefense += StatIncrease;

            character.GetCharacterData.Defense += StatIncrease;

            StatIncrease = 0;
            StatText.text = StatIncrease.ToString();
        }
    }

    public void AddIntelligence()
    {
        if(StatIncrease > 0)
        {
            character.CharacterIntelligence += StatIncrease;

            character.GetCharacterData.Intelligence += StatIncrease;

            StatIncrease = 0;
            StatText.text = StatIncrease.ToString();
        }
    }
}