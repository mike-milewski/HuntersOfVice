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

    public Equipment GetEquipment
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
    private Transform WeaponTransform, ArmorTransform;

    [SerializeField]
    private Image FillArea, FillAreaTwo, EquipmentImage;

    [SerializeField]
    private TextMeshProUGUI ShopLevelText, UpgradeShopLevelText, ShopExperienceText, CoinAmountText, DiscountText, EquipmentRewardInfoText;

    private void Awake()
    {
        ShopLevelText.text = "Level: " + shopData.ShopLevel;
        UpgradeShopLevelText.text = "Level: " + shopData.ShopLevel;

        CoinAmountText.text = inventory.GetCoins.ToString();

        UpdateShopExperience();

        ShowNextReward();
    }

    public void Buy()
    {

    }

    public void Sell()
    {

    }

    public void Craft()
    {

    }

    public void GainExperience()
    {
        UpdateShopExperience();
    }

    private void UpdateShopExperience()
    {
        FillArea.fillAmount = shopData.ExperiencePoints / shopData.NextToLevel;

        ShopExperienceText.text = "Next: " + shopData.NextToLevel;
    }

    private void GetReward()
    {
        switch(shopLevelRewards[shopData.ShopLevel - 1].GetShopRewards)
        {
            case (ShopRewards.Discount):
                break;
            case (ShopRewards.equipment):
                if(shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetEquipmentType == EquipmentType.Weapon)
                {
                    shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.transform.SetParent(WeaponTransform);
                }
                else
                {
                    shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.transform.SetParent(ArmorTransform);
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
                EquipmentImage.sprite = shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetEquipmentSprite;
                break;
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

        UpdateShopExperience();

        ShowNextReward();
    }

    public void ShowEquipmentRewardInfo()
    {
        if(shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType.Length == 1)
        {
            EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[0].GetStatIncrease;
        }
        if (shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType.Length == 2)
        {
            EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[1].GetStatIncrease;
        }
        if (shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType.Length == 3)
        {
            EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[2].GetStatIncrease;
        }
        if (shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType.Length == 4)
        {
            EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[2].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[3].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[3].GetStatIncrease;
        }
        if (shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType.Length == 5)
        {
            EquipmentRewardInfoText.text = "<size=12>" + "<u>" + shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[0].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[0].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[1].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[1].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[2].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[2].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[3].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[3].GetStatIncrease + "\n" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[4].GetStatusTypes + " +" +
                                            shopLevelRewards[shopData.ShopLevel - 1].GetEquipment.GetStatusType[4].GetStatIncrease;
        }
    }
}
