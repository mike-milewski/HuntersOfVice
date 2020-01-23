#pragma warning disable 0414
using UnityEngine;

public class Materials : MonoBehaviour
{
    [SerializeField]
    private MaterialData materialData;

    [SerializeField]
    private Sprite MaterialSprite;

    [SerializeField]
    private string MaterialName;

    [SerializeField]
    private int SellValue, ShopPoints, Quantity;

    private void Awake()
    {
        MaterialSprite = materialData.MaterialSprite;

        MaterialName = materialData.MaterialName;

        SellValue = materialData.SellValue;

        ShopPoints = materialData.ShopPoints;
    }
}
