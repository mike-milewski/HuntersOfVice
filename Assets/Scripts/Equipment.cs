#pragma warning disable 0649, 0414
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EquipmentType { Weapon, Armor }

public enum StatIncreaseType { HP, MP, Strength, Defense, Intelligence }

public enum Ability { NONE, SwiftStrike, StormThrust, BurnStatus, ReducedAutoAttack, SlowStatus, Tenacity, ManaPulse, StrengthIntelligenceReverse, Alleviate, 
                      HpForSkillCast, WhirlwindSlash, EvilsEnd, CriticalChanceIncrease, ExtraContract, BraveLight, Contracts, MiasmaPulse, NetherStar, IgnoreDefense,
                      MyceliumBash, IronCap, HpHeal, Quickness, ImmuneToStatusEffects, DoubleStatusDuration, MpHeal, MildewSplash, SpinShroom, ViciousEmbodiment,
                      ToadstoolReflectDamage, ReducedDamage, StrengthDown, IntelligenceDown, DefenseDown, SoulPierce, ExtraSkillPoints }

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

    private GameObject statusicon = null;

    [SerializeField]
    private Shop shop;

    [SerializeField]
    private Sprite EquipmentSprite;

    [SerializeField]
    private TextMeshProUGUI EquipmentNameText, EquipmentInfoText, EquipmentPanelText, ShopEquipmentPanelText, SkillPointText;

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

    [SerializeField]
    private int SkillPointsRequired;

    private int SkillPointsAcquired;

    private bool SkillMastered;

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

    public int GetRequiredSkillPoints
    {
        get
        {
            return SkillPointsRequired;
        }
        set
        {
            SkillPointsRequired = value;
        }
    }

    public int GetAcquiredSkillPoints
    {
        get
        {
            return SkillPointsAcquired;
        }
        set
        {
            SkillPointsAcquired = value;
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
                if(character.GetCharacterData.name == "Knight" || character.GetCharacterData.name == "Toadstool")
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
        SkillPointText.text = "";
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

        CheckSkillPointText();

        if(equipmentData.Element != PlayerElement.NONE)
        {
            EquipmentInfoText.text = StatsText().text + "\n" + "Element: " + equipmentData.Element + EquipmentAbilityTextInEquipMenu();
        }
    }

    private void CurrentArmorEquippedText()
    {
        EquipmentNameText.text = equipmentData.EquipmentName;

        CheckSkillPointText();

        EquipmentInfoText.text = StatsText().text + EquipmentAbilityTextInEquipMenu();
    }

    public TextMeshProUGUI CheckSkillPointText()
    {
        if (SkillPointsRequired > 0)
        {
            if (SkillPointsAcquired >= SkillPointsRequired && SkillPointsRequired != 0)
            {
                SkillPointText.text = "MASTERED";
            }
            else
            {
                SkillPointText.text = "Skill Points: " + SkillPointsAcquired + " / " + SkillPointsRequired;
            }
        }
        return SkillPointText;
    }

    public string SkillPointTextInMenu()
    {
        string skillPoints = "";

        if (SkillPointsRequired > 0)
        {
            skillPoints = "Skill Points: " + SkillPointsAcquired + " / " + SkillPointsRequired;
            if (SkillPointsAcquired >= SkillPointsRequired && SkillPointsRequired != 0)
            {
                skillPoints = "MASTERED";
                SkillMastered = true;
            }
        }

        return skillPoints;
    }

    public void UpdateSkillPoinText()
    {
        if(SkillMastered)
        {
            SkillPointText.text = "MASTERED";
        }
        if (SkillPointsAcquired >= SkillPointsRequired && SkillPointsRequired != 0 && !SkillMastered)
        {
            SkillsManager.Instance.CreateSkillMasteryText(EquipmentAbilityTextInEquipMenu());
            GameManager.Instance.ShowSkillMasteryText();
            SkillPointText.text = "MASTERED";
        }
        if(SkillPointsAcquired < SkillPointsRequired && SkillPointsRequired != 0)
        {
            SkillPointText.text = "Skill Points: " + SkillPointsAcquired + " / " + SkillPointsRequired;
        }
    }

    public string SkillPointRequirementInShopMenu()
    {
        string skillPointRequirement = "";

        if (SkillPointsRequired > 0)
        {
            skillPointRequirement = "Required Skill Points: " + SkillPointsRequired;
        }

        return skillPointRequirement;
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
                                        EquipmentAbilityText() + "\n\n" + "Sell Value: " + SellValue() + "\n\n" + SkillPointTextInMenu(); ;
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Sell Value: " + SellValue() + "\n\n" + SkillPointTextInMenu(); ;
            }
        }
        if (stattype.Length == 2)
        {
            if (equipmentData.Element != PlayerElement.NONE)
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" + "Element: " + equipmentData.Element +
                                                        EquipmentAbilityText() + "\n\n" + "Sell Value: " + SellValue() + "\n\n" + SkillPointTextInMenu(); ;
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Sell Value: " + SellValue() + "\n\n" + SkillPointTextInMenu(); ;
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
                                       EquipmentAbilityText() + "\n\n" + "Sell Value: " + SellValue() + "\n\n" + SkillPointTextInMenu(); ;
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                       stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                       stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                       stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + EquipmentAbilityText() +
                                       "\n\n" + "Sell Value: " + SellValue() + "\n\n" + SkillPointTextInMenu(); ;
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
                                                        EquipmentAbilityText() + "\n\n" + "Sell Value: " + SellValue() + "\n\n" + SkillPointTextInMenu(); ;
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Sell Value: " + SellValue() + "\n\n" + SkillPointTextInMenu(); ;
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
                                                        EquipmentAbilityText() + "\n\n" + "Sell Value: " + SellValue() + "\n\n" + SkillPointTextInMenu(); ;
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" +
                                        stattype[4].GetStatusTypes + " +" + stattype[4].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Sell Value: " + SellValue() + "\n\n" + SkillPointTextInMenu(); ;
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
                                        EquipmentAbilityText() + "\n\n" + "Buy Value: " + equipmentData.BuyValue + "\n\n" + SkillPointRequirementInShopMenu();
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Buy Value: " + equipmentData.BuyValue + "\n\n" + SkillPointRequirementInShopMenu();
            }
        }
        if (stattype.Length == 2)
        {
            if (equipmentData.Element != PlayerElement.NONE)
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" + "Element: " + equipmentData.Element +
                                                        EquipmentAbilityText() + "\n\n" + "Buy Value: " + equipmentData.BuyValue + "\n\n" + SkillPointRequirementInShopMenu();
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Buy Value: " + equipmentData.BuyValue + "\n\n" + SkillPointRequirementInShopMenu();
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
                                       "\n\n" + "Buy Value: " + equipmentData.BuyValue + "\n\n" + SkillPointRequirementInShopMenu();
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                       stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                       stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                       stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + EquipmentAbilityText() +
                                       "\n\n" + "Buy Value: " + equipmentData.BuyValue + "\n\n" + SkillPointRequirementInShopMenu();
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
                                                        EquipmentAbilityText() + "\n\n" + "Buy Value: " + equipmentData.BuyValue + "\n\n" + SkillPointRequirementInShopMenu();
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Buy Value: " + equipmentData.BuyValue + "\n\n" + SkillPointRequirementInShopMenu();
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
                                                        EquipmentAbilityText() + "\n\n" + "Buy Value: " + equipmentData.BuyValue + "\n\n" + SkillPointRequirementInShopMenu();
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" +
                                        stattype[4].GetStatusTypes + " +" + stattype[4].GetStatIncrease + EquipmentAbilityText() +
                                        "\n\n" + "Buy Value: " + equipmentData.BuyValue + "\n\n" + SkillPointRequirementInShopMenu();
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
                skillText = "\n\n" + "<#EFDFB8>" + "Attacks - 10% Burning status." + "</color> ";
                break;
            case (Ability.ReducedAutoAttack):
                skillText = "\n\n" + "<#EFDFB8>" + "Reduces auto-attack time by 1 second." + "</color> ";
                break;
            case (Ability.SlowStatus):
                skillText = "\n\n" + "<#EFDFB8>" + "Attacks - 10% Slowed status." + "</color> ";
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
            case (Ability.MyceliumBash):
                skillText = "\n\n" + "<#EFDFB8>" + "Mycelium Bash - 2 hits & 2 MP cost." + "</color> ";
                break;
            case (Ability.IronCap):
                skillText = "\n\n" + "<#EFDFB8>" + "Iron Cap - Increased duration." + "</color> ";
                break;
            case (Ability.HpHeal):
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack - 2% HP heal." + "</color> ";
                break;
            case (Ability.Quickness):
                skillText = "\n\n" + "<#EFDFB8>" + "Quickness - Removed duration." + "</color> ";
                break;
            case (Ability.ImmuneToStatusEffects):
                skillText = "\n\n" + "<#EFDFB8>" + "Immune to status ailments." + "</color> ";
                break;
            case (Ability.DoubleStatusDuration):
                skillText = "\n\n" + "<#EFDFB8>" + "Doubled status aliment duration." + "</color> ";
                break;
            case (Ability.MpHeal):
                skillText = "\n\n" + "<#EFDFB8>" + "Gradual MP recovery." + "</color> ";
                break;
            case (Ability.MildewSplash):
                skillText = "\n\n" + "<#EFDFB8>" + "Mildew Splash - Halved cooldown." + "</color> ";
                break;
            case (Ability.SpinShroom):
                skillText = "\n\n" + "<#EFDFB8>" + "Spinshroom - Increased power." + "</color> ";
                break;
            case (Ability.ToadstoolReflectDamage):
                skillText = "\n\n" + "<#EFDFB8>" + "5% of max HP reflected as damage. Bosses - 1% reflected." + "</color> ";
                break;
            case (Ability.ViciousEmbodiment):
                skillText = "\n\n" + "<#EFDFB8>" + "Status effects - 5% Doomed status." + "</color> ";
                break;
            case (Ability.ReducedDamage):
                skillText = "\n\n" + "<#EFDFB8>" + "Damage - 10% reduced." + "</color> ";
                break;
            case (Ability.SoulPierce):
                skillText = "\n\n" + "<#EFDFB8>" + "Withering - 20s duration." + "</color> ";
                break;
            case (Ability.StrengthDown):
                skillText = "\n\n" + "<#EFDFB8>" + "Attacks - 10% Strength Down." + "</color> ";
                break;
            case (Ability.DefenseDown):
                skillText = "\n\n" + "<#EFDFB8>" + "Attacks - 10% Defense Down." + "</color> ";
                break;
            case (Ability.IntelligenceDown):
                skillText = "\n\n" + "<#EFDFB8>" + "Attacks - 10% Intelligence Down." + "</color> ";
                break;
            case (Ability.ExtraSkillPoints):
                skillText = "\n\n" + "<#EFDFB8>" + "Skill Points earned +2." + "</color> ";
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
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack and damage skills have a 5% chance of inflicting the Burning status effect." + "</color> ";
                break;
            case (Ability.ReducedAutoAttack):
                skillText = "\n\n" + "<#EFDFB8>" + "Reduces auto-attack time by 1 second." + "</color> ";
                break;
            case (Ability.SlowStatus):
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack and damage skills have a 5% chance of inflicting the Slowed status effect." + "</color> ";
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
            case (Ability.MyceliumBash):
                skillText = "\n\n" + "<#EFDFB8>" + "Mycelium Bash deals damage twice but now costs 2 MP." + "</color> ";
                break;
            case (Ability.IronCap):
                skillText = "\n\n" + "<#EFDFB8>" + "Increases the duration of Iron Cap by 5 seconds." + "</color> ";
                break;
            case (Ability.HpHeal):
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack restores 2% HP." + "</color> ";
                break;
            case (Ability.Quickness):
                skillText = "\n\n" + "<#EFDFB8>" + "Removes the duration of Quickness." + "</color> ";
                break;
            case (Ability.ImmuneToStatusEffects):
                skillText = "\n\n" + "<#EFDFB8>" + "Nullifies status ailments." + "</color> ";
                break;
            case (Ability.DoubleStatusDuration):
                skillText = "\n\n" + "<#EFDFB8>" + "Doubles the duration of status ailments." + "</color> ";
                break;
            case (Ability.MpHeal):
                skillText = "\n\n" + "<#EFDFB8>" + "Gradually restores 1% of MP over 3 seconds." + "</color> ";
                break;
            case (Ability.MildewSplash):
                skillText = "\n\n" + "<#EFDFB8>" + "Reduces the cooldown of Mildew Splash by half." + "</color> ";
                break;
            case (Ability.SpinShroom):
                skillText = "\n\n" + "<#EFDFB8>" + "Increases the power of Spinshroom to 100." + "</color> ";
                break;
            case (Ability.ToadstoolReflectDamage):
                skillText = "\n\n" + "<#EFDFB8>" + "Reflects damage back to an enemy based on 5% of your maximum HP. Attacks from bosses reflect 1% damage." + "</color> ";
                break;
            case (Ability.ViciousEmbodiment):
                skillText = "\n\n" + "<#EFDFB8>" + "Inflicting status ailments has a 5% chance of inflicting the Doomed status effect instead." + "</color> ";
                break;
            case (Ability.ReducedDamage):
                skillText = "\n\n" + "<#EFDFB8>" + "Taking damage has a 15% chance to reduce that damage by 10%." + "</color> ";
                break;
            case (Ability.SoulPierce):
                skillText = "\n\n" + "<#EFDFB8>" + "Increases the duration of Soul Pierce's Withering status effect to 20 seconds." + "</color> ";
                break;
            case (Ability.StrengthDown):
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack and damage skills have a 5% chance of inflicting the Strength Down status effect." + "</color> ";
                break;
            case (Ability.DefenseDown):
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack and damage skills have a 5% chance of inflicting the Defense Down status effect." + "</color> ";
                break;
            case (Ability.IntelligenceDown):
                skillText = "\n\n" + "<#EFDFB8>" + "Auto-attack and damage skills have a 5% chance of inflicting the Intelligence Down status effect." + "</color> ";
                break;
            case (Ability.ExtraSkillPoints):
                skillText = "\n\n" + "<#EFDFB8>" + "Skill Points earned from enemies is increased by 2." + "</color>";
                break;
        }
        return skillText;
    }

    private void GainEquipmentAbility()
    {
        if(SkillMastered)
        {
            return;
        }
        else
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
                    basicAttack.GetAttackDelay -= 0.5f;
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
                    if (skill.GetCoolDownImage.fillAmount > 0)
                    {
                        skill.GetCoolDownImage.fillAmount = 1;
                        skill.GetCD = skill.GetCoolDown;
                    }
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
                case (Ability.MyceliumBash):
                    character.GetComponent<PlayerAnimations>().GetAdditionalHit = true;
                    skill.GetManaCost = 2;
                    break;
                case (Ability.IronCap):
                    skill.GetStatusDuration = 20;
                    break;
                case (Ability.HpHeal):
                    character.GetComponent<Health>().GetUnlockedPassive = true;
                    break;
                case (Ability.Quickness):
                    skill.GetStatusDuration = -1;
                    if (skill.GetStatusIcon.activeInHierarchy)
                    {
                        skill.GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();
                    }
                    break;
                case (Ability.ImmuneToStatusEffects):
                    character.GetIsImmuneToStatusEffects = true;
                    break;
                case (Ability.DoubleStatusDuration):
                    basicAttack.GetDoublesStatusDuration = true;
                    break;
                case (Ability.MpHeal):
                    RestoreMpEffect();
                    break;
                case (Ability.MildewSplash):
                    skill.GetCoolDown = 12.5f;
                    if (skill.GetCoolDownImage.fillAmount > 0)
                    {
                        skill.GetCoolDownImage.fillAmount = 1;
                        skill.GetCD = skill.GetCoolDown;
                    }
                    break;
                case (Ability.SpinShroom):
                    skill.GetPotency = 100;
                    break;
                case (Ability.ToadstoolReflectDamage):
                    character.GetComponent<Health>().GetReflectingDamage = true;
                    break;
                case (Ability.ViciousEmbodiment):
                    basicAttack.GetInflictsDoomStatus = true;
                    break;
                case (Ability.ReducedDamage):
                    character.GetComponent<Health>().GetReducedDamage = true;
                    break;
                case (Ability.DefenseDown):
                    basicAttack.GetHasDefenseDownStatus = true;
                    break;
                case (Ability.IntelligenceDown):
                    basicAttack.GetHasIntelligenceDownStatus = true;
                    break;
                case (Ability.StrengthDown):
                    basicAttack.GetHasStrengthDownStatus = true;
                    break;
                case (Ability.SoulPierce):
                    skill.GetStatusDuration = 20;
                    break;
                case (Ability.ExtraSkillPoints):
                    GameManager.Instance.GetEquipmentMenu.GetAdditionalSkillPoints = 2;
                    break;
            }
        }
    }

    private void LoseEquipmentAbility()
    {
        if(SkillMastered)
        {
            return;
        }
        else
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
                    basicAttack.GetAttackDelay += 0.5f;
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
                    if (skill.GetCoolDownImage.fillAmount > 0)
                    {
                        skill.GetCoolDownImage.fillAmount = 1;
                        skill.GetCD = skill.GetCoolDown;
                    }
                    break;
                case (Ability.Contracts):
                    ContractSkills[0].GetPlayerStatusEffect = EffectStatus.ContractWithEvil;
                    ContractSkills[1].GetPlayerStatusEffect = EffectStatus.ContractWithTheVile;
                    ContractSkills[2].GetPlayerStatusEffect = EffectStatus.ContractWithNefariousness;
                    ContractSkills[0].GetSkillDescription = "Increases intelligence by 15% and decreases defense by 15%.";
                    ContractSkills[1].GetSkillDescription = "Restores 3% MP over 3 seconds and reduces HP by 1% over 5 seconds.";
                    ContractSkills[2].GetSkillDescription = "Reduces the casting time of all skills by 25% and reduces maximum HP by 25%.";
                    ContractSkills[0].GetStatusDescription = "Increased Intelligence & Decreased Defense";
                    ContractSkills[1].GetStatusDescription = "Restoring MP & Taking Damage";
                    ContractSkills[2].GetStatusDescription = "Skill cast and cost reduced & HP halved";
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
                case (Ability.MyceliumBash):
                    character.GetComponent<PlayerAnimations>().GetAdditionalHit = false;
                    skill.GetManaCost = 0;
                    break;
                case (Ability.IronCap):
                    skill.GetStatusDuration = 15;
                    break;
                case (Ability.HpHeal):
                    character.GetComponent<Health>().GetUnlockedPassive = false;
                    break;
                case (Ability.Quickness):
                    skill.GetStatusDuration = 20;
                    if (skill.GetStatusIcon.activeInHierarchy)
                    {
                        skill.GetStatusIcon.GetComponent<StatusIcon>().RemoveEffect();
                    }
                    break;
                case (Ability.ImmuneToStatusEffects):
                    character.GetIsImmuneToStatusEffects = false;
                    break;
                case (Ability.DoubleStatusDuration):
                    basicAttack.GetDoublesStatusDuration = false;
                    break;
                case (Ability.MpHeal):
                    statusicon.GetComponent<StatusIcon>().RemoveMpRestoreStatus();
                    break;
                case (Ability.MildewSplash):
                    skill.GetCoolDown = 25;
                    if (skill.GetCoolDownImage.fillAmount > 0)
                    {
                        skill.GetCoolDownImage.fillAmount = 1;
                        skill.GetCD = skill.GetCoolDown;
                    }
                    break;
                case (Ability.SpinShroom):
                    skill.GetPotency = 65;
                    break;
                case (Ability.ToadstoolReflectDamage):
                    character.GetComponent<Health>().GetReflectingDamage = false;
                    break;
                case (Ability.ViciousEmbodiment):
                    basicAttack.GetInflictsDoomStatus = false;
                    break;
                case (Ability.ReducedDamage):
                    character.GetComponent<Health>().GetReducedDamage = false;
                    break;
                case (Ability.DefenseDown):
                    basicAttack.GetHasDefenseDownStatus = false;
                    break;
                case (Ability.IntelligenceDown):
                    basicAttack.GetHasIntelligenceDownStatus = false;
                    break;
                case (Ability.StrengthDown):
                    basicAttack.GetHasStrengthDownStatus = false;
                    break;
                case (Ability.SoulPierce):
                    skill.GetStatusDuration = 10;
                    break;
                case (Ability.ExtraSkillPoints):
                    GameManager.Instance.GetEquipmentMenu.GetAdditionalSkillPoints = 0;
                    break;
            }
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

    private void RestoreMpEffect()
    {
        var StatusTxt = ObjectPooler.Instance.GetPlayerStatusText();

        StatusTxt.SetActive(true);

        StatusTxt.transform.SetParent(skill.GetTextHolder.transform, false);

        StatusTxt.GetComponentInChildren<TextMeshProUGUI>().text = "<#5DFFB4>+ Mana Regen";

        StatusTxt.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetMpRestoreSprite;

        statusicon = ObjectPooler.Instance.GetPlayerStatusIcon();

        statusicon.SetActive(true);

        statusicon.transform.SetParent(skill.GetStatusEffectIconTrans, false);

        statusicon.GetComponent<StatusIcon>().GetEffectStatus = EffectStatus.MpRestore;

        statusicon.GetComponent<StatusIcon>().PlayerInput();

        statusicon.GetComponentInChildren<Image>().sprite = GameManager.Instance.GetMpRestoreSprite;

        statusicon.GetComponent<StatusIcon>().MpRestorePassive();
    }
}
