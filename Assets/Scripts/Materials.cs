#pragma warning disable 0414
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
    private int SellValue, ShopPoints, Quantity;

    private void Awake()
    {
        gameObject.GetComponent<Image>().sprite = materialData.MaterialSprite;

        MaterialName = materialData.MaterialName;

        MaterialDescription = materialData.MaterialDescription;

        SellValue = materialData.SellValue;

        ShopPoints = materialData.ShopPoints;
    }

    public void OpenInformationPanel()
    {
        gameObject.transform.parent.parent.parent.GetComponent<Inventory>().GetItemDescriptionPanel.SetActive(true);

        gameObject.transform.parent.parent.parent.GetComponent<Inventory>().GetItemDescriptionPanel.GetComponentInChildren<TextMeshProUGUI>().text = 
                                                               "<size=12>" + "<u>" + MaterialName + "</u>" + "</size>" + "\n\n" +
                                                               MaterialDescription + "\n\n" + "Shop Points: " + ShopPoints + "\n" + "Sell Value: " + SellValue;
    }

    public void CloseInformationPanel()
    {
        gameObject.transform.parent.parent.parent.GetComponent<Inventory>().GetItemDescriptionPanel.SetActive(false);
    }
}
