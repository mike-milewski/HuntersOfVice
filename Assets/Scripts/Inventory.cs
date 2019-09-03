using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Transform CoinTextParent;

    [SerializeField]
    private GameObject ShopMaterials, SynthMaterials;

    [SerializeField]
    private TextMeshProUGUI CoinText;

    private int Coins;

    public int GetCoinAmount(int value)
    {
        Coins += value;

        CoinText.text = Coins.ToString();

        return value;
    }

    public TextMeshProUGUI ReturnGoldText()
    {
        var CoinText = ObjectPooler.Instance.GetCoinText();

        CoinText.SetActive(true);

        CoinText.transform.SetParent(CoinTextParent.transform, false);

        return CoinText.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ToggleMaterials()
    {
        if(ShopMaterials.transform.childCount > 0)
        {
            foreach(Materials obj in ShopMaterials.GetComponentsInChildren<Materials>(true))
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
        if(SynthMaterials.transform.childCount > 0)
        {
            foreach (Materials obj in SynthMaterials.GetComponentsInChildren<Materials>(true))
            {
                if (!obj.gameObject.activeInHierarchy)
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
}
