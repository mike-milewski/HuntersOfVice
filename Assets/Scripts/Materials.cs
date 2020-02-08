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

    private void Awake()
    {
        gameObject.GetComponent<Image>().sprite = materialData.MaterialSprite;

        MaterialName = materialData.MaterialName;

        MaterialDescription = materialData.MaterialDescription;

        ShopPoints = materialData.ShopPoints;
    }

    public void OpenInformationPanel()
    {
        GameManager.Instance.GetInventoryPanel.GetComponent<Inventory>().GetItemDescriptionPanel.SetActive(true);

        GameManager.Instance.GetInventoryPanel.GetComponent<Inventory>().GetItemDescriptionPanel.GetComponentInChildren<TextMeshProUGUI>().text =
                                                               "<size=12>" + "<u>" + MaterialName + "</u>" + "</size>" + "\n\n" +
                                                               MaterialDescription + "\n\n" + "Shop Points: " + ShopPoints + "\n\n" + "Quantity: " + Quantity;
    }

    public void CloseInformationPanel()
    {
        GameManager.Instance.GetInventoryPanel.GetComponent<Inventory>().GetItemDescriptionPanel.SetActive(false);
    }

    public void SetMaterialParent()
    {
        if(gameObject.transform.parent == GameManager.Instance.GetInventoryPanel.GetComponent<Inventory>().GetShopMaterialTransform)
        {
            gameObject.transform.SetParent(GameManager.Instance.GetShopUpgradePanel.transform);
            AddExperience();
        }
        else
        {
            gameObject.transform.SetParent(GameManager.Instance.GetInventoryPanel.GetComponent<Inventory>().GetShopMaterialTransform);
            SubtractExperience();
        }
    }

    private void AddExperience()
    {
        GameManager.Instance.GetShop.GetExperiencePoints += ShopPoints;
        //GameManager.Instance.GetShop.GetNextToLevel -= ShopPoints;
        GameManager.Instance.GetShop.ShowPreviewExperience();
    }

    private void SubtractExperience()
    {
        GameManager.Instance.GetShop.GetExperiencePoints -= ShopPoints;
        //GameManager.Instance.GetShop.GetNextToLevel += ShopPoints;
        GameManager.Instance.GetShop.ShowPreviewExperience();
    }
}
