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
    private Character Knight, ShadowPriest;

    [SerializeField]
    private ShopLevelRewards[] KnightShopLevelRewards;

    [SerializeField]
    private ShopLevelRewards[] ShadowPriestShopLevelRewards;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private GameObject LevelUpObject;

    [SerializeField]
    private Transform WeaponTransform, ArmorTransform, LevelUpObjectTransform, UpgradeTransform;

    [SerializeField]
    private Image FillArea, FillAreaTwo, EquipmentImage;

    [SerializeField]
    private TextMeshProUGUI ShopLevelText, UpgradeShopLevelText, ShopExperienceText, CoinAmountText, DiscountText, EquipmentRewardInfoText, PreviewShopLevelText,
                            PreviewShopExperienceText, NextLevelRewardText, PreviewNextLevelRewardText;

    [SerializeField]
    private int ShopLevel, ExperiencePoints, NextToLevel;

    [SerializeField]
    private int[] ShopLevelExperiences;

    private int NTL, ShopPreviewLevel;

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

        NextToLevel = ShopLevelExperiences[ShopLevel];

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

        while ((float)ExperiencePoints >= (float)NextToLevel)
        {
            PreviewLevelUp();
        }
        if ((float)ExperiencePoints < 0)
        {
            ShopPreviewLevel--;
            int shoplevel = Mathf.Abs(ShopLevelExperiences[ShopLevel + ShopPreviewLevel]);
            NextToLevel = shoplevel;

            ExperiencePoints = Mathf.Abs(ExperiencePoints + NextToLevel);

            ShowNextRewardPreview();

            if (ShopPreviewLevel <= 0)
            {
                PreviewShopLevelText.text = "";
                PreviewNextLevelRewardText.text = "";
                NextLevelRewardText.text = "Next Level";
            }
            else
            {
                PreviewShopLevelText.text = "+" + ShopPreviewLevel;
            }
            FillAreaTwo.fillAmount = (float)ExperiencePoints / (float)NextToLevel;
        }
        NTL = Mathf.Abs(ExperiencePoints - NextToLevel);

        if (UpgradeTransform.childCount <= 0)
        {
            PreviewShopExperienceText.text = "";
            ShopExperienceText.text = Mathf.Abs(NTL).ToString();
        }
        else
        {
            PreviewShopExperienceText.text = Mathf.Abs(NTL).ToString();
            ShopExperienceText.text = "";
        }
    }

    private void UpdateShopExperience()
    {
        FillArea.fillAmount = (float)ExperiencePoints / (float)NextToLevel;
        FillAreaTwo.fillAmount = (float)ExperiencePoints / (float)NextToLevel;

        PreviewShopLevelText.text = "";
        PreviewShopExperienceText.text = "";

        if (ShopPreviewLevel > 0)
        {
            Instantiate(LevelUpObject, LevelUpObjectTransform);
        }

        while (ShopPreviewLevel > 0)
        {
            LevelUp();
        }

        NTL = Mathf.Abs(ExperiencePoints - NextToLevel);

        ShopExperienceText.text = Mathf.Abs(NTL).ToString();
    }

    private void GetReward()
    {
        if(Knight.gameObject.activeInHierarchy)
        {
            switch (KnightShopLevelRewards[ShopLevel].GetShopRewards)
            {
                case (ShopRewards.Discount):
                    EquipmentDiscount();
                    break;
                case (ShopRewards.equipment):
                    if (KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentType == EquipmentType.Weapon)
                    {
                        KnightShopLevelRewards[ShopLevel].GetEquip.GetComponent<DragUiObject>().enabled = false;
                        KnightShopLevelRewards[ShopLevel].GetEquip.transform.SetParent(WeaponTransform, false);
                    }
                    else
                    {
                        KnightShopLevelRewards[ShopLevel].GetEquip.GetComponent<DragUiObject>().enabled = false;
                        KnightShopLevelRewards[ShopLevel].GetEquip.transform.SetParent(ArmorTransform, false);
                    }
                    break;
            }
        }
        else if(ShadowPriest.gameObject.activeInHierarchy)
        {
            switch (ShadowPriestShopLevelRewards[ShopLevel].GetShopRewards)
            {
                case (ShopRewards.Discount):
                    EquipmentDiscount();
                    break;
                case (ShopRewards.equipment):
                    if (ShadowPriestShopLevelRewards[ShopLevel].GetEquip.GetEquipmentType == EquipmentType.Weapon)
                    {
                        ShadowPriestShopLevelRewards[ShopLevel].GetEquip.GetComponent<DragUiObject>().enabled = false;
                        ShadowPriestShopLevelRewards[ShopLevel].GetEquip.transform.SetParent(WeaponTransform, false);
                    }
                    else
                    {
                        ShadowPriestShopLevelRewards[ShopLevel].GetEquip.GetComponent<DragUiObject>().enabled = false;
                        ShadowPriestShopLevelRewards[ShopLevel].GetEquip.transform.SetParent(ArmorTransform, false);
                    }
                    break;
            }
        }
    }

    private void ShowNextReward()
    {
        if(Knight.gameObject.activeInHierarchy)
        {
            switch (KnightShopLevelRewards[ShopLevel].GetShopRewards)
            {
                case (ShopRewards.Discount):
                    DiscountText.gameObject.SetActive(true);
                    EquipmentImage.gameObject.SetActive(false);
                    DiscountText.text = "Item discount -" + KnightShopLevelRewards[ShopLevel].GetDiscountAmount + "%";
                    break;
                case (ShopRewards.equipment):
                    EquipmentImage.gameObject.SetActive(true);
                    DiscountText.gameObject.SetActive(false);
                    EquipmentImage.sprite = KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentSprite;
                    break;
            }
        }
        else if(ShadowPriest.gameObject.activeInHierarchy)
        {
            switch (ShadowPriestShopLevelRewards[ShopLevel].GetShopRewards)
            {
                case (ShopRewards.Discount):
                    DiscountText.gameObject.SetActive(true);
                    EquipmentImage.gameObject.SetActive(false);
                    DiscountText.text = "Item discount -" + ShadowPriestShopLevelRewards[ShopLevel].GetDiscountAmount + "%";
                    break;
                case (ShopRewards.equipment):
                    EquipmentImage.gameObject.SetActive(true);
                    DiscountText.gameObject.SetActive(false);
                    EquipmentImage.sprite = ShadowPriestShopLevelRewards[ShopLevel].GetEquip.GetEquipmentSprite;
                    break;
            }
        }
    }

    private void ShowNextRewardPreview()
    {
        switch (KnightShopLevelRewards[ShopLevel].GetShopRewards)
        {
            case (ShopRewards.Discount):
                DiscountText.gameObject.SetActive(true);
                EquipmentImage.gameObject.SetActive(false);
                DiscountText.text = "Item discount -" + KnightShopLevelRewards[ShopLevel + ShopPreviewLevel].GetDiscountAmount + "%";
                break;
            case (ShopRewards.equipment):
                EquipmentImage.gameObject.SetActive(true);
                DiscountText.gameObject.SetActive(false);
                EquipmentImage.sprite = KnightShopLevelRewards[ShopLevel + ShopPreviewLevel].GetEquip.GetEquipmentSprite;
                break;
        }
    }

    private void EquipmentDiscount()
    {
        float Discount = KnightShopLevelRewards[ShopLevel].GetDiscountAmount / 100f;

        foreach (Equipment equip in WeaponTransform.GetComponentsInChildren<Equipment>(true))
        {
            float DiscountAmount = equip.GetEquipmentData.BuyValue * Discount;
            Mathf.Round(DiscountAmount);
            equip.GetEquipmentData.BuyValue -= (int)DiscountAmount;
        }
        foreach (Equipment equip in ArmorTransform.GetComponentsInChildren<Equipment>(true))
        {
            float DiscountAmount = equip.GetEquipmentData.BuyValue * Discount;
            Mathf.Round(DiscountAmount);
            equip.GetEquipmentData.BuyValue -= (int)DiscountAmount;
        }
    }

    private void PreviewLevelUp()
    {
        ShopPreviewLevel++;

        FillArea.fillAmount = 0;

        PreviewShopLevelText.text = "+" + ShopPreviewLevel;

        int SurplusExperience = Mathf.Abs(ExperiencePoints - NextToLevel);

        ExperiencePoints = SurplusExperience;

        int NextShopLevel = ShopLevelExperiences[ShopLevel + ShopPreviewLevel];

        NextToLevel = NextShopLevel;

        FillAreaTwo.fillAmount = (float)ExperiencePoints / (float)NextToLevel;

        ShowNextRewardPreview();

        PreviewNextLevelRewardText.text = "Next Level";
        NextLevelRewardText.text = "";
    }

    private void LevelUp()
    {
        GetReward();

        ShopLevel++;

        ShopPreviewLevel--;

        if(ShopPreviewLevel <= 0)
        {
            PreviewShopLevelText.text = "";
        }

        int NextShopLevel = ShopLevelExperiences[ShopLevel];

        NextToLevel = NextShopLevel;

        ShopLevelText.text = "Level: " + ShopLevel;
        UpgradeShopLevelText.text = "Level: " + ShopLevel;

        UpdateShopExperience();

        ShowNextReward();

        PreviewNextLevelRewardText.text = "";
        NextLevelRewardText.text = "Next Level";
    }

    public void ShowEquipmentRewardInfo()
    {
        if(KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType.Length == 1)
        {
            if(KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            "Element: " + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease;
            }
        }
        if (KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType.Length == 2)
        {
            if(KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            "Element: " + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease;
            }
        }
        if (KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType.Length == 3)
        {
            if(KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            "Element: " + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatIncrease;
            }
            
        }
        if (KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType.Length == 4)
        {
            if(KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatIncrease + "\n" +
                                            "Element: " + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatIncrease;
            }
            
        }
        if (KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType.Length == 5)
        {
            if(KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element != PlayerElement.NONE)
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[4].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[4].GetStatIncrease + "\n" +
                                            "Element: " + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.Element;
            }
            else
            {
                EquipmentRewardInfoText.text = "<size=12>" + "<u>" + KnightShopLevelRewards[ShopLevel].GetEquip.GetEquipmentData.EquipmentName + "</u>" + "</size>" + "\n\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[0].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[1].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[2].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[3].GetStatIncrease + "\n" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[4].GetStatusTypes + " +" +
                                            KnightShopLevelRewards[ShopLevel].GetEquip.GetStatusType[4].GetStatIncrease;
            }
            
        }
    }
}
