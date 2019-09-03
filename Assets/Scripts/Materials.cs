using UnityEngine;

public enum MaterialType { Shop, Synthesis }

public class Materials : MonoBehaviour
{
    [SerializeField]
    private MaterialType materialType;

    [SerializeField]
    private int BuyValue;

    private int SellValue;

    public MaterialType GetMaterialType
    {
        get
        {
            return materialType;
        }
        set
        {
            materialType = value;
        }
    }

    public int GetBuyValue
    {
        get
        {
            return BuyValue;
        }
        set
        {
            BuyValue = value;
        }
    }

    public int GetSellValue
    {
        get
        {
            return SellValue;
        }
        set
        {
            SellValue = value;
        }
    }
}
