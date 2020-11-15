#pragma warning disable 0649, 0414
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EquipmentType { Weapon, Armor }

public enum StatIncreaseType { HP, MP, Strength, Defense, Intelligence }

public enum Ability { NONE, SwiftStrike, StormThrust, BurnStatus, ReducedAutoAttack, SlowStatus, Tenacity, ManaPulse, StrengthIntelligenceReverse, Alleviate, 
                      HpForSkillCast, WhirlwindSlash, EvilsEnd, CriticalChanceIncrease, ExtraContract, BraveLight, Contracts, MiasmaPulse, NetherStar, IgnoreDefense }

[System.Serializable]
public class StatusType
{
    [SerializeField]
    private StatIncreaseType StatIncreaseType;

    [SerializeField]
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

    public StatIncreaseType GetStatusTypes
    {
        get
        {
            return StatIncreaseType;
        }
        set
        {
            StatIncreaseType = value;
        }
    }
}

public class Equipment : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private EquipmentData equipmentData;

    [SerializeField]
    private EquippedCheck equippedCheck;

    [SerializeField]
    private BasicAttack basicAttack;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private Shop shop;

    [SerializeField]
    private Sprite EquipmentSprite;

    [SerializeField]
    private TextMeshProUGUI EquipmentNameText, EquipmentInfoText, EquipmentPanelText, ShopEquipmentPanelText;

    [SerializeField]
    private Skills skill = null;

    [SerializeField]
    private Skills[] ContractSkills;

    [SerializeField]
    private Items items = null;

    [SerializeField]
    private EquipmentType equipmentType;

    [SerializeField]
    private Ability equipmentAbility;

    [SerializeField]
    private StatusType[] stattype;

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

    public EquipmentData GetEquipmentData
    {
        get
        {
            return equipmentData;
        }
        set
        {
            equipmentData = value;
        }
    }

    public EquipmentType GetEquipmentType
    {
        get
        {
            return equipmentType;
        }
        set
        {
            equipmentType = value;
        }
    }

    public Ability GetSkillAbility
    {
        get
        {
            return equipmentAbility;
        }
        set
        {
            equipmentAbility = value;
        }
    }

    public Sprite GetEquipmentSprite
    {
        get
        {
            return EquipmentSprite;
        }
        set
        {
            EquipmentSprite = value;
        }
    }

    public StatusType[] GetStatusType
    {
        get
        {
            return stattype;
        }
        set
        {
            stattype = value;
        }
    }

    private void Awake()
    {
        gameObject.GetComponent<Image>().sprite = EquipmentSprite;
    }

    public void Equip()
    {
        equippedCheck.GetIsEquipped = true;
        switch (equipmentType)
        {
            case (EquipmentType.Weapon):
                WeaponEquip();
                basicAttack.GetEquipment[0] = this;
                basicAttack.GetPlayerElement = basicAttack.GetEquipment[0].equipmentData.Element;
                break;
            case (EquipmentType.Armor):
                ArmorEquip();
                basicAttack.GetEquipment[1] = this;
                break;
        }
    }

    public void UnEquip()
    {
        DecreaseStats();
        LoseEquipmentAbility();
        switch(equipmentType)
        {
            case (EquipmentType.Weapon):
                basicAttack.GetEquipment[0] = null;
                if(character.GetCharacterData.name == "Knight")
                {
                    basicAttack.GetPlayerElement = PlayerElement.Physical;
                }
                else
                {
                    basicAttack.GetPlayerElement = PlayerElement.Magic;
                }
                break;
            case (EquipmentType.Armor):
                basicAttack.GetEquipment[1] = null;
                break;
        }
        equippedCheck.GetIsEquipped = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        EquipmentNameText.text = "";
        EquipmentInfoText.text = "";
    }

    private void WeaponEquip()
    {
        IncreaseStats();

        GainEquipmentAbility();

        CurrentWeaponEquippedText();
    }

    private void ArmorEquip()
    {
        IncreaseStats();

        GainEquipmentAbility();

        CurrentArmorEquippedText();
    }

    private void IncreaseStats()
    {
        for (int i = 0; i < stattype.Length; i++)
        {
            switch (stattype[i].GetStatusTypes)
            {
                case (StatIncreaseType.HP):
                    character.MaxHealth += stattype[i].GetStatIncrease;
                    character.GetComponent<Health>().GetFilledBar();
                    break;
                case (StatIncreaseType.MP):
                    character.MaxMana += stattype[i].GetStatIncrease;
                    character.GetComponent<Mana>().GetFilledBar();
                    break;
                case (StatIncreaseType.Strength):
                    character.CharacterStrength += stattype[i].GetStatIncrease;
                    break;
                case (StatIncreaseType.Defense):
                    character.CharacterDefense += stattype[i].GetStatIncrease;
                    break;
                case (StatIncreaseType.Intelligence):
                    character.CharacterIntelligence += stattype[i].GetStatIncrease;
                    break;
            }
        }
        SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
    }

    private void DecreaseStats()
    {
        for (int i = 0; i < stattype.Length; i++)
        {
            switch (stattype[i].GetStatusTypes)
            {
                case (StatIncreaseType.HP):
                    character.MaxHealth -= stattype[i].GetStatIncrease;
                    if (character.CurrentHealth > character.MaxHealth)
                    {
                        character.CurrentHealth = character.MaxHealth;
                    }
                    else
                    {
                        character.GetCharacterData.Health = character.MaxHealth;
                    }
                    character.GetComponent<Health>().GetFilledBar();
                    break;
                case (StatIncreaseType.MP):
                    character.MaxMana -= stattype[i].GetStatIncrease;
                    if(character.CurrentMana > character.MaxMana)
                    {
                        character.CurrentMana = character.MaxMana;
                    }
                    else
                    {
                        character.GetCharacterData.Mana = character.MaxMana;
                    }
                    character.GetComponent<Mana>().GetFilledBar();
                    break;
                case (StatIncreaseType.Strength):
                    character.CharacterStrength -= stattype[i].GetStatIncrease;
                    break;
                case (StatIncreaseType.Defense):
                    character.CharacterDefense -= stattype[i].GetStatIncrease;
                    break;
                case (StatIncreaseType.Intelligence):
                    character.CharacterIntelligence -= stattype[i].GetStatIncrease;
                    break;
            }
        }
        SkillsManager.Instance.GetCharacterMenu.SetCharacterInfoText();
    }

    public void GetEquipmentStats()
    {
        IncreaseStats();
    }

    private void CurrentWeaponEquippedText()
    {
        EquipmentNameText.text = equipmentData.EquipmentName;

        if(equipmentData.Element != PlayerElement.NONE)
        {
            EquipmentInfoText.text = StatsText().text + "\n" + "Element: " + equipmentData.Element + EquipmentAbilityTextInEquipMenu();
        }
    }

    private void CurrentArmorEquippedText()
    {
        EquipmentNameText.text = equipmentData.EquipmentName;

        EquipmentInfoText.text = StatsText().text + EquipmentAbilityTextInEquipMenu();
    }

    public void PanelText(GameObject panel)
    {
        if(!gameObject.transform.parent.GetComponent<EquipmentCheck>())
        {
            panel.SetActive(true);

            EquipmentStatsInEquipmentMenu();
        }
    }

    private TextMeshProUGUI EquipmentStatsInEquipmentMenu()
    {
        if (stattype.Length == 1)
        {
            if (equipmentData.Element != PlayerElement.NONE)
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" + "Element: " + equipmentData.Element +
                                        EquipmentAbilityText() + "\n\n" + "Sell Value: " + SellValue();
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Sell Value: " + SellValue();
            }
        }
        if (stattype.Length == 2)
        {
            if (equipmentData.Element != PlayerElement.NONE)
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" + "Element: " + equipmentData.Element +
                                                        EquipmentAbilityText() + "\n\n" + "Sell Value: " + SellValue();
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Sell Value: " + SellValue();
            }
        }
        if (stattype.Length == 3)
        {
            if (equipmentData.Element != PlayerElement.NONE)
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                       stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                       stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                       stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" + "Element: " + equipmentData.Element +
                                       EquipmentAbilityText() + "\n\n" + "Sell Value: " + SellValue();
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                       stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                       stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                       stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + EquipmentAbilityText() +
                                       "\n\n" + "Sell Value: " + SellValue();
            }
        }
        if (stattype.Length == 4)
        {
            if (equipmentData.Element != PlayerElement.NONE)
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" + "Element: " + equipmentData.Element +
                                                        EquipmentAbilityText() + "\n\n" + "Sell Value: " + SellValue();
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Sell Value: " + SellValue();
            }
        }
        if (stattype.Length == 5)
        {
            if (equipmentData.Element != PlayerElement.NONE)
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" +
                                                        stattype[4].GetStatusTypes + " +" + stattype[4].GetStatIncrease + "\n" + "Element: " + equipmentData.Element +
                                                        EquipmentAbilityText() + "\n\n" + "Sell Value: " + SellValue();
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" +
                                        stattype[4].GetStatusTypes + " +" + stattype[4].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Sell Value: " + SellValue();
            }
        }
        return EquipmentPanelText;
    }

    private TextMeshProUGUI EquipmentStatsInShopMenu()
    {
        if (stattype.Length == 1)
        {
            if (equipmentData.Element != PlayerElement.NONE)
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" + "Element: " + equipmentData.Element +
                                        EquipmentAbilityText() + "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
        }
        if (stattype.Length == 2)
        {
            if (equipmentData.Element != PlayerElement.NONE)
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" + "Element: " + equipmentData.Element +
                                                        EquipmentAbilityText() + "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
        }
        if (stattype.Length == 3)
        {
            if (equipmentData.Element != PlayerElement.NONE)
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                       stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                       stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                       stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" + "Element: " + equipmentData.Element + EquipmentAbilityText() +
                                       "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                       stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                       stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                       stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + EquipmentAbilityText() +
                                       "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
        }
        if (stattype.Length == 4)
        {
            if (equipmentData.Element != PlayerElement.NONE)
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" + "Element: " + equipmentData.Element +
                                                        EquipmentAbilityText() + "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
        }
        if (stattype.Length == 5)
        {
            if (equipmentData.Element != PlayerElement.NONE)
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" +
                                                        stattype[4].GetStatusTypes + " +" + stattype[4].GetStatIncrease + "\n" + "Element: " + equipmentData.Element +
                                                        EquipmentAbilityText() + "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" +
                                        stattype[4].GetStatusTypes + " +" + stattype[4].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
        }
        return EquipmentPanelText;
    }

    private TextMeshProUGUI StatsText()
    {
        if (stattype.Length == 1)
        {
            EquipmentInfoText.text = stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease;
        }
        if (stattype.Length == 2)
        {
            EquipmentInfoText.text = stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                     stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease;
        }
        if (stattype.Length == 3)
        {
            EquipmentInfoText.text = stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                     stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                     stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease;
        }
        if (stattype.Length == 4)
        {
            EquipmentInfoText.text = stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                     stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                     stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                     stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease;
        }
        if (stattype.Length == 5)
        {
            EquipmentInfoText.text = stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                     stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                     stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                     stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" +
                                     stattype[4].GetStatusTypes + " +" + stattype[4].GetStatIncrease;
        }
        return EquipmentInfoText;
    }

    public string EquipmentAbilityTextInEquipMenu()
    {
        string skillText = "";

        switch (equipmentAbility)
        {
            case (Ability.SwiftStrike):
                skillText = "\n\n" + "<#EFDFB8>" + "Removes the MP cost of Swift Strike." + "</color> ";
                break;
            case (Ability.StormThrust):
                skillText = "\n\n" + "<#EFDFB8>" + "Storm Thrust - Stun status." + "</color> ";
                break;
            case (Ability.BurnStatus):
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack - 10% Burning status." + "</color> ";
                break;
            case (Ability.ReducedAutoAttack):
                skillText = "\n\n" + "<#EFDFB8>" + "Reduces auto-attack time by 1 second." + "</color> ";
                break;
            case (Ability.SlowStatus):
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack - 10% Slowed status." + "</color> ";
                break;
            case (Ability.Tenacity):
                skillText = "\n\n" + "<#EFDFB8>" + "Tenacity - strength 30%." + "</color> ";
                break;
            case (Ability.ManaPulse):
                skillText = "\n\n" + "<#EFDFB8>" + "Ether - Mana Pulse." + "</color> ";
                break;
            case (Ability.StrengthIntelligenceReverse):
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack - Damage based on intelligence." + "</color> ";
                break;
            case (Ability.Alleviate):
                skillText = "\n\n" + "<#EFDFB8>" + "Alleviate heals HP by 30% instead." + "</color> ";
                break;
            case (Ability.HpForSkillCast):
                skillText = "\n\n" + "<#EFDFB8>" + "Skills - 3% HP cost." + "</color> ";
                break;
            case (Ability.WhirlwindSlash):
                skillText = "\n\n" + "<#EFDFB8>" + "Whirlwind Slash - Wind Scarred status." + "</color> ";
                break;
            case (Ability.EvilsEnd):
                skillText = "\n\n" + "<#EFDFB8>" + "Evil's End - 40% HP requirement." + "</color> ";
                break;
            case (Ability.CriticalChanceIncrease):
                skillText = "\n\n" + "<#EFDFB8>" + "Critical Hit Chance - 15%." + "</color> ";
                break;
            case (Ability.ExtraContract):
                skillText = "\n\n" + "<#EFDFB8>" + "Additional contract stack." + "</color> ";
                break;
            case (Ability.BraveLight):
                skillText = "\n\n" + "<#EFDFB8>" + "Bravelight - 45s cooldown." + "</color> ";
                break;
            case (Ability.Contracts):
                skillText = "\n\n" + "<#EFDFB8>" + "Contracts - Removed negative status." + "</color> ";
                break;
            case (Ability.MiasmaPulse):
                skillText = "\n\n" + "<#EFDFB8>" + "Potion - Miasma Pulse." + "</color> ";
                break;
            case (Ability.NetherStar):
                skillText = "\n\n" + "<#EFDFB8>" + "Nether Star - Increased power." + "</color> ";
                break;
            case (Ability.IgnoreDefense):
                skillText = "\n\n" + "<#EFDFB8>" + "Ignores enemy's defense." + "</color> ";
                break;
        }
        return skillText;
    }

    public string EquipmentAbilityText()
    {
        string skillText = "";

        switch(equipmentAbility)
        {
            case (Ability.SwiftStrike):
                skillText = "\n\n" + "<#EFDFB8>" + "Removes the MP cost of Swift Strike." + "</color> ";
                break;
            case (Ability.StormThrust):
                skillText = "\n\n" + "<#EFDFB8>" + "Storm Thrust now applies the Stun status effect instead with a 5 second duration and a 15 second cooldown." + "</color> ";
                break;
            case (Ability.BurnStatus):
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack has a 10% chance of inflicting the Burning status effect." + "</color> ";
                break;
            case (Ability.ReducedAutoAttack):
                skillText = "\n\n" + "<#EFDFB8>" + "Reduces auto-attack time by 1 second." + "</color> ";
                break;
            case (Ability.SlowStatus):
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack has a 10% chance of inflicting the Slowed status effect." + "</color> ";
                break;
            case (Ability.Tenacity):
                skillText = "\n\n" + "<#EFDFB8>" + "Tenacity increases strength by 30% instead." + "</color> ";
                break;
            case (Ability.ManaPulse):
                skillText = "\n\n" + "<#EFDFB8>" + "Ether discharges a potent wave that deals damage to all targets in range with a power of 60." + "</color> ";
                break;
            case (Ability.StrengthIntelligenceReverse):
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack applies damage based on intelligence instead." + "</color> ";
                break;
            case (Ability.Alleviate):
                skillText = "\n\n" + "<#EFDFB8>" + "Alleviate heals HP by 30% instead." + "</color> ";
                break;
            case (Ability.HpForSkillCast):
                skillText = "\n\n" + "<#EFDFB8>" + "Skills cost 3% of your HP to cast instead of using MP." + "</color> ";
                break;
            case (Ability.WhirlwindSlash):
                skillText = "\n\n" + "<#EFDFB8>" + "Whirlwind Slash gains the Wind Scarred status effect, dealing damage gradually." + "</color> ";
                break;
            case (Ability.EvilsEnd):
                skillText = "\n\n" + "<#EFDFB8>" + "The HP penalty for Evil's End is increased to 40%." + "</color> ";
                break;
            case (Ability.CriticalChanceIncrease):
                skillText = "\n\n" + "<#EFDFB8>" + "Increases Critical Hit rate to 15%." + "</color> ";
                break;
            case (Ability.ExtraContract):
                skillText = "\n\n" + "<#EFDFB8>" + "Allows the stacking of an additional contract." + "</color> ";
                break;
            case (Ability.BraveLight):
                skillText = "\n\n" + "<#EFDFB8>" + "Reduces the cooldown of Bravelight by half." + "</color> ";
                break;
            case (Ability.Contracts):
                skillText = "\n\n" + "<#EFDFB8>" + "Removes the detrimental effects of all contracts." + "</color> ";
                break;
            case (Ability.MiasmaPulse):
                skillText = "\n\n" + "<#EFDFB8>" + "Potion discharges a toxic wave that poisons all targets in range with a power of 30." + "</color> ";
                break;
            case (Ability.NetherStar):
                skillText = "\n\n" + "<#EFDFB8>" + "Increases the power of Nether Star by 200." + "</color> ";
                break;
            case (Ability.IgnoreDefense):
                skillText = "\n\n" + "<#EFDFB8>" + "Ignores enemies defense." + "</color> ";
                break;
        }
        return skillText;
    }

    private void GainEquipmentAbility()
    {
        switch (equipmentAbility)
        {
            case (Ability.NONE):
                return;
            case (Ability.SwiftStrike):
                skill.GetManaCost = 0;
                break;
            case (Ability.StormThrust):
                skill.GetEnemyStatusEffect = StatusEffect.Stun;
                skill.GetStatusEffectName = "Stun";
                skill.GetStatusDescription = "Unable to act.";
                skill.GetAddedEffect = "Unable to act";
                skill.GetStatusDuration = 5.0f;
                skill.GetCoolDown = 15;
                break;
            case (Ability.BurnStatus):
                basicAttack.GetHasBurnStatus = true;
                break;
            case (Ability.ReducedAutoAttack):
                basicAttack.GetAutoAttackTime -= 1.0f;
                break;
            case (Ability.SlowStatus):
                basicAttack.GetHasSlowStatus = true;
                break;
            case (Ability.Tenacity):
                skill.GetStatusEffectPotency = 30;
                skill.GetSkillDescription = "Increases strength by 30%";
                break;
            case (Ability.ManaPulse):
                items.GetUnlockedPassive = true;
                break;
            case (Ability.StrengthIntelligenceReverse):
                basicAttack.GetUsesIntelligenceForDamage = true;
                break;
            case (Ability.Alleviate):
                skill.GetAlleviateHealPercentage = 30;
                skill.GetSkillDescription = "Removes all status ailments and restores HP by 30%.";
                break;
            case (Ability.HpForSkillCast):
                SkillsManager.Instance.GetUsesHpForSkillCast = true;
                break;
            case (Ability.WhirlwindSlash):
                skill.GetGainedPassive = true;
                skill.GetEnemyStatusEffect = StatusEffect.DamageOverTime;
                skill.GetStatusEffectName = "Wind Scarred";
                skill.GetStatusDescription = "Taking damage over time.";
                break;
            case (Ability.EvilsEnd):
                skill.GetStatusEffectPotency = 40;
                skill.GetSkillDescription = "Delivers a punishing blow to the target. <#EFDFB8>Can only be executed while the target is at 40% HP or below.</color>";
                break;
            case (Ability.CriticalChanceIncrease):
                character.GetCriticalChance = 15;
                break;
            case (Ability.BraveLight):
                skill.GetCoolDown = 45;
                skill.GetCoolDownImage.fillAmount = skill.GetElapsedCoolDown;
                break;
            case (Ability.Contracts):
                ContractSkills[0].GetPlayerStatusEffect = EffectStatus.ContractWithEvilNoNegative;
                ContractSkills[1].GetPlayerStatusEffect = EffectStatus.ContractWithTheVileNoNegative;
                ContractSkills[2].GetPlayerStatusEffect = EffectStatus.ContractWithNefariousnessNoNegative;
                ContractSkills[0].GetSkillDescription = "Increases Intelligence by 15%.";
                ContractSkills[1].GetSkillDescription = "Restores 3% MP over 3 seconds.";
                ContractSkills[2].GetSkillDescription = "Reduces the casting time of all skills by 25%.";
                ContractSkills[0].GetStatusDescription = "Increased Intelligence";
                ContractSkills[1].GetStatusDescription = "Restoring MP";
                ContractSkills[2].GetStatusDescription = "Skill cast and cost reduced.";
                ApplyContractEffects();
                break;
            case (Ability.ExtraContract):
                SkillsManager.Instance.GetMaxContractStack++;
                break;
            case (Ability.MiasmaPulse):
                items.GetUnlockedPassive = true;
                items.GetStatusEffect = true;
                break;
            case (Ability.NetherStar):
                skill.GetPotency += 200;
                break;
            case (Ability.IgnoreDefense):
                basicAttack.GetIgnoreDefense = true;
                break;
        }
    }

    private void LoseEquipmentAbility()
    {
        switch (equipmentAbility)
        {
            case (Ability.NONE):
                return;
            case (Ability.SwiftStrike):
                skill.GetManaCost = skill.GetSinisterPossessionManaCost;
                break;
            case (Ability.StormThrust):
                skill.GetEnemyStatusEffect = StatusEffect.DefenseDOWN;
                skill.GetStatusEffectName = "Defense Down";
                skill.GetStatusDescription = "Lowered Defense.";
                skill.GetAddedEffect = "Lowered defense";
                skill.GetStatusDuration = 15.0f;
                skill.GetCoolDown = 3;
                break;
            case (Ability.BurnStatus):
                basicAttack.GetHasBurnStatus = false;
                break;
            case (Ability.ReducedAutoAttack):
                basicAttack.GetAutoAttackTime += 1.0f;
                break;
            case (Ability.SlowStatus):
                basicAttack.GetHasSlowStatus = false;
                break;
            case (Ability.Tenacity):
                skill.GetStatusEffectPotency = 10;
                skill.GetSkillDescription = "Increases strength by 10%";
                break;
            case (Ability.ManaPulse):
                items.GetUnlockedPassive = false;
                break;
            case (Ability.StrengthIntelligenceReverse):
                basicAttack.GetUsesIntelligenceForDamage = false;
                break;
            case (Ability.Alleviate):
                skill.GetAlleviateHealPercentage = 20;
                skill.GetSkillDescription = "Removes all status ailments and restores HP by 20%.";
                break;
            case (Ability.HpForSkillCast):
                SkillsManager.Instance.GetUsesHpForSkillCast = false;
                break;
            case (Ability.WhirlwindSlash):
                skill.GetGainedPassive = false;
                skill.GetEnemyStatusEffect = StatusEffect.NONE;
                break;
            case (Ability.EvilsEnd):
                skill.GetStatusEffectPotency = 25;
                skill.GetSkillDescription = "Delivers a punishing blow to the target. <#EFDFB8>Can only be executed while the target is at 25% HP or below.</color>";
                break;
            case (Ability.CriticalChanceIncrease):
                character.GetCriticalChance = 5;
                break;
            case (Ability.BraveLight):
                skill.GetCoolDown = 90;
                skill.GetCoolDownImage.fillAmount = skill.GetElapsedCoolDown;
                break;
            case (Ability.Contracts):
                ContractSkills[0].GetPlayerStatusEffect = EffectStatus.ContractWithEvil;
                ContractSkills[1].GetPlayerStatusEffect = EffectStatus.ContractWithTheVile;
                ContractSkills[2].GetPlayerStatusEffect = EffectStatus.ContractWithNefariousness;
                ContractSkills[0].GetSkillDescription = "Increases intelligence by 15% and decreases defense by 15%.";
                ContractSkills[1].GetSkillDescription = "Restores 3% MP over 3 seconds and reduces HP by 1% over 5 seconds.";
                ContractSkills[2].GetSkillDescription = "Reduces the casting time of all skills by 25% and reduces maximum HP by 25%.";
                ContractSkills[0].GetStatusDescription = "Increased Intelligence, Decreased Defense";
                ContractSkills[1].GetStatusDescription = "Restoring MP, Taking Damage";
                ContractSkills[2].GetStatusDescription = "Skill cast and cost reduced, HP halved";
                RemoveContracts();
                break;
            case (Ability.ExtraContract):
                SkillsManager.Instance.GetMaxContractStack--;
                if (SkillsManager.Instance.GetContractStack >= SkillsManager.Instance.GetMaxContractStack)
                {
                    SkillsManager.Instance.GetContractSkill.GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();
                    SkillsManager.Instance.GetContractStack--;
                }
                break;
            case (Ability.MiasmaPulse):
                items.GetUnlockedPassive = false;
                items.GetStatusEffect = false;
                break;
            case (Ability.NetherStar):
                skill.GetPotency -= 200;
                break;
            case (Ability.IgnoreDefense):
                basicAttack.GetIgnoreDefense = false;
                break;
        }
    }

    private void ApplyContractEffects()
    {
        if(ContractSkills[0].GetStatusIcon.activeInHierarchy)
        {
            ContractSkills[0].GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();
            SkillsManager.Instance.GetContractStack--;
        }
        if (ContractSkills[1].GetStatusIcon.activeInHierarchy)
        {
            ContractSkills[1].GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();
            SkillsManager.Instance.GetContractStack--;
        }
        if (ContractSkills[2].GetStatusIcon.activeInHierarchy)
        {
            ContractSkills[2].GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();
            SkillsManager.Instance.GetContractStack--;
        }
    }

    private void RemoveContracts()
    {
        if (ContractSkills[0].GetStatusIcon.activeInHierarchy)
        {
            ContractSkills[0].GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();
            SkillsManager.Instance.GetContractStack--;
        }
        if (ContractSkills[1].GetStatusIcon.activeInHierarchy)
        {
            ContractSkills[1].GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();
            SkillsManager.Instance.GetContractStack--;
        }
        if (ContractSkills[2].GetStatusIcon.activeInHierarchy)
        {
            ContractSkills[2].GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();
            SkillsManager.Instance.GetContractStack--;
        }
    }

    public void ShowEquipmentStatInfo()
    {
        if (gameObject.transform.parent.GetComponent<EquipmentCheck>())
        {
            ShopEquipmentPanelText.text = EquipmentStatsInShopMenu().text;
        }   
    }

    public void HideEquipmentStatInfo()
    {
        if (gameObject.transform.parent.GetComponent<EquipmentCheck>())
        {
            ShopEquipmentPanelText.text = "";
        }
    }

    public void Buy()
    {
        if(gameObject.transform.parent.GetComponent<EquipmentCheck>())
        {
            if(inventory.GetCoins >= equipmentData.BuyValue)
            {
                if(equipmentType == EquipmentType.Weapon)
                {
                    if(gameObject.GetComponent<DragUiObject>().GetMenuParent.childCount >= GameManager.Instance.GetEquipmentMenu.GetMaxWeapons)
                    {
                        GameManager.Instance.MaxWeaponsReachedText();
                    }
                    else
                    {
                        Create(gameObject);
                        inventory.AddCoins(-equipmentData.BuyValue);

                        GameManager.Instance.GetShop.UpdateCoins();
                        SoundManager.Instance.BuyItem();
                    }
                }
                if (equipmentType == EquipmentType.Armor)
                {
                    if (gameObject.GetComponent<DragUiObject>().GetMenuParent.childCount >= GameManager.Instance.GetEquipmentMenu.GetMaxArmor)
                    {
                        GameManager.Instance.MaxArmorReachedText();
                    }
                    else
                    {
                        Create(gameObject);
                        inventory.AddCoins(-equipmentData.BuyValue);

                        GameManager.Instance.GetShop.UpdateCoins();
                        SoundManager.Instance.BuyItem();
                    }
                }
            }
            else
            {
                GameManager.Instance.NotEnoughCoinsText();
            }
        }
    }

    private void Create(GameObject obj)
    {
        var CreatedObj = Instantiate(obj);
        switch(equipmentType)
        {
            case (EquipmentType.Weapon):
                CreatedObj.GetComponent<DragUiObject>().enabled = true;
                CreatedObj.transform.SetParent(GameManager.Instance.GetEquipmentMenu.GetWeaponPanel.transform, false);
                if (GameManager.Instance.GetEquipmentMenu.GetIsOpened)
                {
                    CreatedObj.SetActive(true);
                }
                else
                {
                    CreatedObj.SetActive(false);
                }
                break;
            case (EquipmentType.Armor):
                CreatedObj.GetComponent<DragUiObject>().enabled = true;
                CreatedObj.transform.SetParent(GameManager.Instance.GetEquipmentMenu.GetArmorPanel.transform, false);
                if (GameManager.Instance.GetEquipmentMenu.GetIsOpened)
                {
                    CreatedObj.SetActive(true);
                }
                else
                {
                    CreatedObj.SetActive(false);
                }
                break;
        }
    }

    private int SellValue()
    {
        int value = equipmentData.BuyValue / 2;

        Mathf.Round(value);

        return value;
    }

    public void ReceiveCoins()
    {
        int value = equipmentData.BuyValue / 2;

        Mathf.Round(value);

        inventory.AddCoins(value);

        shop.UpdateCoins();
    }
}
