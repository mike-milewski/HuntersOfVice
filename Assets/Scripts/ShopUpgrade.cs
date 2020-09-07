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
            foreach(Materials m in UpgradeTransform.GetComponentsInChildren<Materials>(true))
            {
                GameManager.Instance.GetShop.GetExperiencePoints -= m.GetShopPoints * m.GetCommittedQuantity;
            }
            CheckForSameMaterialName();
            GameManager.Instance.GetShop.ShowPreviewExperience();
        }
    }

    private bool CheckForSameMaterialName()
    {
        bool SameName = false;

        foreach (Materials mat in MaterialTransform.GetComponentsInChildren<Materials>(true))
        {
            foreach(Materials m in UpgradeTransform.GetComponentsInChildren<Materials>(true))
            {
                if (mat.GetMaterialData.MaterialName == m.GetMaterialData.MaterialName)
                {
                    SameName = true;

                    mat.GetQuantity += m.GetCommittedQuantity;

                    m.transform.SetParent(GameManager.Instance.transform);

                    Destroy(m.gameObject);
                }
            }
        }
        return SameName;
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
