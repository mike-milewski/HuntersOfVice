#pragma warning disable 0414
using UnityEngine;
using UnityEngine.UI;

public class Materials : MonoBehaviour
{
    [SerializeField]
    private MaterialData materialData;

    [SerializeField]
    private Image MaterialImage;

    [SerializeField]
    private string MaterialName;

    [SerializeField]
    private int SellValue, ShopPoints;

    private void Awake()
    {
        MaterialName = materialData.MaterialName;

        SellValue = materialData.SellValue;

        ShopPoints = materialData.ShopPoints;
    }
}
