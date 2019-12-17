using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum EquipmentType { Weapon, Armor }

public enum StatIncreaseType { HP, MP, Strength, Defense, Intelligence }

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
    private Sprite EquipmentSprite;

    [SerializeField]
    private TextMeshProUGUI EquipmentNameText, EquipmentInfoText, EquipmentPanelText;

    [SerializeField]
    private EquipmentType equipmentType;

    [SerializeField]
    private StatusType[] stattype;

    [SerializeField]
    private int BuyValue;

    private int SellValue;

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

    public int GetBuyValue
    {
        get
        {
            return BuyValue;
        }
        set
        {
            BuyValue = value;
        }
    }

    public int GetSellValue
    {
        get
        {
            return SellValue;
        }
        set
        {
            SellValue = value;
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
        switch(equipmentType)
        {
            case (EquipmentType.Weapon):
                basicAttack.GetEquipment[0] = null;
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

        CurrentWeaponEquippedText();
    }

    private void ArmorEquip()
    {
        IncreaseStats();

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
                    character.GetComponent<Health>().GetFilledBar();
                    break;
                case (StatIncreaseType.MP):
                    character.MaxMana -= stattype[i].GetStatIncrease;
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

    private void CurrentWeaponEquippedText()
    {
        EquipmentNameText.text = equipmentData.EquipmentName;

        EquipmentInfoText.text = StatsText().text;
    }

    private void CurrentArmorEquippedText()
    {
        EquipmentNameText.text = equipmentData.EquipmentName;

        EquipmentInfoText.text = StatsText().text;
    }

    public void PanelText(GameObject panel)
    {
        panel.SetActive(true);

        if (stattype.Length == 1)
        {
            EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n";
        }
        if (stattype.Length == 2)
        {
            EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease;
        }
        if (stattype.Length == 3)
        {
            EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                       stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                       stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                       stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease;
        }
        if (stattype.Length == 4)
        {
            EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease;
        }
        if(stattype.Length == 5)
        {
            EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" +
                                        stattype[4].GetStatusTypes + " +" + stattype[4].GetStatIncrease;
        }
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
}
