#pragma warning disable 0414, 0649
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Materials : MonoBehaviour
{
    [SerializeField]
    private MaterialData materialData;

    private string MaterialDescription;

    [SerializeField]
    private string MaterialName;

    [SerializeField]
    private int ShopPoints, Quantity;

    public MaterialData GetMaterialData
    {
        get
        {
            return materialData;
        }
        set
        {
            materialData = value;
        }
    }

    public string GetMaterialName
    {
        get
        {
            return MaterialName;
        }
        set
        {
            MaterialName = value;
        }
    }

    public int GetShopPoints
    {
        get
        {
            return ShopPoints;
        }
        set
        {
            ShopPoints = value;
        }
    }

    public int GetQuantity
    {
        get
        {
            return Quantity;
        }
        set
        {
            Quantity = value;
        }
    }

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = materialData.MaterialSprite;

        MaterialName = materialData.MaterialName;

        MaterialDescription = materialData.MaterialDescription;

        ShopPoints = materialData.ShopPoints;
    }

    public void OpenInformationPanel()
    {
        if (gameObject.transform.parent == GameManager.Instance.GetInventoryPanel.GetComponent<Inventory>().GetShopMaterialTransform)
        {
            GameManager.Instance.GetInventoryPanel.GetComponent<Inventory>().GetItemDescriptionPanel.SetActive(true);

            GameManager.Instance.GetInventoryPanel.GetComponent<Inventory>().GetItemDescriptionPanel.GetComponentInChildren<TextMeshProUGUI>().text =
                                                                   "<size=12>" + "<u>" + MaterialName + "</u>" + "</size>" + "\n\n" +
                                                                   MaterialDescription + "\n\n" + "Shop Points: " + ShopPoints + "\n\n" + "Quantity: " + Quantity;
        }
        else
        {
            GameManager.Instance.GetItemDescriptionPanel.SetActive(true);

            GameManager.Instance.GetItemDescriptionPanel.GetComponentInChildren<TextMeshProUGUI>().text =
                                                                   "<size=12>" + "<u>" + MaterialName + "</u>" + "</size>" + "\n\n" +
                                                                   MaterialDescription + "\n\n" + "Shop Points: " + ShopPoints + "\n\n" + "Quantity: " + Quantity;
        }
    }

    public void CloseInformationPanel()
    {
        if (gameObject.transform.parent == GameManager.Instance.GetInventoryPanel.GetComponent<Inventory>().GetShopMaterialTransform)
        {
            GameManager.Instance.GetInventoryPanel.GetComponent<Inventory>().GetItemDescriptionPanel.SetActive(false);
        }
        else
        {
            GameManager.Instance.GetItemDescriptionPanel.SetActive(false);
        }
    }

    public void SetMaterialParent()
    {
        if(GameManager.Instance.GetShopupgrade.GetCanUpgrade)
        {
            if (gameObject.transform.parent == GameManager.Instance.GetInventoryPanel.GetComponent<Inventory>().GetShopMaterialTransform)
            {
                CloseInformationPanel();
                gameObject.transform.SetParent(GameManager.Instance.GetShopUpgradePanel.transform);
                AddExperience();
            }
            else
            {
                CloseInformationPanel();
                gameObject.transform.SetParent(GameManager.Instance.GetInventoryPanel.GetComponent<Inventory>().GetShopMaterialTransform);
                SubtractExperience();
            }
        }
    }

    private void AddExperience()
    {
        GameManager.Instance.GetShop.GetExperiencePoints += ShopPoints;
        GameManager.Instance.GetShop.GetNTL -= ShopPoints;
        GameManager.Instance.GetShop.ShowPreviewExperience();
    }

    private void SubtractExperience()
    {
        GameManager.Instance.GetShop.GetExperiencePoints -= ShopPoints;
        GameManager.Instance.GetShop.GetNTL += ShopPoints;
        GameManager.Instance.GetShop.ShowPreviewExperience();
    }
}
