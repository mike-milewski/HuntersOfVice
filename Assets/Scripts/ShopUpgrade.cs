#pragma warning disable 0649
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgrade : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private Transform UpgradeTransform, MaterialTransform;

    private bool CanUpgrade;

    public bool GetCanUpgrade
    {
        get
        {
            return CanUpgrade;
        }
        set
        {
            CanUpgrade = value;
        }
    }

    public void ToggleMaterials()
    {
        if(UpgradeTransform.childCount > 0)
        {
            foreach (Materials materials in UpgradeTransform.GetComponentsInChildren<Materials>(true))
            {
                materials.transform.SetParent(MaterialTransform, true);
                if(!inventory.GetInInventory)
                {
                    materials.gameObject.SetActive(false);
                }
                else
                {
                    materials.gameObject.SetActive(true);
                }
                GameManager.Instance.GetShop.GetExperiencePoints -= materials.GetShopPoints;
                GameManager.Instance.GetShop.ShowPreviewExperience();
            }
        }
    }

    public void Confirm()
    {
        if (UpgradeTransform.childCount > 0)
        {
            foreach (Materials materials in UpgradeTransform.GetComponentsInChildren<Materials>(true))
            {
                GameManager.Instance.GetShop.GainExperience();
                Destroy(materials.gameObject);
            }
        }
    }

    public void CanUpgradeShop()
    {
        CanUpgrade = true;
    }

    public void CannotUpgradeShop()
    {
        CanUpgrade = false;
    }
}
