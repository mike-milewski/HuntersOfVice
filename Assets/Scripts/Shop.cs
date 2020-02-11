#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ShopRewards { Discount, equipment }

[System.Serializable]
public class ShopLevelRewards
{
    [SerializeField]
    private ShopRewards shopRewards;

    [SerializeField]
    private Equipment equip = null;

    [SerializeField]
    private int DiscountAmount;

    public ShopRewards GetShopRewards
    {
        get
        {
            return shopRewards;
        }
        set
        {
            shopRewards = value;
        }
    }

    public Equipment GetEquip
    {
        get
        {
            return equip;
        }
        set
        {
            equip = value;
        }
    }

    public int GetDiscountAmount
    {
        get
        {
            return DiscountAmount;
        }
        set
        {
            DiscountAmount = value;
        }
    }
}

public class Shop : MonoBehaviour
{
    [SerializeField]
    private ShopLevelRewards[] shopLevelRewards;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private GameObject LevelUpObject;

    [SerializeField]
    private Transform WeaponTransform, ArmorTransform, LevelUpObjectTransform;

    [SerializeField]
    private Image FillArea, FillAreaTwo, EquipmentImage;

    [SerializeField]
    private TextMeshProUGUI ShopLevelText, UpgradeShopLevelText, ShopExperienceText, CoinAmountText, DiscountText, EquipmentRewardInfoText;

    [SerializeField]
    private int ShopLevel, ExperiencePoints, NextToLevel;

    private int NTL;

    public int GetExperiencePoints
    {
        get
        {
            return ExperiencePoints;
        }
        set
        {
            ExperiencePoints = value;
        }
    }

    public int GetNTL
    {
        get
        {
            return NTL;
        }
        set
        {
            NTL = value;
        }
    }

    public int GetNextToLevel
    {
        get
        {
            return NextToLevel;
        }
        set
        {
            NextToLevel = value;
        }
    }

    private void Awake()
    {
        ShopLevelText.text = "Level: " + ShopLevel;
        UpgradeShopLevelText.text = "Level: " + ShopLevel;

        UpdateCoins();

        UpdateShopExperience();

        ShowNextReward();
    }

    public void UpdateCoins()
    {
        CoinAmountText.text = inventory.GetCoins.ToString();
    }

    public void GainExperience()
    {
        UpdateShopExperience();
    }

    public void ShowPreviewExperience()
    {
        UpdatePreviewShopExperience();
    }

    private void UpdatePreviewShopExperience()
    {
        FillAreaTwo.fillAmount = (float)ExperiencePoints / (float)NextToLevel;

        ShopExperienceText.text = "Next: " + Mathf.Abs(NTL);
    }

    private void UpdateShopExperience()
    {
        FillArea.fillAmount = (float)ExperiencePoints / (float)NextToLevel;
        FillAreaTwo.fillAmount = (float)ExperiencePoints / (float)NextToLevel;

        if ((float)ExperiencePoints >= (float)NextToLevel)
        {
            LevelUp();
        }

        NTL = Mathf.Abs(ExperiencePoints - NextToLevel);

        ShopExperienceText.text = "Next: " + Mathf.Abs(NTL);
    }

    private void GetReward()
    {
        switch(shopLevelRewards[ShopLevel].GetShopRewards)
        {
            case (ShopRewards.Discount):
                EquipmentDiscount();
                break;
            case (ShopRewards.equipment):
                if(shopLevelRewards[ShopLevel].GetEquip.GetEquipmentType == EquipmentType.Weapon)
                {
                    shopLevelRewards[ShopLevel].GetEquip.GetComponent<DragUiObject>().enabled = false;
                    shopLevelRewards[ShopLevel].GetEquip.transform.SetParent(WeaponTransform, false);
                }
                else
                {
                    shopLevelRewards[ShopLevel].GetEquip.GetComponent<DragUiObject>().enabled = false;
                    shopLevelRewards[ShopLevel].GetEquip.transform.SetParent(ArmorTransform, false);
                }
                break;
        }
    }

    private void ShowNextReward()
    {
        switch(shopLevelRewards[ShopLevel].GetShopRewards)
        {
            case (ShopRewards.Discount):
                DiscountText.gameObject.SetActive(true);
                EquipmentImage.gameObject.SetActive(false);
                DiscountText.text = "Item discount -" + shopLevelRewards[ShopLevel].GetDiscountAmount + "%";
                break;
            case (ShopRewards.equipment):
                EquipmentImage.gameObject.SetActive(true);
                DiscountText.gameObject.SetActive(false);
                EquipmentImage.sprite = shopLevelRewards[ShopLevel].GetEquip.GetEquipmentSprite;
                break;
        }
    }

    private void EquipmentDiscount()
    {
        foreach(Equipment equip in WeaponTransform.GetComponentsInChildren<Equipment>(true))
        {
            equip.GetEquipmentData.BuyValue -= shopLevelRewards[ShopLevel].GetDiscountAmount;
        }
        foreach (Equipment equip in ArmorTransform.GetComponentsInChildren<Equipment>(true))
        {
            equip.GetEquipmentData.BuyValue -= shopLevelRewards[ShopLevel].GetDiscountAmount;
        }
    }

    private void LevelUp()
    {
        GetReward();

        ShopLevel++;

        int SurplusExperience = Mathf.Abs(ExperiencePoints - NextToLevel);

        ExperiencePoints = SurplusExperience;

        int NextShopLevel = NextToLevel * 3;

        Mathf.Round(NextShopLevel);

        NextToLevel = NextShopLevel;

        ShopLevelText.text = "Level: " + ShopLevel;
        UpgradeShopLevelText.text = "Level: " + ShopLevel;

        Instantiate(LevelUpObject, LevelUpObjectTransform);

        UpdateShopExperience();

        ShowNextReward();
    }

    public void ShowEquipmentRewardInfo()
    {
        if(shopLevelRewards[ShopLevel].GetEquip.GetStatusType.Length == 1)
        {
            if(shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            "Element: " + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease;
            }
        }
        if (shopLevelRewards[ShopLevel].GetEquip.GetStatusType.Length == 2)
        {
            if(shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            "Element: " + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease;
            }
        }
        if (shopLevelRewards[ShopLevel].GetEquip.GetStatusType.Length == 3)
        {
            if(shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            "Element: " + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatIncrease;
            }
            
        }
        if (shopLevelRewards[ShopLevel].GetEquip.GetStatusType.Length == 4)
        {
            if(shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatIncrease + "\n" +
                                            "Element: " + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatIncrease;
            }
            
        }
        if (shopLevelRewards[ShopLevel].GetEquip.GetStatusType.Length == 5)
        {
            if(shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[4].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[4].GetStatIncrease + "\n" +
                                            "Element: " + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatIncrease + "\n" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[4].GetStatusTypes + " +" +
                                            shopLevelRewards[ShopLevel].GetEquip.GetStatusType[4].GetStatIncrease;
            }
            
        }
    }
}
