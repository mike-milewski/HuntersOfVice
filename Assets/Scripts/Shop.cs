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
    private ShopData shopData;

    [SerializeField]
    private Animator ShopLevelAnim;

    [SerializeField]
    private Transform WeaponTransform, ArmorTransform;

    [SerializeField]
    private Image FillArea, FillAreaTwo, EquipmentImage;

    [SerializeField]
    private TextMeshProUGUI ShopLevelText, UpgradeShopLevelText, ShopExperienceText, CoinAmountText, DiscountText, EquipmentRewardInfoText;

    [SerializeField]
    private int ExperiencePoints, NextToLevel;

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
        ShopLevelText.text = "Level: " + shopData.ShopLevel;
        UpgradeShopLevelText.text = "Level: " + shopData.ShopLevel;

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

        ShopExperienceText.text = "Next: " + NextToLevel;
    }

    private void UpdateShopExperience()
    {
        FillArea.fillAmount = ExperiencePoints / NextToLevel;

        ShopExperienceText.text = "Next: " + NextToLevel;
    }

    private void GetReward()
    {
        switch(shopLevelRewards[shopData.ShopLevel - 1].GetShopRewards)
        {
            case (ShopRewards.Discount):
                EquipmentDiscount();
                break;
            case (ShopRewards.equipment):
                if(shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentType == EquipmentType.Weapon)
                {
                    shopLevelRewards[shopData.ShopLevel - 1].GetEquip.transform.SetParent(WeaponTransform);
                }
                else
                {
                    shopLevelRewards[shopData.ShopLevel - 1].GetEquip.transform.SetParent(ArmorTransform);
                }
                break;
        }
    }

    private void ShowNextReward()
    {
        switch(shopLevelRewards[shopData.ShopLevel - 1].GetShopRewards)
        {
            case (ShopRewards.Discount):
                DiscountText.gameObject.SetActive(true);
                EquipmentImage.gameObject.SetActive(false);
                DiscountText.text = "Item discount -" + shopLevelRewards[shopData.ShopLevel - 1].GetDiscountAmount + "%";
                break;
            case (ShopRewards.equipment):
                EquipmentImage.gameObject.SetActive(true);
                DiscountText.gameObject.SetActive(false);
                EquipmentImage.sprite = shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentSprite;
                break;
        }
    }

    private void EquipmentDiscount()
    {
        foreach(Equipment equip in WeaponTransform.GetComponentsInChildren<Equipment>(true))
        {
            equip.GetEquipmentData.BuyValue -= shopLevelRewards[shopData.ShopLevel - 1].GetDiscountAmount;
        }
        foreach (Equipment equip in ArmorTransform.GetComponentsInChildren<Equipment>(true))
        {
            equip.GetEquipmentData.BuyValue -= shopLevelRewards[shopData.ShopLevel - 1].GetDiscountAmount;
        }
    }

    private void LevelUp()
    {
        GetReward();

        shopData.ShopLevel++;

        int SurplusExperience = Mathf.Abs(shopData.ExperiencePoints - shopData.NextToLevel);

        int NextShopLevel = shopData.NextToLevel * (int)1.25f;

        Mathf.Round(NextShopLevel);

        shopData.ExperiencePoints = SurplusExperience;

        ShopLevelText.text = "Level: " + shopData.ShopLevel;
        UpgradeShopLevelText.text = "Level: " + shopData.ShopLevel;

        ShopLevelAnim.SetBool("Level", true);

        UpdateShopExperience();

        ShowNextReward();
    }

    public void ShowEquipmentRewardInfo()
    {
        if(shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType.Length == 1)
        {
            if(shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            "Element: " + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatIncrease;
            }
        }
        if (shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType.Length == 2)
        {
            if(shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            "Element: " + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatIncrease;
            }
        }
        if (shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType.Length == 3)
        {
            if(shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            "Element: " + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[2].GetStatIncrease;
            }
            
        }
        if (shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType.Length == 4)
        {
            if(shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[3].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[3].GetStatIncrease + "\n" +
                                            "Element: " + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[3].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[3].GetStatIncrease;
            }
            
        }
        if (shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType.Length == 5)
        {
            if(shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[3].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[3].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[4].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[4].GetStatIncrease + "\n" +
                                            "Element: " + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[3].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[3].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[4].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquip.GetStatusType[4].GetStatIncrease;
            }
            
        }
    }
}
