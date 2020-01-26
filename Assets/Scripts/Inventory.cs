using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Transform CoinTextParent;

    [SerializeField]
    private Transform ShopMaterialTransform;

    [SerializeField]
    private GameObject ItemDescriptionPanel;

    [SerializeField]
    private TextMeshProUGUI CoinText;

    private bool InInventory;

    private int Coins;

    public int GetCoins
    {
        get
        {
            return Coins;
        }
        set
        {
            Coins = value;
        }
    }

    public bool GetInInventory
    {
        get
        {
            return InInventory;
        }
        set
        {
            InInventory = value;
        }
    }

    public Transform GetShopMaterialTransform
    {
        get
        {
            return ShopMaterialTransform;
        }
        set
        {
            ShopMaterialTransform = value;
        }
    }

    public GameObject GetItemDescriptionPanel
    {
        get
        {
            return ItemDescriptionPanel;
        }
        set
        {
            ItemDescriptionPanel = value;
        }
    }

    public int AddCoins(int value)
    {
        Coins += value;

        CoinText.text = Coins.ToString();

        return value;
    }

    public TextMeshProUGUI ReturnCoinText()
    {
        var CoinText = ObjectPooler.Instance.GetCoinText();

        CoinText.SetActive(true);

        CoinText.transform.SetParent(CoinTextParent.transform, false);

        return CoinText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ToggleMaterials()
    {
        if(ShopMaterialTransform.childCount > 0)
        {
            foreach(Materials obj in ShopMaterialTransform.GetComponentsInChildren<Materials>(true))
            {
                if(!obj.gameObject.activeInHierarchy)
                {
                    obj.gameObject.SetActive(true);
                }
                else
                {
                    obj.gameObject.SetActive(false);
                }
            }
        }
    }

    public void CloseInformationPanel()
    {
        ItemDescriptionPanel.SetActive(false);
    }

    public void ToggleInInventory()
    {
        if(!InInventory)
        {
            InInventory = true;
        }
        else
        {
            InInventory = false;
        }
    }
}
