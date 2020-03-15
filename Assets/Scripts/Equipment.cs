#pragma warning disable 0649
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
    private Inventory inventory;

    [SerializeField]
    private Shop shop;

    [SerializeField]
    private Sprite EquipmentSprite;

    [SerializeField]
    private TextMeshProUGUI EquipmentNameText, EquipmentInfoText, EquipmentPanelText, ShopEquipmentPanelText;

    [SerializeField]
    private EquipmentType equipmentType;

    [SerializeField]
    private StatusType[] stattype;

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

        if(equipmentData.Element != PlayerElement.NONE)
        {
            EquipmentInfoText.text = StatsText().text + "\n" + equipmentData.Element;
        }
    }

    private void CurrentArmorEquippedText()
    {
        EquipmentNameText.text = equipmentData.EquipmentName;

        EquipmentInfoText.text = StatsText().text;
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
                                        "\n\n" + "Sell Value: " + SellValue();
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease +
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
                                                        "\n\n" + "Sell Value: " + SellValue();
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease +
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
                                       "\n\n" + "Sell Value: " + SellValue();
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                       stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                       stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                       stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease +
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
                                                        "\n\n" + "Sell Value: " + SellValue();
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease +
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
                                                        "\n\n" + "Sell Value: " + SellValue();
            }
            else
            {
                EquipmentPanelText.text = "<size=12>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" +
                                        stattype[4].GetStatusTypes + " +" + stattype[4].GetStatIncrease +
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
                                        "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease +
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
                                                        "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease +
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
                                       stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" + "Element: " + equipmentData.Element +
                                       "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                       stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                       stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                       stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease +
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
                                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" + "Element: " + equipmentData.Element
                                                        + "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease +
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
                                                        "\n\n" + "Buy Value: " + equipmentData.BuyValue;
            }
            else
            {
                EquipmentPanelText.text = "<size=18>" + "<u>" + equipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                        stattype[0].GetStatusTypes + " +" + stattype[0].GetStatIncrease + "\n" +
                                        stattype[1].GetStatusTypes + " +" + stattype[1].GetStatIncrease + "\n" +
                                        stattype[2].GetStatusTypes + " +" + stattype[2].GetStatIncrease + "\n" +
                                        stattype[3].GetStatusTypes + " +" + stattype[3].GetStatIncrease + "\n" +
                                        stattype[4].GetStatusTypes + " +" + stattype[4].GetStatIncrease +
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
                Create(gameObject);
                inventory.AddCoins(-equipmentData.BuyValue);
                GameManager.Instance.GetShop.UpdateCoins();
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
